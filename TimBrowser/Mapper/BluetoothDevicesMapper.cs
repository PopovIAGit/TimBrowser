using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TimBrowser.Bluetooth;

namespace TimBrowser.Mapper
{
    public class BluetoothDevicesMapper
    {
        public static ObservableCollection<BluetoothDevice> Map(List<BluetoothDevice> list)
        {
            ObservableCollection<BluetoothDevice> collection = new ObservableCollection<BluetoothDevice>();

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
