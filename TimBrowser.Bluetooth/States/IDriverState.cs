using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Events;
using InTheHand.Net;
using System.IO;
using InTheHand.Net.Sockets;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public interface IDriverState
    {
        void Discover(BluetoothClient client,
            Action<List<BluetoothDevice>> onComplete);
        void Connect(BluetoothClient client, BluetoothDevice device,
            Action<Stream> onComplete);
        void Authorize(BluetoothDevice bluetootDevice, string pinCode,
            Action<bool> onComplete);
        void Disconnect();
        StateId Id { get; }
        Driver Driver { get; }

        Action<DriverErrorEventArgs> OnError { get; set; }
    }
}
