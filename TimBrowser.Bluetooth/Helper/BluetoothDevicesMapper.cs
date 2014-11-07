using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net.Sockets;

namespace TimBrowser.Bluetooth.Helper
{
    public class BluetoothDevicesMapper
    {
        public static List<BluetoothDevice> Map(BluetoothDeviceInfo[] peers)
        {
            List<BluetoothDevice> btDevices = new List<BluetoothDevice>();

            foreach (var item in peers)
            {
                BluetoothDevice dev = new BluetoothDevice(
                    item.DeviceAddress,item.DeviceName,
                    item.Authenticated, item.Connected, item.Remembered);

                btDevices.Add(dev);
            }

            return btDevices;
        }
    }
}
