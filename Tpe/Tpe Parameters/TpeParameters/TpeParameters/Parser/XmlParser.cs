using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using TpeParameters.Model;
using TpeParameters.Events;
using TpeParameters.Helpers;
using System.Xml;

namespace TpeParameters.Parser
{
    public class TpeXmlParser
    {
        #region Attribute Names

        public const string TableAttribute = "Table";
        public const string GroupAttribute = "Group";
        public const string ParameterAttribute = "Parameter";

        public const string DeviceIdAttribute = "DeviceId";
        public const string DeviceNameAttribute = "DeviceName";
        public const string FirmwareVersionAttribute = "FirmwareVersion";
        public const string NameAttribute = "Name";
        public const string TypeAttribute = "Type";
        public const string DescriptionAttribute = "Description";
        public const string FieldSpecialDescriptionAttribute = "SpecialDescription";
        public const string IndexAttribute = "Index";
        public const string AddressAttribute = "Address";
        public const string VariableAttribute = "Variable";

        public const string ConfigurationAttribute = "Configuration";
        public const string CanEditAttribute = "CanEdit";
        public const string ChosenAttribute = "Chosen";
        public const string IsLogAttribute = "IsLog";
        public const string AppointmentAttribute = "Appointment";

        public const string ValueDescriptionAttribute = "ValueDescription";
        public const string UnitAttribute = "Unit";
        public const string MinimumAttribute = "Minimum";
        public const string MaximumAttribute = "Maximum";
        public const string DefaultAttribute = "Default";
        public const string CoefficientAttribute = "Coefficient";
        public const string FieldsAttribute = "Fields";
        public const string FieldAttribute = "Field";
        public const string BitValueAttribute = "BitValue";
        public const string InfoAttribute = "Info";
        public const string CommentAttibute = "Comment";

        #endregion

        public void ParseAsync(XmlDocument xmlDocucment, Action<TableItem> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                TableItem table = null;

                try
                {
                    table = Parse(xmlDocucment);
                }
                catch
                { }

                e.Result = table;
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                TableItem table = null;

                if (e.Result != null)
                {
                    table = (TableItem)e.Result;
                }

                if (onComplete != null)
                    onComplete(table);
            };

            worker.RunWorkerAsync();
        }



        public TableItem Parse(XmlDocument xmlDocucment)
        {
            XElement mainXmlEl = XElement.Load(new XmlNodeReader(xmlDocucment));

            // Считываем таблицу
            /*
            var xmlTable = (from el in mainXmlEl.Descendants(TableAttribute) select el).FirstOrDefault();
            */

            var xmlTable = mainXmlEl;

            if (xmlTable == null)
                throw new TpeXmlException(TpeXmlErrorCodes.TableNotFound);

            string deviceIdStr = xmlTable.Attribute(DeviceIdAttribute).Value;
            string deviceName = xmlTable.Attribute(DeviceNameAttribute).Value;
            string firmwareVersionStr = xmlTable.Attribute(FirmwareVersionAttribute).Value;

            int tableId = 1;                                                    // одна таблица
            int deivceId = deviceIdStr.ConvertStrToInt();
            double firmwareVersion = firmwareVersionStr.ConvertStrToInt();

            var xmlGroups = from el in xmlTable.Descendants(GroupAttribute)
                            select el;

            int groupIdCounter = 0;             // счетчик групп  
            int parameterIdCounter = 0;         // счетчик параметров

            List<GroupItem> tableGroups = new List<GroupItem>();

            #region Group

            foreach (var gr in xmlGroups)
            {
                groupIdCounter++;

                string groupName = gr.Attribute(NameAttribute).Value;
                string groupTypeStr = gr.Attribute(TypeAttribute).Value;
                string groupDescription = gr.Attribute(DescriptionAttribute).Value;

                GroupTypes groupType = groupTypeStr.GetGroupTypeFromStr();

                List<ParameterItem> groupParameters = new List<ParameterItem>();
                
                #region Parameter

                var xmlParameters = from el in gr.Descendants(ParameterAttribute)
                                    select el;

                ParameterConfiguration parameterConfiguration = null;
                ParameterValueDescription parameterValueDescription = null;
                ParameterInfo parameterInfo = null;

                foreach (var parameter in xmlParameters)
                {
                    parameterIdCounter++;

                    string parameterIndex = parameter.Attribute(IndexAttribute).Value;
                    string parameterName = parameter.Attribute(NameAttribute).Value;
                    string parameterAddressStr = parameter.Element(AddressAttribute).Value;
                    int parameterAddress = parameterAddressStr.ConvertStrToInt();
                    string parameterVariable = Utils.GetElementValueString(parameter.Element(VariableAttribute));
                    double parameterDValue = 0;
                    String parameterValue = "0";

                    #region Configuration

                    var configuration = parameter.Descendants(ConfigurationAttribute).First();

                    string paramTypeStr = configuration.Element(TypeAttribute).Value;
                    ParamTypes paramType = Utils.GetParamTypeFromString(paramTypeStr);
                    bool isChosen = Utils.GetElementValueBool(configuration.Element(ChosenAttribute));

                    var appointmentNode = configuration.Element(AppointmentAttribute);
                    string appointmentStr = String.Empty;

                    if (appointmentNode != null)
                        appointmentStr = appointmentNode.Value;

                    ParamAppointments paramAppointments = Utils.GetParamAppointmentFromString(appointmentStr);

                    bool canEdit = Utils.GetElementValueBool(configuration.Element(CanEditAttribute));
                    bool isLog = Utils.GetElementValueBool(configuration.Element(IsLogAttribute));
                    parameterConfiguration = new ParameterConfiguration(paramType, paramAppointments, canEdit, isChosen);

                    #endregion

                    #region ValueDescription

                    var valueDescription = parameter.Descendants(ValueDescriptionAttribute).First();

                    string parameterValueUnit = Utils.GetElementValueString(valueDescription.Element(UnitAttribute));
                    double parameterValueMinimum = Utils.GetElementValueDouble(valueDescription.Element(MinimumAttribute));
                    double parameterValueMaximum = Utils.GetElementValueDouble(valueDescription.Element(MaximumAttribute), 65535);
                    double parameterValueDefault = Utils.GetElementValueDouble(valueDescription.Element(DefaultAttribute));
                    double parameterValueCoefficient = Utils.GetElementValueDouble(valueDescription.Element(CoefficientAttribute), 1);

                    string parameterValueTypeString = valueDescription.Element(TypeAttribute).Value;
                    ParamValueTypes parameterValueType = Utils.GetParamValueTypeFromString(parameterValueTypeString);

                    List<ParameterFieldItem> paramValueFields = null;

                    #region Parameter Value Fields

                    if (parameterValueType == ParamValueTypes.List || parameterValueType == ParamValueTypes.Enum ||
                        parameterValueType == ParamValueTypes.Union)
                    {
                        var fieldsNode = valueDescription.Descendants(FieldsAttribute).FirstOrDefault();

                        if (fieldsNode != null)
                        {
                            paramValueFields = new List<ParameterFieldItem>();

                            var fields = fieldsNode.Descendants(FieldAttribute);

                            foreach (var f in fields)
                            {
                                string fieldBitValueString = f.Attribute(BitValueAttribute).Value;
                                int fieldBitValue = fieldBitValueString.ConvertStrToInt();

                                string fieldDescription = f.Attribute(DescriptionAttribute).Value;

                                string fieldSpecialDescription = String.Empty;

                                if (f.Attribute(FieldSpecialDescriptionAttribute) != null)
                                    fieldSpecialDescription = f.Attribute(FieldSpecialDescriptionAttribute).Value;

                                paramValueFields.Add(new ParameterFieldItem(fieldBitValue, fieldDescription,
                                    fieldSpecialDescription));
                            }
                        }
                    }

                    #endregion

                    parameterValueDescription = new ParameterValueDescription(parameterValueMinimum,
                        parameterValueMaximum, parameterValueDefault, parameterValueUnit,
                        parameterValueCoefficient, parameterValueType, paramValueFields);

                    #endregion

                    #region Info

                    var info = parameter.Descendants(InfoAttribute).First();

                    string paramInfoDescription = Utils.GetElementValueString(info.Element(DescriptionAttribute));
                    string paramInfoComment = Utils.GetElementValueString(info.Element(CommentAttibute));

                    parameterInfo = new ParameterInfo(paramInfoDescription, paramInfoComment);

                    #endregion

                    ParameterItem groupParameterItem = new ParameterItem(parameterIdCounter,
                        parameterIndex, parameterName, parameterAddress, parameterVariable,
                        parameterDValue, parameterValue, parameterConfiguration, parameterValueDescription,
                        parameterInfo);

                    groupParameters.Add(groupParameterItem);

                }

                #endregion

                GroupItem tableGroupItem = new GroupItem(groupIdCounter, groupName, groupType,
                    groupDescription, groupParameters);

                tableGroups.Add(tableGroupItem);

            }

            #endregion

            TableItem table = new TableItem(tableId, deivceId, deviceName, firmwareVersion,
                tableGroups, null);

            return table;

        }
    }
}
