using Modbus;
using Modbus.Device;
using Modbus.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TimBrowser.DataCore.Communication;

namespace TimBrowser.ApiImplementation.Communication
{
    public class ModbusProtocolCommunicationFunc : IProtocolCommunicationFunc
    {
        public ModbusProtocolCommunicationFunc(IStreamResource streamResource)
        {
            _modbusMaster = ModbusSerialMaster.CreateRtu(streamResource);

            _modbusMaster.Transport.Retries = 0;
            _modbusMaster.Transport.WaitToRetryMilliseconds = 120;// 120;//220
            _modbusMaster.Transport.ReadTimeout = 150;// 150;
            _modbusMaster.Transport.WriteTimeout = 150;// 150;
        }

        private const int MAX_DATA_COUNT = 220;
        private const int MB_DELAY = 1;
        private IModbusSerialMaster _modbusMaster;

        private const int MAX_REPEAT = 0;
        
        public int _isMaxRepeat;

        private bool _isBusy;
        private bool _isError;

        public ushort[] ReadBytes(int deviceAddr, int startAddr, int count)
        {
            _isError = false;

            string errMessage = String.Empty;

            ushort[] modbusData = new ushort[0];

            _isBusy = true;

            //DelayFunc();


            try
            {
                modbusData = _modbusMaster.ReadHoldingRegisters((byte)deviceAddr, (ushort)startAddr, (ushort)count);//(ushort)startAddr
                _isMaxRepeat = 0;
            }
            catch (SlaveException slaveException)
            {
                if (_isMaxRepeat > 5) {errMessage = slaveException.Message;} else { _isMaxRepeat++; } //ma
            }
            catch (Exception ex)
            {
                if (_isMaxRepeat > 5) {errMessage = ex.Message; } else {_isMaxRepeat++; } //modbusData = _modbusMaster.ReadHoldingRegisters((byte)deviceAddr, (ushort)startAddr, (ushort)count);  } //ma
            }

            if (!String.IsNullOrEmpty(errMessage))
            {
                RaiseCommErrorEvent(errMessage);
            }

            _isBusy = false;

            return modbusData;
        }

        public void WriteBytes(int deviceAddr, int startAddr, ushort[] buffer)
        {
            if (buffer.Length == 0)
                return;

            _isError = false;

            _isBusy = true;

            //DelayFunc();

            string errMessage = String.Empty;

            try
            {
                _modbusMaster.WriteMultipleRegisters((byte)deviceAddr, (ushort)startAddr, buffer);
            }
            catch (SlaveException slaveException)
            {
                errMessage = slaveException.Message;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }

            if (!String.IsNullOrEmpty(errMessage))
            {
                RaiseCommErrorEvent(errMessage);
            }

            _isBusy = false;
        }

        private void DelayFunc()
        {
            Thread.Sleep(MB_DELAY);
        }

        private void RaiseCommErrorEvent(string message)
        {
            _isError = true;

            if (CommErrorAction != null)
                CommErrorAction(message);
        }

        public int MaxCount
        {
            get { return MAX_DATA_COUNT; }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
        }

        public bool IsError
        {
            get { return _isError; }
        }

        public Action<string> CommErrorAction { get; set; }
    }
}
