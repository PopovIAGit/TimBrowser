using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TimBrowser.Bluetooth;
using Windows.Devices.Bluetooth;

namespace TimBrowser.Mapper
{
    public class BleDevicesMapper
    {
        public static ObservableCollection<BluetoothLEDevice> Map(List<BluetoothLEDevice> list)
        {
            ObservableCollection<BluetoothLEDevice> collection = new ObservableCollection<BluetoothLEDevice>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    collection.Add(item);
                }
            }

            return collection;
        }
    }
}
