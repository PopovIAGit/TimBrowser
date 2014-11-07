using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InTheHand.Net;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.States
{
    public class AuthorizationState : DriverStateBase
    {
        public AuthorizationState(StateId id, Driver driver)
            : base (id,driver)
        {

        }

        private bool _isAuthorizationInProgress = false;

        public override void Authorize(BluetoothDevice bluetootDevice, string pinCode,
            Action<bool> onComplete)
        {
            if (_isAuthorizationInProgress)
                return;

            _isAuthorizationInProgress = true;

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
                {
                    try
                    {
                        e.Result = AuthRequest(bluetootDevice.Address, pinCode);
                    }
                    catch (Exception)
                    {
                        ReportError(new Events.DriverErrorEventArgs(DriverErrCode.ConnectError));
                    }
                };

            worker.RunWorkerCompleted += (s, e) =>
                {
                    bool result = (bool)e.Result;

                    bluetootDevice.SetAuthorization(result);

                    if (result)
                        Driver.SetState(StateId.Authorized);

                    if (onComplete != null)
                        onComplete(result);

                    _isAuthorizationInProgress = false;


                };

            worker.RunWorkerAsync();
        }

        private bool AuthRequest(BluetoothAddress bluetoothAddr, string pinCode)
        {
            if (String.IsNullOrEmpty(pinCode))
                return false;

            return
            InTheHand.Net.Bluetooth.BluetoothSecurity.PairRequest(bluetoothAddr, pinCode);
        }
    }
}
