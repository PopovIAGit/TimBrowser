using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TimBrowser.Bluetooth.Codes;
using TimBrowser.Bluetooth.States;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.Helpers;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace TimBrowser.BLE
{
    public class BLEDriver
    {
        static string _aqsAllBLEDevices = "(System.Devices.Aep.ProtocolId:=\"{bb7bb05e-5972-42b5-94fc-76eaa7084d49}\")";
        static string[] _requestedBLEProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.Bluetooth.Le.IsConnectable", };
        static String GuuiServis = "000000ff-0000-1000-8000-00805f9b34fb";
         

        public BLEDriver()
        {
            _devices = new List<BluetoothLEDevice>();
            _watcher = new BluetoothLEAdvertisementWatcher();
          //  _deviceWatcher = DeviceInformation.CreateWatcher(_aqsAllBLEDevices, _requestedBLEProperties, DeviceInformationKind.AssociationEndpoint);
        }

        private IDriverState _currentState;
        private IBLECommunication _communication;
        private List<BluetoothLEDevice> _devices;
        private BluetoothLEDevice _device;
        private DeviceWatcher _deviceWatcher;
        private BluetoothLEAdvertisementWatcher _watcher;
        private String _passWord;

        public String PassWord
        {
            set { _passWord = value; }
        }
     

        public List<BluetoothLEDevice> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                
            }
        }



        public async void Discover()
        {
           // var 1
            /*  string bluetoothLEDeviceSelector = BluetoothLEDevice.GetDeviceSelector();
             DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(bluetoothLEDeviceSelector);
             foreach (DeviceInformation deviceInfo in devices)
             {
                 BluetoothLEDevice bleDevice = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);
                 Devices.Add(bleDevice);
             }*/

            // var 2
            _devices.Clear();
            if (_watcher == null)
                _watcher = new BluetoothLEAdvertisementWatcher();
            _watcher.Stop();
            _watcher.SignalStrengthFilter.InRangeThresholdInDBm = -110;
            _watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -110;
            _watcher.ScanningMode = BluetoothLEScanningMode.Active;
            _watcher.Received += Watcher_Received;
            _watcher.Start();

            

        }

        private async void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            _device = await BluetoothLEDevice.FromBluetoothAddressAsync(args.BluetoothAddress);
            if (_device != null)
            {
                _devices.Add(_device);
            }
        }
        /// <summary>
        /// Получаем 
        /// </summary>
        public async void Connect(BluetoothLEDevice index)
        {
            _device =  await BluetoothLEDevice.FromIdAsync(index.BluetoothDeviceId.Id);
            if (_device != null) 
            {
                // var servisesResult = await _device.GetGattServicesForUuidAsync(Guid.Parse(GuuiServis));

                if (_device.DeviceInformation.Pairing.IsPaired)
                {
                    // Устройство уже спарено, выполняем подключение
                    await Connect1();
                }
                else
                {
                    await Connect1();
                }
            }
        }

        private async Task Connect1()
        {
            // Здесь выполняем необходимые действия для подключения к устройству
            // Например, инициализируем GATT-сервисы и характеристики для обмена данными

            // Пример: получение сервиса по его UUID
            var serviceResult = await _device.GetGattServicesAsync();
            if (serviceResult.Status == GattCommunicationStatus.Success)
            {
                var service = serviceResult.Services.FirstOrDefault();

                // Пример: получение характеристики по ее UUID
                var characteristicResult = await service.GetCharacteristicsAsync();
                if (characteristicResult.Status == GattCommunicationStatus.Success)
                {
                    var characteristic = characteristicResult.Characteristics.FirstOrDefault();

                    // Далее можно выполнять операции чтения/записи значений характеристики
                }
                else
                {
                    throw new Exception("Не удалось получить характеристику.");
                }
            }
            else
            {
                throw new Exception("Не удалось получить сервис.");
            }

        }

        public async void Disconnect()
        { 
            if (_device != null)
            {
                _device.Dispose();
                _device = null;
                _devices.Clear();
                GC.Collect();
            }
        }
        
        /// <summary>
        /// останавливаем гляделку
        /// </summary>
        public void StopWatcher()
        {
            _watcher.Stop();
        }


        public IDriverState CurrentState
        {
            get { return _currentState; }
        }

        public bool CanDiscover
        {
            get;
            private set;
        }

        public bool CanConnect
        {
            get;
            private set;
        }

        public bool CanDisconnect
        {
            get;
            private set;
        }

        public IBLECommunication Communication
        {
            get { return _communication; }
        }

    }
}
