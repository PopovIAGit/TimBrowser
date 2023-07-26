using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TpeParameters.Model;
using System.Xml;

namespace TpeParameters.Parser
{
    public class XmlBuilder
    {
        public XmlDocument BuildTpeXmlTable(TableItem tableItem)
        {
            if (tableItem == null)
                return null;

            XmlDocument xmlDoc;
            XmlNode xmlDeclaration;

            xmlDoc = new XmlDocument();

            xmlDeclaration = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmlDoc.AppendChild(xmlDeclaration);

            XmlElement tableRoot = xmlDoc.CreateElement("Table");
            xmlDoc.AppendChild(tableRoot);

            XmlAttribute deviceIdAttribute = xmlDoc.CreateAttribute("DeviceId");
            deviceIdAttribute.Value = tableItem.DeviceId.ToString();

            XmlAttribute deviceNameAttribute = xmlDoc.CreateAttribute("DeviceName");
            deviceNameAttribute.Value = tableItem.DeviceName;

            XmlAttribute firmwareVersionAttribute = xmlDoc.CreateAttribute("FirmwareVersion");
            firmwareVersionAttribute.Value = tableItem.FirmwareVersion.ToString();

            tableRoot.Attributes.Append(deviceIdAttribute);
            tableRoot.Attributes.Append(deviceNameAttribute);
            tableRoot.Attributes.Append(firmwareVersionAttribute);

            #region Group

            foreach (var group in tableItem.Groups)
            {
                XmlElement groupRoot = xmlDoc.CreateElement("Group");
                tableRoot.AppendChild(groupRoot);

                XmlAttribute groupNameAttribute = xmlDoc.CreateAttribute("Name");
                groupNameAttribute.Value = group.Name;

                XmlAttribute groupTypeAttribute = xmlDoc.CreateAttribute("Type");
                groupTypeAttribute.Value = group.GroupType.ToString();

                XmlAttribute groupDescriptionAttribute = xmlDoc.CreateAttribute("Description");
                groupDescriptionAttribute.Value = group.Description;

                groupRoot.Attributes.Append(groupNameAttribute);
                groupRoot.Attributes.Append(groupTypeAttribute);
                groupRoot.Attributes.Append(groupDescriptionAttribute);

                #region Parameters

                foreach (var param in group.Parameters)
                {
                    //<Parameter Index="A0" Name="Статус работы">
                    XmlElement paramRoot = xmlDoc.CreateElement("Parameter");
                    groupRoot.AppendChild(paramRoot);

                    XmlAttribute paramIndexAttribute = xmlDoc.CreateAttribute("Index");
                    paramIndexAttribute.Value = param.Index;

                    XmlAttribute paramNameAttribute = xmlDoc.CreateAttribute("Name");
                    paramNameAttribute.Value = param.Name;

                    paramRoot.Attributes.Append(paramIndexAttribute);
                    paramRoot.Attributes.Append(paramNameAttribute);

                    XmlElement addressElement = xmlDoc.CreateElement("Address");
                    addressElement.InnerText = param.Address.ToString();

                    paramRoot.AppendChild(addressElement);


                    if (param.VariableName != String.Empty)
                    {
                        XmlElement variableElement = xmlDoc.CreateElement("Variable");
                        variableElement.InnerText = param.VariableName;

                        paramRoot.AppendChild(variableElement);
                    }

                    #region Configuration

                    XmlElement configurationElement = xmlDoc.CreateElement("Configuration");

                    paramRoot.AppendChild(configurationElement);

                    XmlElement configurationTypeElement = xmlDoc.CreateElement("Type");
                    configurationTypeElement.InnerText = param.Configuration.ParamType.ToString();

                    XmlElement configurationAppointmentElement = xmlDoc.CreateElement("Appointment");
                    configurationAppointmentElement.InnerText = param.Configuration.Appointment.ToString();

                    configurationElement.AppendChild(configurationTypeElement);
                    configurationElement.AppendChild(configurationAppointmentElement);

                    if (param.Configuration.CanEdit)
                    {
                        XmlElement configurationCanEditElement = xmlDoc.CreateElement("CanEdit");
                        configurationCanEditElement.InnerText = param.Configuration.CanEdit.ToString();
                        configurationElement.AppendChild(configurationCanEditElement);
                    }

                    if (param.Configuration.IsChosen)
                    {
                        XmlElement configurationChosenElement = xmlDoc.CreateElement("Chosen");
                        configurationChosenElement.InnerText = param.Configuration.IsChosen.ToString();
                        configurationElement.AppendChild(configurationChosenElement);
                    }

                    #endregion

                    #region Value Description

                    XmlElement valueDescriptionElement = xmlDoc.CreateElement("ValueDescription");
                    paramRoot.AppendChild(valueDescriptionElement);

                    XmlElement valueMinumumElement = xmlDoc.CreateElement("Minimum");                    
                    valueMinumumElement.InnerText = param.ValueDescription.Minimum.ToString();                    

                    XmlElement valueMaximumElement = xmlDoc.CreateElement("Maximum");
                    valueMaximumElement.InnerText = param.ValueDescription.Maximum.ToString();

                    XmlElement valueDefaultElement = xmlDoc.CreateElement("Default");
                    valueDefaultElement.InnerText = param.ValueDescription.Default.ToString();

                    XmlElement valueTypeElement = xmlDoc.CreateElement("Type");
                    valueTypeElement.InnerText = param.ValueDescription.ValueType.ToString();

                    valueDescriptionElement.AppendChild(valueMinumumElement);
                    valueDescriptionElement.AppendChild(valueMaximumElement);
                    valueDescriptionElement.AppendChild(valueDefaultElement);
                    valueDescriptionElement.AppendChild(valueTypeElement);

                    if (param.ValueDescription.Unit != String.Empty)
                    {
                        XmlElement valueUnitElement = xmlDoc.CreateElement("Unit");
                        valueUnitElement.InnerText = param.ValueDescription.Unit;
                        valueDescriptionElement.AppendChild(valueUnitElement);
                    }

                    if (param.ValueDescription.Coefficient != 1)
                    {
                        XmlElement valueCoefficientElement = xmlDoc.CreateElement("Coefficient");
                        valueCoefficientElement.InnerText = param.ValueDescription.Coefficient.ToString();
                        valueDescriptionElement.AppendChild(valueCoefficientElement);
                    }

                    #region Bit Fields

                    if (param.ValueDescription.Fields != null)
                    {
                        if (param.ValueDescription.Fields.Count > 0)
                        {
                            XmlElement valueFieldsElement = xmlDoc.CreateElement("Fields");
                            valueDescriptionElement.AppendChild(valueFieldsElement);

                            foreach (var field in param.ValueDescription.Fields)
                            {
                                XmlElement fieldElement = xmlDoc.CreateElement("Field");
                                valueFieldsElement.AppendChild(fieldElement);

                                XmlAttribute fieldBitValueAttribute = xmlDoc.CreateAttribute("BitValue");
                                fieldBitValueAttribute.Value = field.BitValue.ToString();

                                XmlAttribute fieldDescriptionAttribute = xmlDoc.CreateAttribute("Description");
                                fieldDescriptionAttribute.Value = field.Description;

                                fieldElement.Attributes.Append(fieldBitValueAttribute);
                                fieldElement.Attributes.Append(fieldDescriptionAttribute);

                                if (field.SpecialDescription != String.Empty)
                                {
                                    XmlAttribute fieldSpecialDescriptionAttribute = xmlDoc.CreateAttribute("SpecialDescription");
                                    fieldSpecialDescriptionAttribute.Value = field.SpecialDescription;

                                    fieldElement.Attributes.Append(fieldSpecialDescriptionAttribute);
                                }
                            }
                        }
                    }



                    #endregion

                    #endregion

                    #region Info

                    XmlElement infoElement = xmlDoc.CreateElement("Info");
                    paramRoot.AppendChild(infoElement);

                    XmlElement infoDescriptionElement = xmlDoc.CreateElement("Description");

                    if (param.Info.Description != String.Empty)
                    {
                        infoDescriptionElement.InnerText = param.Info.Description;
                    }

                    infoElement.AppendChild(infoDescriptionElement);

                    if (param.Info.Comment != String.Empty)
                    {
                        XmlElement infoCommentElement = xmlDoc.CreateElement("Comment");
                        infoCommentElement.InnerText = param.Info.Comment;

                        infoElement.AppendChild(infoCommentElement);
                    }

                    #endregion

                }

                #endregion

            }

            #endregion

            return xmlDoc;
        }

    }
}
