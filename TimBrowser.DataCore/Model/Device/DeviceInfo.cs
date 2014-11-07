using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Model.Device
{
    public class DeviceInfo
    {
        /*
        private DateTime dateTimeDownload;
        */

        public DeviceInfo(int deviceId, double firmwareVersion, int parametersCount,
            int logsCount, int productYear, int factoryNumber, string deviceNumStr,
            string deviceName)
        {
            _deviceId = deviceId;
            _firmwareVersion = firmwareVersion;
            _parametersCount = parametersCount;
            _logsCount = logsCount;
            _productYear = productYear;
            _factoryNumber = factoryNumber;
            _deviceNumberString = deviceNumStr;
            _deviceName = deviceName;
        }

        #region Fields

        private int _deviceId;
        private double _firmwareVersion;
        private int _parametersCount;
        private int _logsCount;
        private int _productYear;
        private int _factoryNumber;
        private string _deviceNumberString;
        private string _deviceName;

        #endregion

        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        public int DeviceId
        {
            get { return _deviceId; }
        }

        /// <summary>
        /// Версия прошивки
        /// </summary>
        public double FirmwareVersion
        {
            get { return _firmwareVersion; }
        }


        /// <summary>
        /// Количество параметров в устройстве
        /// </summary>
        public int ParametersCount
        {
            get { return _parametersCount; }
        }

        /// <summary>
        /// Количество журналов в устройстве
        /// </summary>
        public int LogsCount
        {
            get { return _logsCount; }
        }

        /// <summary>
        /// Год изготовления устройства
        /// </summary>
        public int ProductYear
        {
            get { return _productYear; }
        }

        /// <summary>
        /// Заводской номер
        /// </summary>
        public int FactoryNumber
        {
            get { return _factoryNumber; }
        }

        /// <summary>
        /// Полный строковый номер устройства
        /// </summary>
        public string DeviceNumberString
        {
            get { return _deviceNumberString; }
        }

        /// <summary>
        /// Наименование устройства
        /// </summary>
        public string DeviceName
        {
            get { return _deviceName; }
        }

    }
}

