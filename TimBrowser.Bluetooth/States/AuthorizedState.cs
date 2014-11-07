using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class AuthorizedState : DriverStateBase
    {
        public AuthorizedState(StateId id, Driver driver)
            : base (id,driver)
        {

        }


        public override void Connect(InTheHand.Net.Sockets.BluetoothClient client, BluetoothDevice device, Action<System.IO.Stream> onComplete)
        {
            Driver.SetState(StateId.Connecting);
            Driver.Connect(device);
        }

    }
}
