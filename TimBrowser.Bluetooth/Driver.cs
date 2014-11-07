using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.States;
using TimBrowser.Bluetooth.Codes;
using InTheHand.Net.Sockets;
using TimBrowser.Bluetooth.Events;
using System.IO;
using TimBrowser.DataCore.Communication;
using System.Reflection;

namespace TimBrowser.Bluetooth
{
    public class Driver
    {
        public Driver()
        {
            BuildLibraries();

            SetState(StateId.Initial);
        }

        private IDriverState _currentState;
        private BluetoothClient _client;
        private List<BluetoothDevice> _discoveredDevices;
        private Stream _peerStream;
        private ITimCommunication _communication;

        private void BuildLibraries()
        {
            string resource1 = "TimBrowser.Bluetooth.InTheHand.Net.Personal.dll";
            string dll1 = "InTheHand.Net.Personal.dll";

            AppDomain.CurrentDomain.AssemblyResolve += 
                new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            EmbeddedAssembly.Load(resource1, dll1);
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        public void SetState(StateId stateId)
        {
            _currentState = Helper.DriverStateFactory.GetDriverState(stateId, this);
            _currentState.OnError = ReportError;

            CanDiscover = (stateId == StateId.Initial || stateId == StateId.Discovered);
            CanConnect = (stateId == StateId.Discovered);
            CanDisconnect = (stateId == StateId.Connected);

            ReportStateChanged(new DriverStateChangeArgs(stateId));
            
        }

        public void Discover()
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
        }

        public void Connect(BluetoothDevice device)
        {
            if (!CheckClient())
                return;

            _currentState.Connect(_client, device, (stream) =>
                {
                    _peerStream = stream;
                    _communication = new BluetoothCommunication(_peerStream);
                    _communication.SetConnected(true);
                });
        }

        public void AuthRequest(BluetoothDevice device, string pinCode,
            Action<bool> onComplete)
        {
            _currentState.Authorize(device, pinCode, (result) =>
                {
                    if (onComplete != null)
                    {
                        onComplete(result);
                    }
                });
        }

        public void CancelAuth()
        {
            SetState(StateId.Discovered);
        }

        public void Disconnect()
        {
            _currentState.Disconnect();

            if (_communication != null)
                _communication.SetConnected(false);
        }

        public void CloseClient()
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
        }

        private bool CheckClient()
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
        }

        private void ReportError(DriverErrorEventArgs e)
        {
            EventHandler<DriverErrorEventArgs> handler = OnError;

            if (handler != null)
            {
                handler(this, e);
            }
        }


        private void ReportStateChanged(DriverStateChangeArgs e)
        {
            EventHandler<DriverStateChangeArgs> handler = OnStateChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #region Properties

        public List<BluetoothDevice> DiscoveredDevices
        {
            get { return _discoveredDevices; }
        }

        public Stream PeerStream
        {
            get { return _peerStream; }
        }

        public ITimCommunication Communication
        {
            get { return _communication; }
        }

        public Action DiscoverCompleted { get; set; }

        public event EventHandler<DriverStateChangeArgs> OnStateChanged;
        public event EventHandler<DriverErrorEventArgs> OnError;

        public IDriverState CurrentState
        {
            get { return _currentState; }
        }

        public bool CanDiscover
        {
            get;
            private set;
        }

        public bool CanConnect
        {
            get;
            private set;
        }

        public bool CanDisconnect
        {
            get;
            private set;
        }

        #endregion



    }
}
