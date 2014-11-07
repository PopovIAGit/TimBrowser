using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class DiscoveredState : DriverStateBase
    {
        public DiscoveredState(StateId id, Driver driver)
            : base(id, driver)
        {

        }

        public override void Authorize(BluetoothDevice bluetootDevice, string pinCode, Action<bool> onComplete)
        {
            Driver.SetState(StateId.Authorization);
            Driver.AuthRequest(bluetootDevice, pinCode, onComplete);
        }

        public override void Discover(InTheHand.Net.Sockets.BluetoothClient client, Action<List<BluetoothDevice>> onComplete)
        {
            Driver.SetState(StateId.Discovering);
            Driver.Discover();
        }

        public override void Connect(InTheHand.Net.Sockets.BluetoothClient client, BluetoothDevice device, Action<System.IO.Stream> onComplete)
        {
            Driver.SetState(StateId.Connecting);
            Driver.Connect(device);
        }
    }
}
