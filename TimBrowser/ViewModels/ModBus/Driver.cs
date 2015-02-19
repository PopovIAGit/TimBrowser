using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TimBrowser.Bluetooth.States;
//using TimBrowser.Bluetooth.Codes;
//using TimBrowser.Bluetooth.Events;
//using TimBrowser.DataCore.Communication;
using TimBrowser.ApiImplementation.Communication;
//using TimBrowser.ApiImplementation.ILow;
using System.IO;
using System.IO.Ports;
using TimBrowser.Model;
using TimBrowser.DataCore.Communication;
using System.Reflection;

namespace TimBrowser.ViewModels.ModBus
{
    public class Driver
    {
        public Driver()
        {
            //SetState(StateId.Initial);
            //_statusMessage = container.Resolve<IStatusMessage>();
            _communicationSource = new ModbusCommunication();
        }

        public const int ComPortDataBitsCount = 8;
        public ICommunicationSource _communicationSource;
        //public ITimCommunication _communicationSource;
        //private IStatusMessage _statusMessage;

        //private IDriverState _currentState;
        //private BluetoothClient _client;
        //private List<BluetoothDevice> _discoveredDevices;
        //private Stream _peerStream;
        //private ITimCommunication _communication;

        private SerialPort _comPort; //+
        //private Stream _peerStream;

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        /*public void SetState(StateId stateId)
        {
            _currentState = Helper.DriverStateFactory.GetDriverState(stateId, this);
            _currentState.OnError = ReportError;

            CanDiscover = (stateId == StateId.Initial || stateId == StateId.Discovered);
            CanConnect = (stateId == StateId.Discovered);
            CanDisconnect = (stateId == StateId.Connected);

            ReportStateChanged(new DriverStateChangeArgs(stateId));
        }*/

        //+
        public bool OpenComPort(int _deviceAddress, string comPortName, int baudRate, MbParity parity, MbStopBits stopBits)
        {
            if (_comPort == null)
            {
                _communicationSource.DeviceAddress = _deviceAddress;
                _comPort = new SerialPort(comPortName, baudRate, (Parity)parity,
                    ComPortDataBitsCount, (StopBits)stopBits);
            }
            else
            {
                _comPort.PortName = comPortName;
                _comPort.BaudRate = baudRate;
                _comPort.Parity = (Parity)parity;
                _comPort.StopBits = (StopBits)stopBits;
            }

            try
            {
                _comPort.Open();

                IsSerialPortOpened = true;

                var serialLowFunc = new SerialPortLowCommunicationFunc(_comPort);
                var serialMbAdapter = new StreamModbusAdapter(_comPort.BaseStream);

                ModbusProtocolCommunicationFunc modbusComFunc = new ModbusProtocolCommunicationFunc(serialMbAdapter);

                SetLowCommunicationFunc(serialLowFunc);
                SetProtocolCommunicationFunc(modbusComFunc);
            }
            catch (Exception ex)
            {
                _comPort = null;
                IsSerialPortOpened = false;
                return false;
            }

            return true;
        }

        private void SetLowCommunicationFunc(ILowCommunicationFunc lowComFunc)
        {
            _communicationSource.LowCommunicationFunc = lowComFunc;
        }

        private void SetProtocolCommunicationFunc(IProtocolCommunicationFunc protocolFunc)
        {
            _communicationSource.ProtocolCommunicationFunc = protocolFunc;
            _communicationSource.ProtocolCommunicationFunc.CommErrorAction = RaiseCommErrorEvent;
        }

        private void RaiseCommErrorEvent(string errMessage)
        {
            //_statusMessage.SetMessage(errMessage);
            
            /*if (_communicationSource.CommErrorEvent != null)
                _communicationSource.CommErrorEvent(this, new CommErrorEventArgs(errMessage));
            */
        }
        //+
        public void ClosePort()
        {
            if (_comPort != null)
            {
                _comPort.Close();
                _comPort = null;
            }
            IsSerialPortOpened = false;
        }


        /*public void Discover()
        {
            if (!CheckClient())
                return;

            _currentState.Discover(_client,
                (discoveredList) =>
                    {
                        _discoveredDevices = discoveredList;

                        if (DiscoverCompleted != null)
                            DiscoverCompleted();
                    }
                );
        }*/

        /*public void Connect(BluetoothDevice device)
        {
            if (!CheckClient())
                return;

            _currentState.Connect(_client, device, (stream) =>
                {
                    _peerStream = stream;
                    _communication = new BluetoothCommunication(_peerStream);
                    _communication.SetConnected(true);
                });
        }*/

        /*public void Disconnect()
        {
            _currentState.Disconnect();

            if (_communication != null)
                _communication.SetConnected(false);
        }*/

        /*public void CloseClient()
        {
            if (_client != null)
            {
                try
                {
                    SetState(StateId.Initial);
                    _client.Close();
                    _client = null;
                }
                catch (Exception)
                { }
            }
        }*/

        /*private bool CheckClient()
        {
            bool success = true;

            if (_client == null)
            {
                try
                {
                    _client = new BluetoothClient();
                }
                catch
                {
                    success = false;

                    ReportError(new DriverErrorEventArgs(DriverErrCode.CannotCreateClient));
                }
            }

            return success;
        }*/

        /*private void ReportError(DriverErrorEventArgs e)
        {
            EventHandler<DriverErrorEventArgs> handler = OnError;

            if (handler != null)
            {
                handler(this, e);
            }
        }*/


        /*private void ReportStateChanged(DriverStateChangeArgs e)
        {
            EventHandler<DriverStateChangeArgs> handler = OnStateChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }*/

        #region Properties

        /*public List<BluetoothDevice> DiscoveredDevices
        {
            get { return _discoveredDevices; }
        }*/

        /*public Stream PeerStream
        {
            get { return _peerStream; }
        }*/

        /*public ITimCommunication Communication
        {
            get { return _communication; }
        }*/

        public Action DiscoverCompleted { get; set; }

        //public event EventHandler<DriverStateChangeArgs> OnStateChanged;
        //public event EventHandler<DriverErrorEventArgs> OnError;

        /*public IDriverState CurrentState
        {
            get { return _currentState; }
        }*/

        //+
        public bool IsSerialPortOpened { get; private set; }

        #endregion



    }
}
