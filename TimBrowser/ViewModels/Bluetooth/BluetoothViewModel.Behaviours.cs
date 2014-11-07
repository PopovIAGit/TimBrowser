using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using TimBrowser.Bluetooth.Codes;
using System.Windows;

namespace TimBrowser.ViewModels
{
	// Behaviours
	public partial class BluetoothViewModel : DownloadCommBase
	{

		#region Fields

		private string _discoverButtonContent;
		private string _connectButtonContent;
		private string _infoText;
		private Visibility _deviceListVisibility;
		private Visibility _discoverButtonVisibility;
		private Visibility _connectButtonVisibility;
		private Visibility _bluetoothIconVisibility;
		private bool _isConnectButtonEnabled;
		private bool _isDiscoverButtonEnabled;
		private bool _isBusy;

		private StateId _prevStateId;

		#endregion

		#region Methods

		private void StateBehaviour(StateId stateId)
		{
			switch (stateId)
			{
				case StateId.Initial:

					DiscoverButtonContent = "Поиск устройств";
					SetInfoText("Выполните поиск устройств");

                    IsBusy = false;

					break;

				case StateId.Discovering:

					DiscoverButtonContent = "Поиск устройств";
					SetInfoText("Выполняется поиск устройств...");

                    IsBusy = true;

					break;

				case StateId.Discovered:

					DiscoverButtonContent = "Поиск устройств";
					ConnectButtonContent = "Соединение с устройством";

					if (_prevStateId == StateId.Discovering)
						SetInfoText("Поиск устройств выполнен");
					/*
				else
					SetInfoText(" ");
				*/

                    IsBusy = false;

					break;

				case StateId.Connecting:

					SetInfoText("Соединение с " + _selectedBluetoothDevice.Name);

					break;

				case StateId.Connected:

					SetInfoText("Связь с " + _selectedBluetoothDevice.Name + " установлена");

					ConnectButtonContent = "Разъединить связь";

					RaiseConnectAction(true);

					break;

				case StateId.Disconnected:

					RaiseConnectAction(false);

					break;
			}

			IsDiscoverButtonEnabled = _bluetoothDriver.CanDiscover;
			IsConnectButtonEnabled = _bluetoothDriver.CanConnect || _bluetoothDriver.CanDisconnect;

			if (stateId == StateId.Connecting || stateId == StateId.Connected ||
				stateId == StateId.Disconnecting)
			{
				DiscoverButtonVisibility = Visibility.Collapsed;
			}
			else
			{
				DiscoverButtonVisibility = Visibility.Visible;
			}

            if (stateId == StateId.Initial || stateId == StateId.Discovering)
            {
                ConnectButtonVisibility = Visibility.Collapsed;
            }
            else if (stateId == StateId.Discovered)
                ConnectButtonVisibility = Visibility.Visible;

			if (stateId == StateId.Initial || stateId == StateId.Discovering)
				DeviceListVisibility = Visibility.Collapsed;
			else
				DeviceListVisibility = Visibility.Visible;

            IsBusy = (stateId == StateId.Discovering || stateId == StateId.Connecting);

			if (_bluetoothDevices != null)
			{
				if (_bluetoothDevices.Count > 0)
				{
					DeviceListVisibility = (stateId == StateId.Discovered) ? Visibility.Visible :
						Visibility.Collapsed;
				}
			}

			if (_deviceListVisibility == Visibility.Visible)
				BluetoothIconVisibility = Visibility.Collapsed;
			else
				BluetoothIconVisibility = Visibility.Visible;

			_prevStateId = stateId;
		}

        public override void DownloadBehaviour(bool isDownloading)
        {
            ConnectButtonVisibility = (isDownloading) ? Visibility.Collapsed :
                Visibility.Visible;

            IsBusy = isDownloading;
        }

		private void DeviceSelectedBehaviour(bool isSelected)
		{
			ConnectButtonVisibility = (isSelected) ? Visibility.Visible :
				Visibility.Collapsed;
		}

		private void OnDriverErrorBehaviour(DriverErrCode errCode)
		{
			if (errCode == DriverErrCode.NoErr)
				return;

			string text = "Ошибка: ";

			switch (errCode)
			{
				case DriverErrCode.CannotCreateClient:
					text += "отсутствует или поврежден Bluetooth-адаптер";
					break;

				case DriverErrCode.ConnectError:
					text += "нет связи с устройством " + _selectedBluetoothDevice.Name;
					break;
			}

			SetInfoText(text);
		}

		private void SetInfoText(string text)
		{
			InfoText = text;
		}

		#endregion

		#region Properties

		public override bool IsBusy
		{
            get { return _isBusy; }
			set
			{
                if (_isBusy != value)
				{
                    _isBusy = value;
                    NotifyOfPropertyChange("IsBusy");
				}
			}
		}

		public string DiscoverButtonContent
		{
			get { return _discoverButtonContent; }
			set
			{
				_discoverButtonContent = value;
				NotifyOfPropertyChange("DiscoverButtonContent");
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

		public Visibility DeviceListVisibility
		{
			get { return _deviceListVisibility; }
			set
			{
				if (_deviceListVisibility != value)
				{
					_deviceListVisibility = value;
					NotifyOfPropertyChange("DeviceListVisibility");
				}
			}
		}

		public bool IsDiscoverButtonEnabled
		{
			get { return _isDiscoverButtonEnabled; }
			set
			{
				if (_isDiscoverButtonEnabled != value)
				{
					_isDiscoverButtonEnabled = value;
					NotifyOfPropertyChange("IsDiscoverButtonEnabled");
				}
			}
		}

		public string ConnectButtonContent
		{
			get { return _connectButtonContent; }
			set
			{
				_connectButtonContent = value;
				NotifyOfPropertyChange("ConnectButtonContent");
			}
		}

		public Visibility DiscoverButtonVisibility
		{
			get { return _discoverButtonVisibility; }
			set
			{
				if (_discoverButtonVisibility != value)
				{
					_discoverButtonVisibility = value;
					NotifyOfPropertyChange("DiscoverButtonVisibility");
				}
			}
		}

		public Visibility ConnectButtonVisibility
		{
			get { return _connectButtonVisibility; }
			set
			{
				_connectButtonVisibility = value;
				NotifyOfPropertyChange("ConnectButtonVisibility");
			}
		 }

		public Visibility BluetoothIconVisibility
		{
			get { return _bluetoothIconVisibility; }
			set
			{
				_bluetoothIconVisibility = value;
				NotifyOfPropertyChange("BluetoothIconVisibility");
			}
		}

		public bool IsConnectButtonEnabled
		{
			get { return _isConnectButtonEnabled; }
			set
			{
				_isConnectButtonEnabled = value;
				NotifyOfPropertyChange("IsConnectButtonEnabled");
			}
		}

		#endregion

	}
}
