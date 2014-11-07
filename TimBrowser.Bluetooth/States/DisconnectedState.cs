using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class DisconnectedState : DriverStateBase
    {
        public DisconnectedState(StateId id, Driver driver)
            : base(id, driver)
        {

        }

        public override void Disconnect()
        {
            Driver.SetState(StateId.Discovered);
        }

    }
}
