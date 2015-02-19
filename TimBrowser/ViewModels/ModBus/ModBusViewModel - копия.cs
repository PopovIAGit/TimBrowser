using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;
using System.IO.Ports;
//using System.Collections.Generic;
using Caliburn.Micro;
using System.Windows.Input;
using TimBrowser.Model;
using Microsoft.Practices.Unity;

namespace TimBrowser.ViewModels
{
    public class ModBusViewModel : DownloadCommBase
    {

        /*
            //_communicationService = containter.Resolve<CommunicationService>();

            LoadSettings();

            //OpenPortCommand = new DelegateCommand(OpenPort);
            //UnloadedCommand = new DelegateCommand(Unloaded);

            _openPortButtonContent = "Открыть порт";
        */

         /*public ModBusViewModel(IWindowManager windowManager)
            : base()
        {
            _windowManager = windowManager;
            LoadSettings();
            //OpenPortCommand = new DelegateCommand(OpenPort);
            //UnloadedCommand = new DelegateCommand(Unloaded);

            _openPortButtonContent = "Открыть порт";
        }

        private readonly IWindowManager _windowManager;

        //private CommunicationService _communicationService;
        private string _selectedComPort;
        private int _selectedBaudRate;
        private MbParity _selectedMbParity;
        private MbStopBits _selectedStopBit;
        private string _openPortButtonContent;

        public ICommand OpenPortCommand { get; private set; }
        public ICommand UnloadedCommand { get; private set; }


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
        }*/

        /// <summary>
        /// открывает или закрывает COM-порт
        /// </summary>
        private void OpenPort()
        {
            /*if (!_communicationService.IsSerialPortOpened)
            {
                if (_communicationService.OpenComPort(_selectedComPort, _selectedBaudRate, _selectedMbParity,
                    _selectedStopBit))
                {
                    OpenPortButtonContent = "Закрыть порт";
                }
            }
            else
            {
                _communicationService.ClosePort();
                OpenPortButtonContent = "Открыть порт";
            }*/
        }

        /*private void Unloaded()
        {
            //_communicationService.ClosePort();
        }

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

        //----------------

        public override bool IsBusy
        {
            get;
            set;
        }

        private ITimCommunication _communication;
        public override ITimCommunication Communication
        {
            get { return _communication; }
        }*/
    }
}
