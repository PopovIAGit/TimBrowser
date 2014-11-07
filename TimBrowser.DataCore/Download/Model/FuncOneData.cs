using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Download.Model
{
    [Serializable]
    public class FuncOneData
    {
        public FuncOneData(int idOfDevice, int parametersNumber,
            int logsNumber, byte[] logsTypes, int firmwareVersion)
        {
            _idOfDevice = idOfDevice;
            _parametersNumber = parametersNumber;
            _logsNumber = logsNumber;
            _logsTypes = logsTypes;
            _firmwareVersion = firmwareVersion;
        }

        private int _idOfDevice;
        private int _parametersNumber;
        private int _logsNumber;
        private byte[] _logsTypes;
        private int _firmwareVersion;

        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        public int IdOfDevice
        {
            get { return _idOfDevice; }
        }

        /// <summary>
        /// Количество параметров
        /// </summary>
        public int ParametersNumber
        {
            get { return _parametersNumber; }
        }

        /// <summary>
        /// Количество журналов
        /// </summary>
        public int LogsNumber
        {
            get { return _logsNumber; }
        }

        /// <summary>
        /// Типы журналов
        /// </summary>
        public byte[] LogsTypes
        {
            get { return _logsTypes; }
        }

        public int FirmwareVersion
        {
            get { return _firmwareVersion; }
        }
    }
}
