using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net;

namespace TimBrowser.Bluetooth
{
    public class BluetoothDevice
    {
        private BluetoothAddress _address;
        private string _name;
        private bool _isAuthenticated;
        private bool _isConnected;
        private bool _isRemembered;
        private bool _wasConnectError;


        /// <summary>
        /// Design-time constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="authenticated"></param>
        /// <param name="connected"></param>
        /// <param name="remembered"></param>
        public BluetoothDevice(string name, bool authenticated, 
            bool connected, bool remembered)
        {
            _name = name;
            _isAuthenticated = authenticated;
            _isConnected = connected;
            _isRemembered = remembered;
        }

        public BluetoothDevice(BluetoothAddress address, string name, bool authenticated, 
            bool connected, bool remembered)
        {
            _address = address;
            _name = name;
            _isAuthenticated = authenticated;
            _isConnected = connected;
            _isRemembered = remembered;
        }

        internal void SetAuthorization(bool authenticated)
        {
            _isAuthenticated = authenticated;
        }

        internal void SetConnected(bool connected)
        {
            _isConnected = connected;
        }

        internal void SetConnectErr(bool connectErr)
        {
            _wasConnectError = connectErr;
        }

        #region Properties

        public BluetoothAddress Address
        {
            get { return _address; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
        }

        public bool IsRemembered
        {
            get { return _isRemembered; }
        }

        public bool WasConnectError
        {
            get { return _wasConnectError; }
        }
    }

        #endregion
}
