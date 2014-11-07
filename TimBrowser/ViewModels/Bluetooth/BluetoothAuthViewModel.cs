﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using TimBrowser.Views;
using System.Dynamic;
using System.Windows;
using TimBrowser.Bluetooth;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TimBrowser.ViewModels
{
    public partial class BluetoothAuthViewModel : Screen
    {
        public BluetoothAuthViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            _titleMessage = "Введите пин код";
        }

        #region Fields

        private IWindowManager _windowManager;
        private Bluetooth.Driver _bluetoothDriver;
        private BluetoothDevice _currentDevice;

        private BluetoothAuthView _view;
        private string _pinCode;
        private bool _isAuthControlsEnabled;
        private bool _isAuthRequestBusy;
        private string _titleMessage;

        #endregion

        #region Methods

        public void ViewLoaded(BluetoothAuthView view)
        {
            _view = view;
        }

        protected override void OnActivate()
        {
            IsAuthControlsEnabled = true;
        }

        public void ActivateView(Bluetooth.Driver bluetoothDriver,
            BluetoothDevice currentDevice)
        {
            _bluetoothDriver = bluetoothDriver;
            _currentDevice = currentDevice;

            dynamic settings = new ExpandoObject();

            settings.WindowStartupLocation = WindowStartupLocation.Manual;
            settings.SizeToContent = SizeToContent.Manual;
            settings.ResizeMode = System.Windows.ResizeMode.CanResize;
            settings.WindowStyle = WindowStyle.None;
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/appicon.png"));

            settings.Width = 300;
            settings.Height = 200;

            _windowManager.ShowDialog(this, null, settings);
        }

        public void AuthRequestButton()
        {
            if (_bluetoothDriver == null)
                throw new Exception("Bluetooth driver is null in auth");

            if (!CheckPinCodeString())
                return;

            IsAuthControlsEnabled = false;

            IsAuthRequestBusy = true;

            _bluetoothDriver.AuthRequest(_currentDevice, _pinCode, (success) =>
            {
                IsAuthRequestBusy = false;

                if (success)
                {
                    if (OnAuthSuccess != null)
                        OnAuthSuccess();

                    AuthCancelButton();

                    TitleMessage = "Введите пин код";
                }
                else
                {
                    TitleMessage = "Не удалось провести авторизацию";
                    IsAuthControlsEnabled = true;
                }
            });
        }

        public void PinCodeTextInput()
        {
            _view.PinCodeBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private bool CheckPinCodeString()
        {
            if (String.IsNullOrEmpty(_pinCode))
            {
                _view.PinCodeBorder.BorderBrush = new SolidColorBrush(Colors.Red);

                return false;
            }

            return true;
        }

        public void AuthCancelButton()
        {
            if (OnAuthCancel != null)
                OnAuthCancel();

            TryClose();
        }

        #endregion

        #region Properties

        public string PinCode
        {
            get { return _pinCode; }
            set
            {
                _pinCode = value;
                NotifyOfPropertyChange("PinCode");
            }
        }

        public bool IsAuthRequestBusy
        {
            get { return _isAuthRequestBusy; }
            set
            {
                _isAuthRequestBusy = value;
                NotifyOfPropertyChange("IsAuthRequestBusy");
            }
        }

        public bool IsAuthControlsEnabled
        {
            get { return _isAuthControlsEnabled; }
            set
            {
                _isAuthControlsEnabled = value;
                NotifyOfPropertyChange("IsAuthControlsEnabled");
            }
        }



        public string TitleMessage
        {
            get { return _titleMessage; }
            set
            {
                _titleMessage = value;
                NotifyOfPropertyChange("TitleMessage");
            }
        }


        public System.Action OnAuthSuccess { get; set; }
        public System.Action OnAuthCancel { get; set; }

        #endregion

    }
}
