using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using TimBrowser.Bluetooth.Codes;
using System.Windows;
using TimBrowser.Bluetooth;
using System.Collections.ObjectModel;
using TimBrowser.Mapper;

namespace TimBrowser.ViewModels
{
    // Main
    public partial class BluetoothViewModel : DownloadCommBase
    {

    #if DESIGN_TIME

        public BluetoothViewModel() :this(null,null)
        {
            if (Execute.InDesignMode)
            {
                ConnectButtonVisibility = Visibility.Visible;
                DeviceListVisibility = Visibility.Visible;
                DiscoverButtonVisibility = Visibility.Visible;
                IsConnectButtonEnabled = true;
                IsDiscoverButtonEnabled = true;
                InfoText = "Выполните поиск устройств";
                DiscoverButtonContent = "Поиск устройств";
                ConnectButtonContent = "Соединение с устройством";

                BluetoothDevices = new ObservableCollection<BluetoothDevice>()
                {
                    new BluetoothDevice("BUR-T-13000", false,false,false),
                    new BluetoothDevice("BUR-М-13000", false,false,false),
                    new BluetoothDevice("Nokia 3310", false,false,false),
                };
            }
        }

    #endif

        public BluetoothViewModel(BluetoothAuthViewModel bluetoothAuthViewModelObj,
            IWindowManager windowManager)
            : base()
        {
            _windowManager = windowManager;
            _bluetoothAuthViewModelObj = bluetoothAuthViewModelObj;
        }

        #region Fields

        private readonly IWindowManager _windowManager;

        private Bluetooth.Driver _bluetoothDriver;

        private BluetoothAuthViewModel _bluetoothAuthViewModelObj;
        private BluetoothDevice _selectedBluetoothDevice;
        private ObservableCollection<BluetoothDevice> _bluetoothDevices;

        #endregion

        #region Methods

        public override void Activate()
        {
            if (_bluetoothDriver == null)
            {
                _bluetoothDriver = new Bluetooth.Driver();
                _bluetoothDriver.DiscoverCompleted = DiscoverDevicesCompleted;
                _bluetoothDriver.OnError += BluetoothDriver_OnError;
                _bluetoothDriver.OnStateChanged += BluetoothDriver_OnStateChanged;
            }

            BluetoothDevices = new ObservableCollection<BluetoothDevice>();

            _bluetoothAuthViewModelObj.OnAuthSuccess = AuthSuccess;
            _bluetoothAuthViewModelObj.OnAuthCancel = AuthCancel;

            StateBehaviour(_bluetoothDriver.CurrentState.Id);

        }

        public override void Deactivate()
        {
            _bluetoothDriver.CloseClient();
        }

        public void DiscoverDevicesButton()
        {
            _bluetoothDriver.Discover();
        }

        public void ConnectDeviceButton()
        {
            if (_bluetoothDriver.CanDisconnect)
                _bluetoothDriver.Disconnect();
            else
            {
                if (_selectedBluetoothDevice.IsAuthenticated)
                {
                    _bluetoothDriver.Connect(_selectedBluetoothDevice);

                    if (_bluetoothDriver.CanConnect)
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
            _bluetoothAuthViewModelObj.ActivateView(_bluetoothDriver, _selectedBluetoothDevice);
        }

        private void AuthSuccess()
        {
            ConnectDeviceButton();
        }

        private void AuthCancel()
        {
            _bluetoothDriver.CancelAuth();
        }

        private void DiscoverDevicesCompleted()
        {
            BluetoothDevices = BluetoothDevicesMapper.Map(_bluetoothDriver.DiscoveredDevices);
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

        public ObservableCollection<BluetoothDevice> BluetoothDevices
        {
            get { return _bluetoothDevices; }
            set
            {
                _bluetoothDevices = value;
                NotifyOfPropertyChange("BluetoothDevices");
            }
        }


        public BluetoothDevice SelectedBluetoothDevice
        {
            get { return _selectedBluetoothDevice; }
            set
            {
                if (_selectedBluetoothDevice != value)
                {
                    _selectedBluetoothDevice = value;
                    NotifyOfPropertyChange("SelectedBluetoothDevice");

                    DeviceSelectedBehaviour(_selectedBluetoothDevice != null);
                }
            }
        }

        public override DataCore.Communication.ITimCommunication Communication
        {
            get
            {
                return _bluetoothDriver.Communication;
            }
        }

        public override DataCore.Communication.ICommunicationSource CommunicationM
        {
            get
            {
                return null;
            }
        }

        /*
        public DownloadInformationModule(ITimCommunication communication)
         */

        #endregion

    }
}
