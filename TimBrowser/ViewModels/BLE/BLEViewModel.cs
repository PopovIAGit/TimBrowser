using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimBrowser.DataCore.Communication;
using TimBrowser.Mapper;
using Windows.Devices.Bluetooth;

namespace TimBrowser.ViewModels
{
    public partial class BLEViewModel : DownloadCommBase
    {
        public BLEViewModel(BLEAuthViewModel bleAuthViewModelObj,
           IWindowManager windowManager)
           : base()
        {
            _windowManager = windowManager;
            _bleAuthViewModelObj = bleAuthViewModelObj;
        }

        #region Fields
        private readonly IWindowManager _windowManager;

        private BLE.BLEDriver _bleDriver;

        private BLEAuthViewModel _bleAuthViewModelObj;
        private BluetoothLEDevice _selectedBleDevice;
        private ObservableCollection<BluetoothLEDevice> _bleDevices;

        #endregion

        #region Methods

        public override void Activate()
        {
            if (_bleDriver == null)
            {
                _bleDriver = new BLE.BLEDriver();
              /*  _bleDriver.DiscoverCompleted = DiscoverDevicesCompleted;
                _bleDriver.OnError += BluetoothDriver_OnError;
                _bleDriver.OnStateChanged += BluetoothDriver_OnStateChanged;*/
            }

            BleDevices = new ObservableCollection<BluetoothLEDevice>();

            _bleAuthViewModelObj.OnAuthSuccess = AuthSuccess;
            _bleAuthViewModelObj.OnAuthCancel = AuthCancel;

            StateBehaviour(_bleDriver.CurrentState.Id);

        }

        public override void Deactivate()
        {
            //_bleDriver.CloseClient();
        }

        public void DiscoverDevicesButton()
        {
            _bleDriver.Discover();
        }

        public void ConnectDeviceButton()
        {
            if (_bleDriver.CanDisconnect)
                _bleDriver.Disconnect();
            else
            {
                if (_selectedBleDevice.DeviceInformation.Pairing.CanPair)
                {
                    _bleDriver.Connect(_selectedBleDevice);

                    if (_bleDriver.CanConnect)
                    {

                    }
                }
                else
                {
                    AuthActivate();
                }
            }
        }

        private void AuthActivate()
        {
            _bleAuthViewModelObj.ActivateView(_bleDriver, _selectedBleDevice);
        }

        private void AuthSuccess()
        {
            ConnectDeviceButton();
        }

        private void AuthCancel()
        {
           // _bleDriver.CancelAuth();
        }

        private void DiscoverDevicesCompleted()
        {
            BleDevices = BleDevicesMapper.Map(_bleDriver.Devices);
        }

        private void BluetoothDriver_OnError(object sender, Bluetooth.Events.DriverErrorEventArgs e)
        {
            OnDriverErrorBehaviour(e.ErrorCode);
        }

        private void BluetoothDriver_OnStateChanged(object sender, Bluetooth.Events.DriverStateChangeArgs e)
        {
            StateBehaviour(e.StateId);
        }

        #endregion

        #region Properties

        public ObservableCollection<BluetoothLEDevice> BleDevices
        {
            get { return _bleDevices; }
            set
            {
                _bleDevices = value;
                NotifyOfPropertyChange("BluetoothDevices");
            }
        }


        public BluetoothLEDevice SelectedBleDevice
        {
            get { return _selectedBleDevice; }
            set
            {
                if (_selectedBleDevice != value)
                {
                    _selectedBleDevice = value;
                    NotifyOfPropertyChange("SelectedBluetoothDevice");

                    DeviceSelectedBehaviour(_selectedBleDevice != null);
                }
            }
        }

        public override DataCore.Communication.ITimCommunication Communication
        {
            get
            {
                return null;
            }
        }

        public override DataCore.Communication.ICommunicationSource CommunicationM
        {
            get
            {
                return null;
            }
        }

        public override IBLECommunication CommunicationBLE
        {
            get 
            {
                return _bleDriver.Communication;
            }
        }

        /*
        public DownloadInformationModule(ITimCommunication communication)
         */

        #endregion
    }
}
