using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
//using TimBrowser.Bluetooth.Codes;
using TimBrowser.Model;
using System.Windows;
using System.Collections.ObjectModel;
using TimBrowser.Mapper;
//using TimBrowser.ApiImplementation;
//using TimBrowser.ApiImplementation.ILow;
using TimBrowser.DataCore.Communication;

namespace TimBrowser.ViewModels
{
    // Main
    public partial class ModBusViewModel : DownloadCommBase
    {
        public ModBusViewModel(IWindowManager windowManager)
            : base()
        {
            _windowManager = windowManager;
            LoadSettings();
            _openPortButtonContent = "Открыть порт";
            StateBehaviour(0);
        }

        #region Fields

        private readonly IWindowManager _windowManager;
        private ModBus.Driver _modBusDriver;
        private string _infoText;
        //private ModBusDevice _selectedModBusDevice;

        private int _deviceAddress;
        private string _selectedComPort;
        private int _selectedBaudRate;
        private MbParity _selectedMbParity;
        private MbStopBits _selectedStopBit;

        private string _openPortButtonContent;

        #endregion

        #region Methods

        /// <summary>
        /// загружает настройки последовательного порта
        /// </summary>
        private void LoadSettings()
        {
           _selectedComPort = (String.IsNullOrEmpty(Properties.Settings.Default.ComPortName)) ?
                _selectedComPort = SerialPortData.ComPorts[0] :
                _selectedComPort = Properties.Settings.Default.ComPortName;

            _selectedBaudRate = (Properties.Settings.Default.ComBaudRate == -1) ?
                _selectedBaudRate = SerialPortData.BaudRates[0] :
                _selectedBaudRate = Properties.Settings.Default.ComBaudRate;

            _selectedMbParity = (Properties.Settings.Default.ComParity == -1) ?
                MbParity.None : (MbParity)Properties.Settings.Default.ComParity;

            _selectedStopBit = (Properties.Settings.Default.ComStopBits == -1) ?
                MbStopBits.One : (MbStopBits)Properties.Settings.Default.ComStopBits;
            
        }

        /// <summary>
        /// открывает или закрывает COM-порт
        /// </summary>
        private void OpenPort()
        {
            if (!_modBusDriver.IsSerialPortOpened)
            {
                if (_modBusDriver.OpenComPort(_deviceAddress, _selectedComPort, _selectedBaudRate, _selectedMbParity,
                    _selectedStopBit))
                {
                    OpenPortButtonContent = "Закрыть порт";
                    SetInfoText("Соединение Открыто");
                    StateBehaviour(1);
                }
                else
                {
                    SetInfoText("COM порт не открылся. Проверьте настройки.");
                    _modBusDriver.ClosePort();
                    StateBehaviour(0);
                }
            }
            else
            {
                _modBusDriver.ClosePort();
                OpenPortButtonContent = "Открыть порт";
                SetInfoText("Соединение Закрыто");
                StateBehaviour(0);
            }
        }

        public override void Activate()
        {
            SetInfoText("Выполните настройку параметров соединения");
            if (_modBusDriver == null)
            {
                _modBusDriver = new ModBus.Driver();
                //_bluetoothDriver.OnError += BluetoothDriver_OnError;
                //_bluetoothDriver.OnStateChanged += BluetoothDriver_OnStateChanged;
            }

            //BluetoothDevices = new ObservableCollection<BluetoothDevice>();

            //StateBehaviour(_bluetoothDriver.CurrentState.Id);
        }

        public override void Deactivate()
        {
            //_bluetoothDriver.CloseClient();
        }

        public void ConnectDeviceButton()
        {
            OpenPort();
            
            /*if (_bluetoothDriver.CanDisconnect)
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
            }*/
        }


        private void BluetoothDriver_OnError(object sender, Bluetooth.Events.DriverErrorEventArgs e)
        {
            //OnDriverErrorBehaviour(e.ErrorCode);
        }

        private void BluetoothDriver_OnStateChanged(object sender, Bluetooth.Events.DriverStateChangeArgs e)
        {
            //StateBehaviour(e.StateId);
        }

        //+
        private void SetInfoText(string text)
        {
            InfoText = text;
        }
        #endregion

        #region Properties

        public string SelectedComPort
        {
            get { return _selectedComPort; }
            set
            {
                _selectedComPort = value;

                Properties.Settings.Default.ComPortName = _selectedComPort;
                Properties.Settings.Default.Save();

                NotifyOfPropertyChange("SelectedComPort");
            }
        }

        public int SelectedBaudRate
        {
            get { return _selectedBaudRate; }
            set
            {
                _selectedBaudRate = value;

                Properties.Settings.Default.ComBaudRate = _selectedBaudRate;
                Properties.Settings.Default.Save();

                NotifyOfPropertyChange("SelectedBaudRate");
            }
        }


        public MbParity SelectedMbParity
        {
            get { return _selectedMbParity; }
            set
            {
                _selectedMbParity = value;

                Properties.Settings.Default.ComParity = (int)_selectedMbParity;
                Properties.Settings.Default.Save();

                NotifyOfPropertyChange("SelectedMbParity");
            }
        }

        public MbStopBits SelectedStopBit
        {
            get { return _selectedStopBit; }
            set
            {
                _selectedStopBit = value;

                Properties.Settings.Default.ComStopBits = (int)_selectedStopBit;
                Properties.Settings.Default.Save();

                NotifyOfPropertyChange("SelectedStopBit");
            }
        }

        public string OpenPortButtonContent
        {
            get { return _openPortButtonContent; }
            set
            {
                _openPortButtonContent = value;
                NotifyOfPropertyChange("OpenPortButtonContent");
            }
        }

        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                NotifyOfPropertyChange("InfoText");
            }
        }

        public int DeviceAddress
        {
            get { return _deviceAddress; }
            set
            {
                _deviceAddress = value;
                NotifyOfPropertyChange("DeviceAddress");
            }
        }

        /*public override ViewModels.ModBus.Driver Communication
        {
            get
            {
                return _modBusDriver._communicationSource;
                
            }
        }*/

        public override DataCore.Communication.ICommunicationSource CommunicationM
        {
            get
            {
                return _modBusDriver._communicationSource;
            }
        }

        public override DataCore.Communication.ITimCommunication Communication
        {
            get
            {
                return null;
            }
        }

        #endregion

    }
}
