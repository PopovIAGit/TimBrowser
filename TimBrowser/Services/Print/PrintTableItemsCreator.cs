using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using TimBrowser.DataCore.Model;
using TimBrowser.Model;

namespace TimBrowser.Services.Print
{
    public class PrintTableItemsCreator
    {
        public PrintTableItemsCreator()
        {

        }

        public const string SET_SYMBOL = "\u2714";

        public PrintTableItem CreateEvLogPrintTable(InformationModuleData informationModule, string mainTitle, string subTitle)
        {
            if (informationModule.DeviceLogs.EventLog != null)
            {
                if (informationModule.DeviceLogs.EventLog.Count > 0)
                {
                    int columnsCount = 4;

                    // создаем заголовки
                    string[] columnsHeader = new string[columnsCount];
                    string[] columnsHeader2 = new string[1];
                    string[] columnsHeader3 = new string[1];
                    columnsHeader2[0] = mainTitle;
                    columnsHeader3[0] = subTitle;

                    columnsHeader[0] = "№";
                    columnsHeader[1] = "Дата и время";
                    columnsHeader[2] = "Наименование";
                    //TODO выставлено 4-ая колонка при выводе журнала 
                    columnsHeader[3] = "Состояние Защиты/Аварии/Неисправности";

                    // Определяем ширину колонок
                    GridLength[] columnsWidths = 
                    {
                        new GridLength(0.1, GridUnitType.Star),
                        new GridLength(0.25, GridUnitType.Star),
                        new GridLength(0.5, GridUnitType.Star),
                        new GridLength(0.15, GridUnitType.Star)
                    };

                    int logEvRecCnt = 0;

                    // создаем колонки с записями
                    List<string[]> columns = new List<string[]>();

                    columns.Add(columnsHeader);
                    //columns.Add(columnsHeader2);
                    //columns.Add(columnsHeader3);

                    foreach (var le in informationModule.DeviceLogs.EventLog)
                    {
                        logEvRecCnt++;

                        string[] currentColum = new string[columnsCount];

                        currentColum[0] = logEvRecCnt.ToString();
                        currentColum[1] = le.LogEventMainCell.DateAndTime.ToString(Helper.Constants.DATE_TIME_FORMAT_STRING);
                        currentColum[2] = le.LogEventMainCell.Id;

                        if (le.LogEventMainCell.Set)
                            currentColum[3] = "Срабатывание";//SET_SYMBOL;
                        else
                            currentColum[3] = "Деблокировка";

                        columns.Add(currentColum);
                    }

                    PrintTableItem printTableItem = new PrintTableItem("Журнал событий", columnsWidths,
                        columns);

                    return printTableItem;
                }
            }


            return null;
        }

        public PrintTableItem CreateCmdLogPrintTable(InformationModuleData informationModule)
        {
            if (informationModule.DeviceLogs.CommandLog != null)
            {
                if (informationModule.DeviceLogs.CommandLog.Count > 0)
                {
                    int columnsCount = 4;

                    //string[] strings = { "№", "Дата и время", "Команда" };

                    // создаем заголовки
                    string[] columnsHeader = new string[columnsCount];
                    columnsHeader[0] = "№";
                    columnsHeader[1] = "Дата и время";
                    columnsHeader[2] = "Команда";
                    columnsHeader[3] = "Источник команд";

                    // Определяем ширину колонок
                    GridLength[] columnsWidths = 
                    {
                        new GridLength(0.15, GridUnitType.Star),
                        new GridLength(0.2, GridUnitType.Star),
                        new GridLength(0.6, GridUnitType.Star),
                        new GridLength(0.6, GridUnitType.Star)
                    };

                    int logCmdRecCnt = 0;

                    // создаем колонки с записями
                    List<string[]> columns = new List<string[]>();

                    columns.Add(columnsHeader);

                    foreach (var lc in informationModule.DeviceLogs.CommandLog)
                    {
                        logCmdRecCnt++;

                        string[] currentColum = new string[columnsCount];

                        currentColum[0] = logCmdRecCnt.ToString();
                        currentColum[1] = lc.DateAndTime.ToString(Helper.Constants.DATE_TIME_FORMAT_STRING);

                        var field = lc.Command.ValueDescription.Fields
                            .Where(f => f.BitValue == ((int)Convert.ToInt32(lc.Command.DValue) & 0x000F)).FirstOrDefault();

                        

                        var fieldSource = lc.Command.ValueDescription.Fields
                            .Where(f => f.BitValue == ((int)Convert.ToInt32(lc.Command.DValue) & 0xFE00)).FirstOrDefault();

                        if (fieldSource != null)
                        {
                            currentColum[3] = fieldSource.Description;
                        }

                        if (field != null)
                        {
                            currentColum[2] = field.Description;
                        }
                        else
                            continue;

                        columns.Add(currentColum);
                    }

                    PrintTableItem printTableItem = new PrintTableItem("Журнал команд", columnsWidths,
                        columns);

                    return printTableItem;

                }
            }

            return null;
        }

        public PrintTableItem CreateParamLogPrintTable(InformationModuleData informationModule)
        {
            if (informationModule.DeviceLogs.ChangeParameterLog != null)
            {
                if (informationModule.DeviceLogs.ChangeParameterLog.Count > 0)
                {
                    int columnsCount = 4;

                    // создаем заголовки
                    string[] columnsHeader = new string[columnsCount];
                    columnsHeader[0] = "№";
                    columnsHeader[1] = "Дата и время";
                    columnsHeader[2] = "Наименование";
                    columnsHeader[3] = "Значение";

                    // Определяем ширину колонок
                    GridLength[] columnsWidths = 
                    {
                        new GridLength(0.15, GridUnitType.Star),
                        new GridLength(0.2, GridUnitType.Star),
                        new GridLength(0.5, GridUnitType.Star),
                        new GridLength(0.2, GridUnitType.Star)
                    };

                    int logParamRecCnt = 0;

                    // создаем колонки с записями
                    List<string[]> columns = new List<string[]>();

                    columns.Add(columnsHeader);

                    foreach (var lp in informationModule.DeviceLogs.ChangeParameterLog)
                    {
                        logParamRecCnt++;

                        string[] currentColum = new string[columnsCount];

                        currentColum[0] = logParamRecCnt.ToString();
                        currentColum[1] = lp.DateAndTime.ToString(Helper.Constants.DATE_TIME_FORMAT_STRING);
                        currentColum[2] = lp.ParamWithNewValue.Name;
                        currentColum[3] = lp.ParamWithNewValue.sValue.ToString();

                        if (lp.ParamWithNewValue.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Enum ||
                            lp.ParamWithNewValue.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.List ||
                            lp.ParamWithNewValue.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Union)
                        {
                            currentColum[3] += " (код)";
                        }
                        else
                            currentColum[3] += lp.ParamWithNewValue.ValueDescription.Unit;

                        columns.Add(currentColum);
                    }

                    PrintTableItem printTableItem = new PrintTableItem("Журнал изменения параметров", columnsWidths,
                        columns);

                    return printTableItem;

                }
            }

            return null;
        }

        public PrintTableItem CreateParamListPrintTable(InformationModuleData informationModule, 
            TpeParameters.Helpers.GroupTypes groupType, string groupTitle)
        {
            if (informationModule.CurrentParameters != null)
            {
                if (informationModule.CurrentParameters.Groups.Count > 0)
                {
                    var paramsList = informationModule.CurrentParameters.Groups.Where(
                        g => (g.GroupType == groupType)).FirstOrDefault();

                    if (paramsList != null)
                    {
                        int columnsCount = 3;

                        // создаем заголовки
                        string[] columnsHeader = new string[columnsCount];
                        columnsHeader[0] = "Индекс";
                        columnsHeader[1] = "Наименование";
                        columnsHeader[2] = "Значение";

                        // Определяем ширину колонок
                        GridLength[] columnsWidths = new GridLength[]
                        {
                            new GridLength(0.15, GridUnitType.Star),
                            new GridLength(0.8, GridUnitType.Star),
                            new GridLength(0.2, GridUnitType.Star)
                        };
                        
                        // создаем колонки с записями
                        List<string[]> columns = new List<string[]>();

                        columns.Add(columnsHeader);

                        foreach (var p in paramsList.Parameters)
                        {
     
                            string[] currentColum = new string[columnsCount];

                            currentColum[0] = p.Index;
                            currentColum[1] = p.Name;
                            currentColum[2] = p.sValue.ToString();

                            if (p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Enum ||
                                p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.List ||
                                p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Union)
                            {
                                currentColum[2] += " (код)";      
                            }
                            else if (p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Time)
                            {
                                currentColum[2] = p.sValue;// DataCore.Transform.Utils.GenerateTimeString((int)Convert.ToInt32(p.sValue));
                            }
                            else if (p.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Date)
                            {
                                currentColum[2] = p.sValue;// DataCore.Transform.Utils.GenerateDateString((int)Convert.ToInt32(p.sValue));
                            }
                            else
                                currentColum[2] += p.ValueDescription.Unit;

                            columns.Add(currentColum);
                        }

                        PrintTableItem printTableItem = new PrintTableItem(groupTitle, columnsWidths,
                            columns);

                        return printTableItem;
                    }
                }
            }


            return null;
        }

    }
}
