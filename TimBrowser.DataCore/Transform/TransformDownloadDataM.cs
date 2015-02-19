using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;
using TimBrowser.DataCore.Model;
using System.ComponentModel;
using TimBrowser.DataCore.Model.Device;
using TimBrowser.DataCore.Helpers;
using TimBrowser.DataCore.Model.Logs;
using TpeParameters.Helpers;
using TimBrowser.DataCore.Services;
using TpeParameters.Model;



namespace TimBrowser.DataCore.Transform
{
    /*public enum TranformErrorCodes
    {
        NoErr = 0,
        GetDeviceInfoErr = 10
    }*/

    public class TransformDownloadDataM
    {
        public TransformDownloadDataM()
        {
            _parametersTemplateService = new ParametersTemplateService();
        }

        private readonly ParametersTemplateService _parametersTemplateService;

        public InformationModuleData Transform(FuncDownloadDataM funcDownloadData)
        {
            InformationModuleData im = null;

            try
            {
                int deviceId = funcDownloadData.FuncOne.IdOfDevice;
                TableItem paramTableTemplate = _parametersTemplateService.GetParametersTemplate(deviceId);
                TableItem paramTable = FillParametersTable(funcDownloadData, paramTableTemplate);

                DeviceInfo deviceInfo = GetDeviceInfo(funcDownloadData, paramTable);
                List<DeviceLogInfo> deviceLogsInfo = GetDeviceLogsInfo(funcDownloadData, deviceInfo);

                List<LogEventRecordItem> logEvent = null;
                List<LogCmdRecordItem> logCmd = null;
                List<LogParamRecordItem> logParamRecord = null;

                for (int logNum = 0; logNum < deviceInfo.LogsCount; logNum++)
                {
                    LogTypes logType = deviceLogsInfo[logNum].Type;

                    switch (logType)
                    {
                        case LogTypes.EventLog:
                            logEvent = GetLogEvents(funcDownloadData, deviceLogsInfo, paramTable);
                            break;

                        case LogTypes.CommandLog:
                            logCmd = GetLogCommands(funcDownloadData, deviceLogsInfo, paramTable);
                            break;

                        case LogTypes.ParameterLog:
                            logParamRecord = GetLogParam(funcDownloadData, deviceLogsInfo, paramTable);
                            break;
                    }
                }

                DeviceLogs deviceLogs = new DeviceLogs(logEvent, logCmd, logParamRecord);

                im = new InformationModuleData(deviceInfo, deviceLogsInfo, deviceLogs, paramTable);
            }
            catch (Exception)
            {

            }

            return im;
        }



        private TableItem FillParametersTable(FuncDownloadDataM funcDownloadData, TableItem parametersTemplate)
        {
            int paramsNum = funcDownloadData.FuncOne.ParametersNumber;
            int groupsNum = parametersTemplate.Groups.Count;

            try
            {

                for (int indexGroup = 0; indexGroup < groupsNum; indexGroup++)
                {
                    GroupItem gr = parametersTemplate.Groups[indexGroup];

                    int groupParamsNum = gr.Parameters.Count;

                    for (int iParam = 0; iParam < groupParamsNum; iParam++)
                    {
                        int paramAddress = gr.Parameters[iParam].Address;

                        if (paramAddress < funcDownloadData.FuncSix.ParametersValues.Count)
                            gr.Parameters[iParam].SetUnsValue(funcDownloadData.FuncSix.ParametersValues[paramAddress]);

                        //gr.Parameters[iParam] = TransformParamValue(gr.Parameters[iParam]);
                    }

                    //parametersTemplate.Groups.Add(gr);

                    // paramTable.Groups.Add(gr);
                }
            }
            catch (Exception e)
            {

            }

            return parametersTemplate;
        }

        private DeviceInfo GetDeviceInfo(FuncDownloadDataM funcDownloadData, TableItem paramTable)
        {
            int firmwarVersion = funcDownloadData.FuncOne.FirmwareVersion;
            int deviceId = funcDownloadData.FuncOne.IdOfDevice;
            int parametersCount = funcDownloadData.FuncOne.ParametersNumber;
            int logsCount = funcDownloadData.FuncOne.LogsNumber;

            ParameterItem productYearParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.ProductYear);
            ParameterItem factoryNumberParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.FactoryNumber);

            if (productYearParameter == null || factoryNumberParameter == null)
                throw new TransformDownloadDataException(TranformErrorCodes.GetDeviceInfoErr);

            int productYear = (int)productYearParameter.Value;
            int factoryNumber = (int)factoryNumberParameter.Value;
            string deviceNumStr = GetDeviceNumber(productYear, factoryNumber, 
                factoryNumberParameter.ValueDescription.Maximum.ToString().Length);
            string deviceName = paramTable.DeviceName;

            DeviceInfo deviceInfo = new DeviceInfo(deviceId, firmwarVersion, parametersCount,
                logsCount, productYear, factoryNumber, deviceNumStr, deviceName);

            return deviceInfo;
        }

        private string GetDeviceNumber(int year, int factNum, int lenght)
        {
            int currentDigitLen = factNum.ToString().Length;

            int zeroCount = lenght - currentDigitLen;

            if (zeroCount <= 0)
                return year.ToString() + factNum.ToString();

            string zeroString = String.Empty;

            for (int i = zeroCount; i > 0; i--)
            {
                zeroString += "0";
            }

            string digitString = year.ToString() + zeroString + factNum.ToString();

            return digitString;
        }

        private List<DeviceLogInfo> GetDeviceLogsInfo(FuncDownloadDataM funcDownloadData, DeviceInfo deviceInfo)
        {
            int logsCount = deviceInfo.LogsCount;

            List<DeviceLogInfo> deviceLogsInfo = new List<DeviceLogInfo>();

            // Анализируем журналы
            for (int indexLog = 0; indexLog < logsCount; indexLog++)
            {
                LogTypes logType = (LogTypes)funcDownloadData.FuncOne.LogsTypes[indexLog];
                int logAddress = funcDownloadData.FuncTwo.LogsData[indexLog].LogCurrentAddress;
                int logRecsNum = funcDownloadData.FuncTwo.LogsData[indexLog].LogRecordsNumber;
                int logCellsNum = funcDownloadData.FuncThree.LogsData[indexLog].LogCellNumber;

                List<TimBrowser.DataCore.Model.Device.DeviceLogInfo.CellField> cellFields = 
                    new List<DeviceLogInfo.CellField>();

                // Анализируем ячейки
                for (int indexCell = 0; indexCell < logCellsNum; indexCell++)
                {
                    int cellFieldsNum = funcDownloadData.FuncThree.LogsData[indexLog].LogCellFieldNumber[indexCell];
                    int[] cellFieldsAddrs = new int[cellFieldsNum];

                    // Анализируем поля ячеек
                    for (int indexField = 0; indexField < cellFieldsNum; indexField++)
                    {
                        cellFieldsAddrs[indexField]
                            = funcDownloadData.FuncFour.LogsData[indexLog].LogsCells[indexCell].LogFieldsAddress[indexField];
                    }

                    cellFields.Add(new DeviceLogInfo.CellField(cellFieldsAddrs));

                }

                deviceLogsInfo.Add(
                    new DeviceLogInfo(logType, indexLog, logAddress, logRecsNum, logCellsNum, cellFields));

            }

            return deviceLogsInfo;
        }

        private List<LogEventRecordItem> GetLogEvents(FuncDownloadDataM funcDownloadData, 
            List<DeviceLogInfo> deviceLogsInfo, TableItem paramTable)
        {
            #region Fault register values

            // Нашли Fault-параметры, которые отвечают за журнал событий
            List<ParameterItem> eventRegParams = GetLogEventParameters(paramTable);

            // Ищем индекс в массиве для журнала событий
            int indexEventLog = deviceLogsInfo.Find(l => l.Type == LogTypes.EventLog).Index;

            // Количество записей журнала событий
            int recsCount = deviceLogsInfo[indexEventLog].RecsCnt;

            // список с индексами Fault регистров в массиве
            List<int> eventRegIndicies = new List<int>();

            // список значений Fault регистров. eventRegValues[0] это значения -> eventRegIndicies[0] 
            List<int[]> eventRegValues = new List<int[]>();

            foreach (var p in eventRegParams)
            {
                // ищем индекс в массиве для текущего fault регистра
                // из информации об основной ячейки журнала событий извлекаем индекс по адрессу Fault параметра
                int eventRegIndex =
                    Array.FindIndex(deviceLogsInfo[indexEventLog].CellFields[Codes.MainCellIndex].ParamAddress,
                    e => e.Equals(p.Address));

                // массив значений для текущего Fault регистра
                int[] currentFaultRegValues = new int[recsCount];

                // заполняем текущий Fault регистр значениями по всем записям
                for (int recNum = 0; recNum < recsCount; recNum++)
                {
                    currentFaultRegValues[recNum] =
                        funcDownloadData.FuncFive.LogsData[indexEventLog].LogFieldRecords[recNum].
                        LogFieldCells[Codes.MainCellIndex].LogFieldValues[eventRegIndex];
                }

                eventRegValues.Add(currentFaultRegValues);
                eventRegIndicies.Add(eventRegIndex);
            }

            #endregion

            #region Fault codes parsing

            List<EventCode> eventCodesTemp = new List<EventCode>();

            int recNumDbg = 0;
            int evRegNumDbg = 0;
            int iBitDbg = 0;

            try
            {
                for (int recNum = 0; recNum < recsCount; recNum++)
                {
                    recNumDbg = recNum;

                    // флаг первого события. означает, что данное событие было записано первым после включения блока
                    bool firstEvent = (funcDownloadData.FuncFive.LogsData[indexEventLog].
                        LogFieldRecords[recNum].LogFieldCells[Codes.MainCellIndex].LogFieldValues

                    // индекс значения текущего fault параметра = последнее поле в основной ячейке
                        // последнее поле считаем по общему число полей в основной ячейке журнала
                        // если это значение равно константе FirstEventIndex значит - первое событие
                    [(deviceLogsInfo[indexEventLog].CellFields[Codes.MainCellIndex].ParamAddress.Length - 1)] ==
                        Codes.FirstEventIndex);

                    int[] currentValueArray = new int[eventRegValues.Count];
                    int[] prevValueArray = new int[eventRegValues.Count];

                    // анализируем каждый fault регистр
                    for (int evRegNum = 0; evRegNum < eventRegValues.Count; evRegNum++)
                    {
                        evRegNumDbg = evRegNum;

                        ParameterItem registerParam = eventRegParams[evRegNum];

                        if (eventRegValues[evRegNum][recNum] != 65535)
                        {
                            // присваеваем предыдущее значение текущего fault регистра
                            if (recNum == 0) prevValueArray[evRegNum] = 0;
                            else prevValueArray[evRegNum] = eventRegValues[evRegNum][recNum - 1];

                            // присываеваем текущее значение текущего fault регистра
                            currentValueArray[evRegNum] = eventRegValues[evRegNum][recNum];

                            // анализируем каждый бит fault регистра
                            for (int iBit = 15; iBit >= 0; iBit--)
                            {
                                iBitDbg = iBit;

                                int bitRegValue = currentValueArray[evRegNum];

                                // проверяем выставлен ли текущий и предыдущий биты
                                bool currentSet = ((currentValueArray[evRegNum] >> iBit) & 1) == 1;
                                bool prevSet = (((prevValueArray[evRegNum] >> iBit) & 1) == 1) && !firstEvent;

                                // флаг, который определяет выставлено событие или снято
                                bool? eventWasSet = null;

                                // предыдущий выставлен, а текущий не выставлен = событие снято!
                                if (!currentSet && prevSet)                 // UnSet
                                    eventWasSet = false;
                                else if ((currentSet && !prevSet))          // Set
                                    eventWasSet = true;

                                if (eventWasSet != null)
                                {
                                    EventCode code = new EventCode();

                                    var bitValue = registerParam.ValueDescription.Fields.Where(
                                        f => f.BitValue == iBit + 1).FirstOrDefault();

                                    if (bitValue != null)
                                    {
                                        code.Name = registerParam.ValueDescription.Fields.Find(
                                            f => f.BitValue == iBit + 1).SpecialDescription;
                                        code.Description = registerParam.ValueDescription.Fields.Find(
                                            f => f.BitValue == iBit + 1).Description;
                                        code.RecordIndex = recNum;
                                        code.Set = eventWasSet;

                                        eventCodesTemp.Add(code);
                                    }
                                }
                            }
                        }
                        else
                        {
                            prevValueArray[evRegNum] = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            // удаляем повторяющие элементы из списка кодов событий
            List<EventCode> eventCodes = eventCodesTemp.Distinct().ToList();

            #endregion

            List<LogEventRecordItem> logEventRecods = new List<LogEventRecordItem>();

            #region Event records generation

            int dbgCode = 0;
            int dbgCell = 0;
            int dbgParam = 0;

            try
            {
                for (int iCode = 0; iCode < eventCodes.Count; iCode++)
                {
                    dbgCode = iCode;

                    bool convErr = false;

                    LogEventMainCellItem mainCell = null;
                    List<LogEventBufferCellItem> bufferCells = new List<LogEventBufferCellItem>();
                    ParameterItem dateParameter = null;
                    ParameterItem timeParameter = null;
                    ParameterItem secondsParameter = null;
                    DateTime recordMainDateTime = new DateTime();

                    for (int iCell = 0; iCell < deviceLogsInfo[indexEventLog].CellsCnt; iCell++)
                    {
                        dbgCell = iCell;

                        int paramsNum =
                            deviceLogsInfo[indexEventLog].CellFields[iCell].ParamAddress.Length;

                        List<ParameterItem> cellParameters = new List<ParameterItem>();

                        for (int iParam = 0; iParam < paramsNum; iParam++)
                        {
                            if (iParam == 19)
                            {
                                dbgParam = iParam;
                            }

                            dbgParam = iParam;

                            // Адресс параметра в таблице параметров
                            int address = deviceLogsInfo[indexEventLog].CellFields[iCell].ParamAddress[iParam];

                            // ищем этот параметр в таблице
                            if (address != Codes.FirstRecordFlagAddr)
                            {
                                // находим шаблонный параметр
                                ParameterItem tempPar = Utils.FindParameterByAddress(address, paramTable);

                                if (tempPar != null)
                                {
                                    ParameterItem parameter = new ParameterItem(tempPar);
                                    bool regFlag = false;

                                    //проверяем, что текущий параметр является Fault параметр
                                    if (parameter.Configuration.Appointment == ParamAppointments.Fault)
                                    { regFlag = true; }

                                    if (!regFlag)           // Проверяем, что параметр не является регистром события
                                    {
                                        parameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexEventLog].LogFieldRecords[
                                            eventCodes[iCode].RecordIndex].LogFieldCells[iCell].LogFieldValues[iParam]);

                                        // трансформация значения параметра будет происходит непосредственно в классе ParamterItem
                                        //parameter = new ParameterItem(TransformParamValue(parameter));

                                        // параметры даты и времени могут быть только в основной ячейке
                                        if (iCell == Codes.MainCellIndex)
                                        {
                                            if (parameter.Configuration.Appointment == ParamAppointments.Date)
                                                dateParameter = parameter;
                                            else if (parameter.Configuration.Appointment == ParamAppointments.Time)
                                                timeParameter = parameter;
                                            else if (parameter.Configuration.Appointment == ParamAppointments.Seconds)
                                                secondsParameter = parameter;
                                        }

                                        bool dateTimeFlag = false;

                                        if (parameter.Configuration.Appointment == ParamAppointments.Date ||
                                            parameter.Configuration.Appointment == ParamAppointments.Time ||
                                            parameter.Configuration.Appointment == ParamAppointments.Seconds)
                                        {
                                            dateTimeFlag = true;
                                        }

                                        if (!dateTimeFlag)
                                        {
                                            cellParameters.Add(parameter);
                                        }

                                    }
                                }
                            }
                        }

                        try
                        {
                            // создаем объекты записей журнала
                            if (iCell == Codes.MainCellIndex)
                            {
                                recordMainDateTime = Utils.GenerateLogDateTime(dateParameter, timeParameter, secondsParameter);

                                mainCell = new LogEventMainCellItem(recordMainDateTime, eventCodes[iCode].Name,
                                    eventCodes[iCode].Description, eventCodes[iCode].RecordIndex,
                                    (bool)eventCodes[iCode].Set, cellParameters);
                            }
                            else
                            {
                                // от времени основной ячейки вычитаются секунды
                                bufferCells.Add(new LogEventBufferCellItem(
                                    recordMainDateTime.AddSeconds(-iCell), cellParameters
                                    ));
                            }
                        }
                        catch (Exception e)
                        {
                            convErr = true;
                        }
                    }

                    if (!convErr)
                        logEventRecods.Add(new LogEventRecordItem(mainCell, bufferCells));
                }

            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }

            #endregion

            return logEventRecods;

        }

        public List<ParameterItem> GetLogEventParameters(TableItem paramTable)
        {
            List<ParameterItem> parameters = new List<ParameterItem>();

            foreach (var g in paramTable.Groups)
            {
                var pars = from p in g.Parameters
                           where p.Configuration.Appointment == ParamAppointments.Fault
                           select p;

                if (pars.Count() > 0)
                {
                    parameters.AddRange(pars.ToList());
                }
            }

            if (parameters.Count == 0)
                return null;

            return parameters;
        }

        private List<LogCmdRecordItem> GetLogCommands(FuncDownloadDataM funcDownloadData,
            List<DeviceLogInfo> deviceLogsInfo, TableItem paramTable)
        {
            List<LogCmdRecordItem> logCmdRecords = new List<LogCmdRecordItem>();

            int indexCmdLog = deviceLogsInfo.Find(p => p.Type == LogTypes.CommandLog).Index;
            int logCmdRecsCnt = deviceLogsInfo[indexCmdLog].RecsCnt;

            ParameterItem statusTemplatePar = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Status);
            ParameterItem commandsTemplatePar = Utils.FindParameterByAppointment(paramTable, ParamAppointments.LogCmdControlWord);
            ParameterItem dateTemaplateParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Date);
            ParameterItem timeTemaplateParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Time);
            ParameterItem secondsTemaplateParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Seconds);

            int statusParFieldIndex = Utils.GetParamFieldIndex(statusTemplatePar.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexCmdLog]);
            int commandParParFieldIndex = Utils.GetParamFieldIndex(commandsTemplatePar.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexCmdLog]);
            int dateParFieldIndex = Utils.GetParamFieldIndex(dateTemaplateParameter.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexCmdLog]);
            int timeParFieldIndex = Utils.GetParamFieldIndex(timeTemaplateParameter.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexCmdLog]);
            int secondsParFieldIndex = Utils.GetParamFieldIndex(secondsTemaplateParameter.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexCmdLog]);

            for (int iRec = 0; iRec < logCmdRecsCnt; iRec++)
            {
                ParameterItem status = new ParameterItem(statusTemplatePar);
                ParameterItem command = new ParameterItem(commandsTemplatePar);

                // проверяем, что поле не содержит дефектных данных
                if (funcDownloadData.FuncFive.LogsData[indexCmdLog].LogFieldRecords[iRec].
                    LogFieldCells[Codes.MainCellIndex].LogFieldValues[statusParFieldIndex] != 65535)
                {
                    status.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexCmdLog].LogFieldRecords[iRec].
                            LogFieldCells[Codes.MainCellIndex].LogFieldValues[statusParFieldIndex]);

                    command.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexCmdLog].LogFieldRecords[iRec].
                            LogFieldCells[Codes.MainCellIndex].LogFieldValues[commandParParFieldIndex]);

                    dateTemaplateParameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexCmdLog].LogFieldRecords[iRec].
                            LogFieldCells[Codes.MainCellIndex].LogFieldValues[dateParFieldIndex]);

                    timeTemaplateParameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexCmdLog].LogFieldRecords[iRec].
                            LogFieldCells[Codes.MainCellIndex].LogFieldValues[timeParFieldIndex]);

                    secondsTemaplateParameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexCmdLog].LogFieldRecords[iRec].
                            LogFieldCells[Codes.MainCellIndex].LogFieldValues[secondsParFieldIndex]);

                    DateTime cmdRecordDateTime = new DateTime();

                    try
                    {
                        cmdRecordDateTime = Utils.GenerateLogDateTime(dateTemaplateParameter,
                            timeTemaplateParameter, secondsTemaplateParameter);
                    }
                    catch (Exception)
                    {

                    }

                    // Источник команд пока выставляем вручную
                    logCmdRecords.Add(new LogCmdRecordItem(cmdRecordDateTime, command, status, CommandSource.None));

                }
            }

            return logCmdRecords;

        }

        private List<LogParamRecordItem> GetLogParam(FuncDownloadDataM funcDownloadData, List<DeviceLogInfo> deviceLogsInfo,
            TableItem paramTable)
        {
            List<LogParamRecordItem> logParamRecords = new List<LogParamRecordItem>();

            int indexParamLog = deviceLogsInfo.Find(p => p.Type == LogTypes.ParameterLog).Index;
            int logParamsCnt = deviceLogsInfo[indexParamLog].RecsCnt;

            ParameterItem dateTemaplateParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Date);
            ParameterItem timeTemaplateParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Time);
            ParameterItem secondsTemaplateParameter = Utils.FindParameterByAppointment(paramTable, ParamAppointments.Seconds);

            int dateParFieldIndex = Utils.GetParamFieldIndex(dateTemaplateParameter.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexParamLog]);
            int timeParFieldIndex = Utils.GetParamFieldIndex(timeTemaplateParameter.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexParamLog]);
            int secondsParFieldIndex = Utils.GetParamFieldIndex(secondsTemaplateParameter.Address, Codes.MainCellIndex,
                deviceLogsInfo[indexParamLog]);
            int newParamFieldIndex = Utils.GetParamFieldIndex(Codes.NewParamAddr, Codes.MainCellIndex,
                deviceLogsInfo[indexParamLog]);
            int newParamValueIndex = Utils.GetParamFieldIndex(Codes.NewParamValue, Codes.MainCellIndex,
                deviceLogsInfo[indexParamLog]);

            for (int iRec = 0; iRec < logParamsCnt; iRec++)
            {
                int paramAddress = funcDownloadData.FuncFive.LogsData[indexParamLog].LogFieldRecords[
                    iRec].LogFieldCells[Codes.MainCellIndex].LogFieldValues[newParamFieldIndex];

                ParameterItem parameterTemplate = Utils.FindParameterByAddress(paramAddress, paramTable);

                if (parameterTemplate != null)
                {
                    // параметр необходимо скопировать, чтобы не переприсваивать значения тех параметров, которые в шаблоне
                    ParameterItem parameter = new ParameterItem(parameterTemplate);

                    if (funcDownloadData.FuncFive.LogsData[indexParamLog].LogFieldRecords[
                    iRec].LogFieldCells[Codes.MainCellIndex].LogFieldValues[newParamValueIndex] != 65535)
                    {
                        parameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexParamLog].LogFieldRecords[
                        iRec].LogFieldCells[Codes.MainCellIndex].LogFieldValues[newParamValueIndex]);

                        dateTemaplateParameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexParamLog].LogFieldRecords[
                        iRec].LogFieldCells[Codes.MainCellIndex].LogFieldValues[dateParFieldIndex]);

                        timeTemaplateParameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexParamLog].LogFieldRecords[
                            iRec].LogFieldCells[Codes.MainCellIndex].LogFieldValues[timeParFieldIndex]);

                        secondsTemaplateParameter.SetUnsValue(funcDownloadData.FuncFive.LogsData[indexParamLog].LogFieldRecords[
                            iRec].LogFieldCells[Codes.MainCellIndex].LogFieldValues[secondsParFieldIndex]);

                        DateTime logParamRecordDateTime = Utils.GenerateLogDateTime(dateTemaplateParameter,
                            timeTemaplateParameter, secondsTemaplateParameter);

                        logParamRecords.Add(new LogParamRecordItem(logParamRecordDateTime, parameter));
                    }
                }
            }

            return logParamRecords;
        }
    }
}
