using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Model;
using TimBrowser.DataCore.Model.Logs;
using TimBrowser.Helper;
using TpeParameters.Model;
using System.Collections.ObjectModel;

namespace TimBrowser.Mapper
{
    public static class LogMapper
    {
        /// <summary>
        /// Преобразование списка журнала событий для использования во ViewModel
        /// </summary>
        /// <param name="logEventRecords">Оригинальный список журнала событий</param>
        /// <returns>Преобразованный список журнала событий</returns>
        public static ObservableCollection<TimLogEvRecItem> MapLogEv(List<LogEventRecordItem> logEventRecords)
        {
            if (logEventRecords == null)
                return null;

            ObservableCollection<TimLogEvRecItem> timLogEvRecords = new ObservableCollection<TimLogEvRecItem>();

            int numberCounter = 0;

            foreach (var ler in logEventRecords)
            {
                numberCounter++;

                string dateTimeString = ler.LogEventMainCell.DateAndTime.
                    ToString(Constants.DATE_TIME_FORMAT_STRING);

                string name = ler.LogEventMainCell.Id.ToString();
                bool isSet = ler.LogEventMainCell.Set;

                List<TimParameterItem> parameters =
                    ParameterMapper.MapLogEvParameters(ler);

                // ObservableCollection<TimParameterItem> chosenParameters =
                //    ParameterMapper.MapLogEvChosenParameters(ler);

                ObservableCollection<TimChosenParameterItem> chosenParameters =
                    ParameterMapper.MapLogEvChosenParametersCol(ler);
                
                timLogEvRecords.Add(new TimLogEvRecItem(numberCounter,
                    dateTimeString, name, isSet, parameters, chosenParameters));
            }

            return timLogEvRecords;
        }

        /// <summary>
        /// Преобразование списка журнала команд для использования во ViewModel
        /// </summary>
        /// <param name="logCmdRecords">Оригинальный список журанала команд</param>
        /// <returns>Преобразованный список журнала команд</returns>
        public static ObservableCollection<TimLogCmdRecItem> MapLogCmd(List<LogCmdRecordItem> logCmdRecords)
        {
            if (logCmdRecords == null)
                return null;

            ObservableCollection<TimLogCmdRecItem> timLogCmdRecords = new ObservableCollection<TimLogCmdRecItem>();

            int numberCounter = 0;

            foreach (var lcr in logCmdRecords)
            {
                numberCounter++;

                string dateTimeString = lcr.DateAndTime.ToString(Constants.DATE_TIME_FORMAT_STRING);
                string cmdName = String.Empty;

                int cmdValue = (int)lcr.Command.Value;

                // ищем поле, которое имеет числовое значение команды
                var cmd = (from c in lcr.Command.ValueDescription.Fields
                           where c.BitValue == cmdValue
                           select c).FirstOrDefault();

                if (cmd != null)
                {
                    cmdName = cmd.Description;
                }

                TimParameterItem statusPar = ParameterMapper.MapParameter(0, lcr.Status, lcr.DateAndTime);

                timLogCmdRecords.Add(new TimLogCmdRecItem(numberCounter,
                    dateTimeString, cmdName, statusPar));
            }

            return timLogCmdRecords;

        }

        /// <summary>
        /// Преобразование списка журнала изменения параметров для использования во ViewModel
        /// </summary>
        /// <param name="logParamRecords">Оригинальный список журнала изменения параметров</param>
        /// <returns>Преобразованный список изменения параметров</returns>
        public static ObservableCollection<TimLogParamRecItem> MapLogParameter(List<LogParamRecordItem> logParamRecords)
        {
            if (logParamRecords == null)
                return null;

            ObservableCollection<TimLogParamRecItem> timLogParamRecords = new ObservableCollection<TimLogParamRecItem>();

            int numberCounter = 0;

            foreach (var lpr in logParamRecords)
            {
                numberCounter++;

                ParameterItem p = lpr.ParamWithNewValue;

                string dateTimeString = lpr.DateAndTime.ToString(Constants.DATE_TIME_FORMAT_STRING);
                string name = p.Index + " " + p.Name;
                string valueString = p.Value.ToString() +
                    p.ValueDescription.Unit;


                if (p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Enum ||
                    p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.List ||
                    p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Union)
                    valueString += " (код)";

                List<TimParameterFieldItem> timFields = 
                    ParameterMapper.MapParameterValueFields((int)p.Value, p.ValueDescription.ValueType,
                    p.ValueDescription.Fields);

                timLogParamRecords.Add(new TimLogParamRecItem(numberCounter,
                    dateTimeString, name, valueString, timFields));
            }

            return timLogParamRecords;
        }
    }
}
