using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Model;
using TpeParameters.Model;
using TimBrowser.DataCore.Model.Logs;
using TimBrowser.Helper;
using System.Collections.ObjectModel;


namespace TimBrowser.Mapper
{
    public static class ParameterMapper
    {
        /// <summary>
        /// Преобразует основной список параметров журнала событий
        /// </summary>
        /// <param name="logEventRecord">Одна конкретная запись журнала событий</param>
        /// <returns>Преобразованный список параметров</returns>
        public static List<TimParameterItem> MapLogEvParameters(LogEventRecordItem logEventRecord)
        {
            List<TimParameterItem> logEvTimParameters = new List<TimParameterItem>();

            int numberCounter = 0;

            string mainDateTimeString = logEventRecord.LogEventMainCell.
                DateAndTime.ToString(Constants.DATE_TIME_FORMAT_STRING);

            List<TimParameterItem> mainParameter = new List<TimParameterItem>();
            List<TimParameterItem> bufParameter = new List<TimParameterItem>();

            foreach (var mainPar in logEventRecord.LogEventMainCell.Parameters)
            {
                numberCounter++;

                string index = mainPar.Index;
                string name = mainPar.Name;

                string valueString = mainPar.Value.ToString() + mainPar.ValueDescription.Unit;

                if (mainPar.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Enum ||
                    mainPar.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.List ||
                    mainPar.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Union)
                {
                    valueString = mainPar.Value.ToString() + " (код)";
                }

                List<TimParameterFieldItem> timFields =
                    MapParameterValueFields((int)mainPar.Value, mainPar.ValueDescription.ValueType,
                    mainPar.ValueDescription.Fields);

                logEvTimParameters.Add(new TimParameterItem(numberCounter,
                    index, mainDateTimeString, name, valueString, timFields));

                foreach (var logEvBufRec in logEventRecord.LogEventBufferCells)
                {
                    var parBuf = logEvBufRec.Parameters.Where(p => p.Id == mainPar.Id).FirstOrDefault();

                    if (parBuf != null)
                    {
                        numberCounter++;

                        string logEvBufDateTimeString = logEvBufRec.DateAndTime.
                            ToString(Constants.DATE_TIME_FORMAT_STRING);

                        string bufParIndex = parBuf.Index;
                        string bufName = parBuf.Name;
                        string bufValueString = parBuf.Value + parBuf.ValueDescription.Unit;


                        if (parBuf.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Enum ||
                            parBuf.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.List ||
                            parBuf.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Union)
                        {
                            bufValueString = parBuf.Value.ToString() + " (код)";
                        }

                        List<TimParameterFieldItem> bufTimFields =
                            MapParameterValueFields((int)parBuf.Value, parBuf.ValueDescription.ValueType,
                            parBuf.ValueDescription.Fields);

                        logEvTimParameters.Add(new TimParameterItem(numberCounter,
                            bufParIndex, logEvBufDateTimeString, bufName, bufValueString, bufTimFields));
                    }
                }

                numberCounter = 0;

                /*
                logEvTimParameters.Add(new TimParameterItem(numberCounter,
                    index, mainDateTimeString, name, valueString, timFields));
                 * */
            }

            /*
            foreach (var mainPar in logEventRecord.LogEventMainCell.Parameters)
            {
                numberCounter++;

                string index = mainPar.Index;                
                string name = mainPar.Name;
                string valueString = mainPar.Value.ToString() + mainPar.ValueDescription.Unit;

                List<TimParameterFieldItem> timFields =
                    MapParameterValueFields((int)mainPar.Value, mainPar.ValueDescription.Fields);

                logEvTimParameters.Add(new TimParameterItem(numberCounter,
                    index, mainDateTimeString, name, valueString, timFields));
            }

            foreach (var logEvBufRec in logEventRecord.LogEventBufferCells)
            {
                string logEvBufDateTimeString = logEvBufRec.DateAndTime.
                    ToString(Constants.DATE_TIME_FORMAT_STRING);

                foreach (var bufPar in logEvBufRec.Parameters)
                {
                    string index = bufPar.Index;
                    string name = bufPar.Name;
                    string valueString = bufPar.Value.ToString() + bufPar.ValueDescription.Unit;

                    List<TimParameterFieldItem> timFields =
                        MapParameterValueFields((int)bufPar.Value, bufPar.ValueDescription.Fields);

                    logEvTimParameters.Add(new TimParameterItem(numberCounter,
                    index, logEvBufDateTimeString, name, valueString, timFields));
                }
            }
            */

            return logEvTimParameters;
        }

        public static ObservableCollection<TimChosenParameterItem> MapLogEvChosenParametersCol(LogEventRecordItem logEventRecord)
        {
            ObservableCollection<TimChosenParameterItem> logEvTimChosenParametersCol = new ObservableCollection<TimChosenParameterItem>();
            
            // счетчик должен инкрементироваться при смене типа параметра!
            int numberCounter = 0;

            string mainDateTimeString = logEventRecord.LogEventMainCell.
                DateAndTime.ToString(Constants.DATE_TIME_FORMAT_STRING);

            var chMainPars = from chp in logEventRecord.LogEventMainCell.Parameters
                             where chp.Configuration.IsChosen
                             select chp;

            foreach (var mainP in chMainPars)
            {
                numberCounter++;

                string index = mainP.Index;
                string name = index + " " + mainP.Name;
                string valueString = mainP.Value.ToString() + mainP.ValueDescription.Unit;

                List<TimParameterFieldItem> timFields =
                    MapParameterValueFields((int)mainP.Value, mainP.ValueDescription.ValueType,
                    mainP.ValueDescription.Fields);

                List<TimParameterItem> chosenParametersList = new List<TimParameterItem>();

                chosenParametersList.Add(new TimParameterItem(numberCounter,
                    index, mainDateTimeString, name, valueString, timFields));

                foreach (var logEvBufRec in logEventRecord.LogEventBufferCells)
                {
                    var bufPar = logEvBufRec.Parameters.Where(p => p.Id == mainP.Id).FirstOrDefault();

                    if (bufPar != null)
                    {
                        numberCounter++;

                        string logEvBufDateTimeString = logEvBufRec.DateAndTime.
                            ToString(Constants.DATE_TIME_FORMAT_STRING);

                        string bufParindex = bufPar.Index;
                        string bufParName = bufPar.Name;
                        string bufParValueString = bufPar.Value.ToString() + bufPar.ValueDescription.Unit;

                        List<TimParameterFieldItem> bufTimFields =
                              MapParameterValueFields((int)bufPar.Value, bufPar.ValueDescription.ValueType,
                              bufPar.ValueDescription.Fields);

                        chosenParametersList.Add(new TimParameterItem(numberCounter,
                            bufParindex, logEvBufDateTimeString, bufParName, bufParValueString, bufTimFields));
                    }
                }

                numberCounter = 0;

                logEvTimChosenParametersCol.Add(new TimChosenParameterItem(name, chosenParametersList));
            }

            return logEvTimChosenParametersCol;
        }

        /// <summary>
        /// Преобразует список избранных параметров журнала событий
        /// </summary>
        /// <param name="logEventRecord">Одна конкретная запись журнала событий</param>
        /// <returns>Преобразованный список избранных параметров</returns>
        public static ObservableCollection<TimParameterItem> MapLogEvChosenParameters(LogEventRecordItem logEventRecord)
        {
            ObservableCollection<TimParameterItem> logEvTimChosenParameters = 
                new ObservableCollection<TimParameterItem>();

            // счетчик должен инкрементироваться при смене типа параметра!
            int numberCounter = 0;

            string mainDateTimeString = logEventRecord.LogEventMainCell.
                DateAndTime.ToString(Constants.DATE_TIME_FORMAT_STRING);

            var chMainPars = from chp in logEventRecord.LogEventMainCell.Parameters
                             where chp.Configuration.IsChosen
                             select chp;

            foreach (var mainP in chMainPars)
            {
                numberCounter++;

                string index = mainP.Index;
                string name = mainP.Name;
                string valueString = mainP.Value.ToString() + mainP.ValueDescription.Unit;

                List<TimParameterFieldItem> timFields =
                    MapParameterValueFields((int)mainP.Value, mainP.ValueDescription.ValueType,
                    mainP.ValueDescription.Fields);

                logEvTimChosenParameters.Add(new TimParameterItem(numberCounter,
                    index, mainDateTimeString, name, valueString, timFields));

                foreach (var logEvBufRec in logEventRecord.LogEventBufferCells)
                {
                    var bufPar = logEvBufRec.Parameters.Where(p => p.Id == mainP.Id).FirstOrDefault();

                    if (bufPar != null)
                    {
                        numberCounter++;

                        string logEvBufDateTimeString = logEvBufRec.DateAndTime.
                            ToString(Constants.DATE_TIME_FORMAT_STRING);

                        string bufParindex = bufPar.Index;
                        string bufParName = bufPar.Name;
                        string bufParValueString = bufPar.Value.ToString() + " " + bufPar.ValueDescription.Unit;

                        List<TimParameterFieldItem> bufTimFields =
                              MapParameterValueFields((int)bufPar.Value, bufPar.ValueDescription.ValueType,
                              bufPar.ValueDescription.Fields);

                        logEvTimChosenParameters.Add(new TimParameterItem(numberCounter,
                            bufParindex, logEvBufDateTimeString, bufParName, bufParValueString, bufTimFields));
                    }
                }

                numberCounter = 0;
            }

            /*
            foreach (var logEvBufRec in logEventRecord.LogEventBufferCells)
            {
                string logEvBufDateTimeString = logEvBufRec.DateAndTime.
                    ToString(Constants.DATE_TIME_FORMAT_STRING);

                var chBufPars =  from chp in logEvBufRec.Parameters
                                 where chp.Configuration.IsChosen
                                 select chp;

                foreach (var bufP in chBufPars)
                {
                    string index = bufP.Index;
                    string name = bufP.Name;
                    string valueString = bufP.Value.ToString() + bufP.ValueDescription.Unit;
                    
                    List<TimParameterFieldItem> timFields =
                          MapParameterValueFields((int)bufP.Value, bufP.ValueDescription.Fields);

                    logEvTimChosenParameters.Add(new TimParameterItem(numberCounter,
                        index, mainDateTimeString, name, valueString, timFields));
                }
            }
            */

            return logEvTimChosenParameters;
        }
        
        /// <summary>
        /// Преобразует список группированных параметров для отображения на ViewModel
        /// </summary>
        /// <param name="groupItems">Оригинальный список группированных параметров</param>
        /// <returns>Преобразованный список группированных параметров</returns>
        public static ObservableCollection<TimParametersGroup> MapGroupsOfParameters(List<GroupItem> groupItems)
        {
            if (groupItems == null)
                return null;

            ObservableCollection<TimParametersGroup> timGroups = new ObservableCollection<TimParametersGroup>();

            int numberCounter = 0;

            foreach (var g in groupItems)
            {
                if (g.GroupType == TpeParameters.Helpers.GroupTypes.User ||
                    g.GroupType == TpeParameters.Helpers.GroupTypes.Factory ||
                    g.GroupType == TpeParameters.Helpers.GroupTypes.Hide)
                {
                    string groupName = g.Name;
                    string groupDescription = g.Description;

                    List<TimParameterItem> groupParameters = new List<TimParameterItem>();

                    numberCounter = 0;

                    foreach (var p in g.Parameters)
                    {
                        numberCounter++;
                        groupParameters.Add(MapParameter(numberCounter, p));
                    }

                    timGroups.Add(new TimParametersGroup(groupName, groupDescription,
                        groupParameters));
                }
            }

            return timGroups;
        }

        public static TimParameterItem MapParameter(int number, ParameterItem parameter, DateTime? dateTime = null)
        {
            string dateTimeString = String.Empty;

            if (dateTime != null)
            {
                dateTimeString = dateTime.Value.ToString(Constants.DATE_TIME_FORMAT_STRING);
            }

            string index = parameter.Index;
            string name = index + " " + parameter.Name;
            string valueString = String.Empty;

            if (parameter.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Time)
            {
                valueString = DataCore.Transform.Utils.GenerateTimeString((int)parameter.Value);
            }
            else if (parameter.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Date)
            {
                valueString = DataCore.Transform.Utils.GenerateDateString((int)parameter.Value);
            }
            else
            {
                valueString = parameter.Value.ToString() + " " + parameter.ValueDescription.Unit;
            }

            if (parameter.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Enum ||
                parameter.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.List ||
                parameter.ValueDescription.ValueType == TpeParameters.Helpers.ParamValueTypes.Union)
                valueString += " (код)";

            List<TimParameterFieldItem> timFields = 
                MapParameterValueFields((int)parameter.Value, parameter.ValueDescription.ValueType,
                parameter.ValueDescription.Fields);

            TimParameterItem timParameter = new TimParameterItem(number,
                index, dateTimeString, name, valueString, timFields);

            return timParameter;
        }

        /// <summary>
        /// Преобразует битовые поля или enum значеия для отображения
        /// </summary>
        /// <param name="value">Значение параметра</param>
        /// <param name="parameterFieldItems">Поля параметра</param>
        /// <returns>преобразованные поля</returns>
        public static List<TimParameterFieldItem> MapParameterValueFields(int value, TpeParameters.Helpers.ParamValueTypes valueType,
            List<ParameterFieldItem> parameterFieldItems)
        {
            if (parameterFieldItems == null)
                return null;

            List<TimParameterFieldItem> timFields = new List<TimParameterFieldItem>();

            foreach (var pf in parameterFieldItems)
            {
                int bitValue = pf.BitValue;
                bool isSet = false;

                if (valueType == TpeParameters.Helpers.ParamValueTypes.Union ||
                    valueType == TpeParameters.Helpers.ParamValueTypes.List)
                    isSet = ((value >> (bitValue - 1)) & 1) == 1;
                else if (valueType == TpeParameters.Helpers.ParamValueTypes.Enum)
                    isSet = (pf.BitValue == value);

                string description = pf.Description;

                timFields.Add(new TimParameterFieldItem(bitValue, isSet, description));
            }

            return timFields;

        }

    }
}
