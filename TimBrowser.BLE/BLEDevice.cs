using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement

namespace TimBrowser.BLE
{
    public class BLEDevice
    {

        private string _name;
        private bool _isAuthenticated;
        private bool _isConnected;
        private bool _isRemembered;
        private bool _wasConnectError;


        public BLEDevice() 
            {
        
            }
    }
}
