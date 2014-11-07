using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.Events
{
    public class DriverStateChangeArgs : EventArgs
    {
        public DriverStateChangeArgs(StateId stateId)
        {
            _stateId = stateId;
        }

        private StateId _stateId;

        public StateId StateId
        {
            get { return _stateId; }
        }
    }
}
