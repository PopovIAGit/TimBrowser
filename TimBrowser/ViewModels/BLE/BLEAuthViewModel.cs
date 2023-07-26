using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TimBrowser.ViewModels
{
    public partial class BLEAuthViewModel : Screen
    {
        public BLEAuthViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            _titleMessage = "Введите пин код";
        }

        #region Fields

        private IWindowManager _windowManager;
        private BLE.Driver _BLEDriver;
        private BluetoothDevice _currentDevice;

        private BLEAuthView _view;
        private string _pinCode;
        private bool _isAuthControlsEnabled;
        private bool _isAuthRequestBusy;
        private string _titleMessage;

        #endregion

    }
}
