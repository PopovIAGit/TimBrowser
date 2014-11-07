using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Events;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using System.IO;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class DriverStateBase : IDriverState
    {
        public DriverStateBase(StateId id, Driver driver)
        {
            _id = id;
            _driver = driver;
        }

        private Driver _driver;
        private StateId _id;

        public virtual void Discover(BluetoothClient client,
            Action<List<BluetoothDevice>> onComplete)
        {

        }

        public virtual void Connect(BluetoothClient client, BluetoothDevice device,
            Action<Stream> onComplete)
        {

        }

        public virtual void Authorize(BluetoothDevice bluetootDevice, string pinCode,
            Action<bool> onComplete)
        {

        }

        public virtual void Disconnect()
        {

        }

        public void ReportError(DriverErrorEventArgs e)
        {
            if (OnError != null)
                OnError(e);
        }

        public StateId Id
        {
            get { return _id; }
        }

        public Driver Driver
        {
            get { return _driver; }
        }

        public Action<DriverErrorEventArgs> OnError { get; set; }
    }
}
