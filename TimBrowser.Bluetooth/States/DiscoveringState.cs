using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;
using InTheHand.Net.Sockets;
using System.ComponentModel;

namespace TimBrowser.Bluetooth.States
{
    public class DiscoveringState : DriverStateBase
    {
        public DiscoveringState(StateId id, Driver driver)
            : base (id,driver)
        {

        }

        private bool _discoveringInProgress;

        public override void Discover(BluetoothClient client,
            Action<List<BluetoothDevice>> onComplete)
        {
            BluetoothClient c = client;
            BackgroundWorker worker = new BackgroundWorker();

            if (!_discoveringInProgress)
            {
                _discoveringInProgress = true;

                try
                {
                    c.BeginDiscoverDevices(Helper.Constants.MaxDiscoverDevices,
                        true, true, true, false,
                        (result) =>
                        {
                            BluetoothDeviceInfo[] peers = c.EndDiscoverDevices(result);

                            List<BluetoothDevice> btDevices = Helper.BluetoothDevicesMapper.Map(peers);

                            _discoveringInProgress = false;

                            if (onComplete != null)
                                onComplete(btDevices);

                            Driver.SetState(StateId.Discovered);

                        }, null);
                }
                catch (Exception)
                {
                    ReportError(new Events.DriverErrorEventArgs(DriverErrCode.DiscoveringError));

                    Driver.SetState(StateId.Initial);
                }
            }
            else
            {
                ReportError(new Events.DriverErrorEventArgs(DriverErrCode.DiscoveringInProgress));
            }
        }

    }
}
