using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Helpers;

namespace TimBrowser.DataCore.Model.Device
{
    public class DeviceLogInfo
    {
        public DeviceLogInfo(LogTypes type, int logIndex, int address, int recsCnt, int cellCnt,
            List<CellField> cellFields)
        {
            _type = type;
            _logIndex = logIndex;
            _address = address;
            _recsCnt = recsCnt;
            _cellsCnt = cellCnt;
            _cellFields = cellFields;
        }

        #region Fields

        private LogTypes _type;                        // Тип журнала
        private int _logIndex;                         // Индекс текущего типа журнала в массиве
        private int _address;                          // Текущий адрес
        private int _recsCnt;                          // Количество записей
        private int _cellsCnt;                         // Количество ячеек в одной записи
        private List<CellField> _cellFields;           // Ячейки

        #endregion

        #region Properties

        /// <summary>
        /// Тип журнала
        /// </summary>
        public LogTypes Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Индекс текущего типа журнала в массиве считанных данных с устройства
        /// </summary>
        public int Index
        {
            get { return _logIndex; }
        }

        /// <summary>
        /// Текущий адрес журнала
        /// </summary>
        public int Address
        {
            get { return _address; }
        }

        /// <summary>
        /// Количество записей журнала
        /// </summary>
        public int RecsCnt
        {
            get { return _recsCnt; }
        }

        /// <summary>
        /// Количество ячеек в одной записи
        /// </summary>
        public int CellsCnt
        {
            get { return _cellsCnt; }
        }

        /// <summary>
        /// Ячейки текущей записи
        /// </summary>
        public List<CellField> CellFields
        {
            get { return _cellFields; }
        }

        #endregion

        public class CellField
        {
            public CellField(int[] paramAddress)
            {
                _paramAddress = paramAddress;
            }

            private int[] _paramAddress;

            public int[] ParamAddress
            {
                get { return _paramAddress; }
            }

        }
    }
}
