using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Helpers;
using TpeParameters.Helpers;
using TpeParameters.Model;
using TimBrowser.DataCore.Model.Device;

namespace TimBrowser.DataCore.Transform
{
    public static class Utils
    {

        internal static ParameterItem FindParameterByAppointment(TableItem paramTable, ParamAppointments paramAppointment)
        {                                                                                               
            ParameterItem parameterItem = null;

            foreach (var g in paramTable.Groups)
            {
                parameterItem = (from p in g.Parameters
                                where p.Configuration.Appointment == paramAppointment
                                select p).FirstOrDefault();

                if (parameterItem != null)
                    return parameterItem;
            }

            return null;
        }

        internal static ParameterItem FindParameterByAddress(int address, TableItem paramTable)
        {
            ParameterItem parameter = null;

            //int tableParamsCount = paramTable.Parameters.Count;
            int tableGoupscount = paramTable.Groups.Count;

            //parameter = paramTable.Parameters.Find(p => p.Address == address);

            if (parameter != null)
                return parameter;

            foreach (var g in paramTable.Groups)
            {
                parameter = g.Parameters.Find(p => p.Address == address);

                if (parameter != null)
                    return parameter;
            }

            return null;
        }

        internal static int GetParamFieldIndex(int paramAddress, int logCell, DeviceLogInfo deviceLogInfo)
        {
            int fieldIndex = Array.FindIndex(deviceLogInfo.CellFields[logCell].ParamAddress,
                f => f == paramAddress);

            return fieldIndex;
        }



        internal static DateTime GenerateLogDateTime(ParameterItem dateParameter, ParameterItem timeParameter, 
            ParameterItem secondsParameter)
        {
            int dateValue = (int)dateParameter.Value;
            int timeValue = (int)timeParameter.Value;

            int day = dateValue & 31;
            int month = (dateValue >> 5) & 15;
            int year = (dateValue >> 9) + 2000;

            if (year > DateTime.Now.Year)
                year = 2000;

            if (day <= 0)   day = 1;
            if (month <= 0) month = 1;

            int minutes = timeValue & 63;
            int hours = timeValue >> 6;

            if (hours > 24)
                hours = 0;

            return new DateTime(year, month, day, hours, minutes, (int)secondsParameter.Value);
        }

        public static string GenerateTimeString(int timeValue)
        {
            int minutes = timeValue & 63;
            int hours = timeValue >> 6;

            string timeString = GetRankStringValue(hours, 2) + ":" + GetRankStringValue(minutes, 2);

            return timeString;
        }

        public static string GenerateDateString(int dateValue)
        {
            int day = dateValue & 31;
            int month = (dateValue >> 5) & 15;
            int year = (dateValue >> 9) + 2000;

            string dateString = GetRankStringValue(day, 2) + "." + GetRankStringValue(month, 2) + "." +
                GetRankStringValue(year, 2);

            return dateString;
        }

        public static string GetRankStringValue(int value, int rank)
        {
            int valueRank = value.ToString().Length;

            int zeroCount = rank - valueRank;

            if (zeroCount <= 0)
                return value.ToString();

            string zeroString = String.Empty;

            for (int i = zeroCount; i > 0; i--)
            {
                zeroString += "0";
            }

            zeroString += value.ToString();

            return zeroString;
        }

        /*
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
         * */

    }
}
