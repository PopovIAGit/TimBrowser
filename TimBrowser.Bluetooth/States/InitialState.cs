using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class InitialState : DriverStateBase
    {
        public InitialState(StateId id, Driver driver)
            : base(id, driver)
        {

        }

        public override void Discover(InTheHand.Net.Sockets.BluetoothClient client, Action<List<BluetoothDevice>> onComplete)
        {
            Driver.SetState(StateId.Discovering);
            Driver.Discover();
        }




    }
}
