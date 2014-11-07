using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;
using System.ComponentModel;

namespace TimBrowser.Bluetooth.States
{
    public class DisconnectingState : DriverStateBase
    {
        public DisconnectingState(StateId id, Driver driver)
            : base(id, driver)
        {

        }

        public override void Disconnect()
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
                {
                    Driver.CloseClient();
                };

            worker.RunWorkerCompleted += (s, e) =>
                {
                    Driver.SetState(StateId.Disconnected);
                    Driver.Disconnect();
                };

            worker.RunWorkerAsync();

        }
    }
}
