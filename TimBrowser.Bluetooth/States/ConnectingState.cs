using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.IO;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class ConnectingState : DriverStateBase
    {
        public ConnectingState(StateId id, Driver driver)
            : base(id, driver)
        {

        }

        public override void Connect(BluetoothClient client, BluetoothDevice device, Action<Stream> onComplete)
        {
            BluetoothEndPoint ep = new BluetoothEndPoint(device.Address, BluetoothService.SerialPort);

            try
            {
                client.BeginConnect(ep, (result) =>
                    {
                        try
                        {
                            client.EndConnect(result);

                            device.SetConnected(client.Connected);
                            device.SetConnectErr(false);

                            Stream peerStream = client.GetStream();

                            if (onComplete != null)
                                onComplete(peerStream);

                            Driver.SetState(StateId.Connected);
                        }
                        catch (Exception)
                        {
                            ConnectError(device);
                        }

                    }, null);
            }
            catch (Exception)
            {
                ConnectError(device);
            }
        }

        public override void Authorize(BluetoothDevice bluetootDevice, string pinCode, Action<bool> onComplete)
        {
            Driver.SetState(StateId.Authorization);
            Driver.AuthRequest(bluetootDevice, pinCode, onComplete);
        }

        private void ConnectError(BluetoothDevice device)
        {
            device.SetConnectErr(true);

            ReportError(new Events.DriverErrorEventArgs(DriverErrCode.ConnectError));

            Driver.SetState(StateId.Discovered);
        }
    }
}
