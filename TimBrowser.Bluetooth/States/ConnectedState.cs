using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class ConnectedState : DriverStateBase
    {
        public ConnectedState(StateId id, Driver driver)
            : base(id, driver)
        {

        }

        public override void Disconnect()
        {
            Driver.SetState(StateId.Disconnecting);
            Driver.Disconnect();
        }


    }
}
