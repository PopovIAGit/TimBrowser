using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TpeParameters.Helpers
{
    public static class Utils
    {
        internal static int ConvertStrToInt(this string value)
        {
            int convertedValue = 0;

            if (String.IsNullOrEmpty(value))
                return 0;

            try
            {
                convertedValue = int.Parse(value);
            }
            catch (OverflowException)
            {
                //SendXmlParserMessage(DataManagerRes.XmlAttributeOverflow, true);
            }
            catch (Exception)
            {
                //SendXmlParserMessage(DataManagerRes.XmlParserError, true);
            }

            return convertedValue;
        }


        // В зависимости от региональных настроек использовать 0.1 или 0,1
        internal static double ConvertStrToDouble(this string value)
        {
            double convertedValue = 0;

            if (String.IsNullOrEmpty(value))
                return 0;

            try
            {
                convertedValue = double.Parse(value);
            }
            catch (OverflowException)
            {
                //SendXmlParserMessage(DataManagerRes.XmlAttributeOverflow, true);
            }
            catch (Exception)
            {
                //SendXmlParserMessage(DataManagerRes.XmlParserError, true);
            }

            return convertedValue;
        }

        internal static GroupTypes GetGroupTypeFromStr(this string typeStr)
        {
            GroupTypes grouptype = GroupTypes.None;

            switch (typeStr)
            {
                case "Show": grouptype = GroupTypes.Show;
                    break;
                case "User": grouptype = GroupTypes.User;
                    break;
                case "Factory": grouptype = GroupTypes.Factory;
                    break;
                case "Command": grouptype = GroupTypes.Command;
                    break;
                case "Hide": grouptype = GroupTypes.Hide;
                    break;
            }

            return grouptype;

        }

        internal static ParamTypes GetParamTypeFromString(string paramTypeStr)
        {
            ParamTypes paramType = ParamTypes.None;

            switch (paramTypeStr)
            {
                case "Public": paramType = ParamTypes.Public;
                    break;
                case "Factory": paramType = ParamTypes.Factory;
                    break;
                case "Reserved": paramType = ParamTypes.Reserved;
                    break;
                case "Hide": paramType = ParamTypes.Hide;
                    break;

            }

            return paramType;
        }

        internal static ParamValueTypes GetParamValueTypeFromString(string paramValueTypeStr)
        {
            ParamValueTypes paramValueType = ParamValueTypes.None;

            switch (paramValueTypeStr)
            {
                case "Uns": paramValueType = ParamValueTypes.Uns;
                    break;
                case "Int": paramValueType = ParamValueTypes.Int;
                    break;
                case "Bin": paramValueType = ParamValueTypes.Bin;
                    break;
                case "Union": paramValueType = ParamValueTypes.Union;
                    break;
                case "List": paramValueType = ParamValueTypes.List;
                    break;
                case "Enum": paramValueType = ParamValueTypes.Enum;
                    break;
                case "Date": paramValueType = ParamValueTypes.Date;
                    break;
                case "Time": paramValueType = ParamValueTypes.Time;
                    break;
            }

            return paramValueType;
        }

        internal static ParamAppointments GetParamAppointmentFromString(string paramAppointmentsTypeStr)
        {
            ParamAppointments paramAppointments = ParamAppointments.Regular;

            switch (paramAppointmentsTypeStr)
            {
                case "Regular": paramAppointments = ParamAppointments.Regular;
                    break;
                case "ProductYear": paramAppointments = ParamAppointments.ProductYear;
                    break;
                case "FactoryNumber": paramAppointments = ParamAppointments.FactoryNumber;
                    break;
                case "Fault": paramAppointments = ParamAppointments.Fault;
                    break;
                case "Date": paramAppointments = ParamAppointments.Date;
                    break;
                case "Time": paramAppointments = ParamAppointments.Time;
                    break;
                case "Seconds": paramAppointments = ParamAppointments.Seconds;
                    break;
                case "Status": paramAppointments = ParamAppointments.Status;
                    break;
                case "StatusDigOut": paramAppointments = ParamAppointments.StatusDigOut;
                    break;
                case "LogCmdControlWord": paramAppointments = ParamAppointments.LogCmdControlWord;
                    break;
                case "Position": paramAppointments = ParamAppointments.Position;
                    break;
            }

            return paramAppointments;
        }



        internal static string GetElementValueString(XElement element, string defaultValue = "")
        {
            string value = defaultValue;

            if (element != null)
                return element.Value;

            return value;
        }

        internal static int GetElementValueInt(XElement element, int defaultValue = 0)
        {
            int value = defaultValue;

            if (element != null)
                return element.Value.ConvertStrToInt();

            return value;
        }

        internal static double GetElementValueDouble(XElement element, double defaultValue = 0)
        {
            double value = defaultValue;

            if (element != null)
            {
                if (element.Value.Contains("-"))
                {

                }

                return element.Value.ConvertStrToDouble();
            }

            return value;
        }

        internal static bool GetElementValueBool(XElement element, bool defaultValue = false)
        {
            bool value = defaultValue;

            if (element != null)
            {
                string strValue = element.Value;

                if (strValue == "1" || strValue == "true" || strValue == "True")
                    return true;
            }

            return value;
        }



    }
}
