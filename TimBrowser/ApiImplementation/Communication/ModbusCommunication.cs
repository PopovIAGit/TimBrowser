using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using TimBrowser.ApiImplementation.ILow;
using TimBrowser.DataCore.Communication;

namespace TimBrowser.ApiImplementation.Communication
{
    public class ModbusCommunication : ICommunicationSource
    {
        public ModbusCommunication()
        {
            _currentLowCommunicationFunc = new EmptyLowCommunicationFunc();

            _deviceAddress = 1;
        }

        private ILowCommunicationFunc _currentLowCommunicationFunc;
        private IProtocolCommunicationFunc _protocolCommunicationFunc;

        private int _deviceAddress;
       //private bool _isBusy;
        private bool _canCommunicate;

        public ILowCommunicationFunc LowCommunicationFunc
        {
            get { return _currentLowCommunicationFunc; }
            set { _currentLowCommunicationFunc = value; }
        }

        public int DeviceAddress
        {
            get { return _deviceAddress; }
            set
            {
                _deviceAddress = value;
            }
        }

        public bool CanCommunicate
        {
            get { return _canCommunicate; }
        }

        public bool IsBusy
        {
            get { return false; }
        }

        public IProtocolCommunicationFunc ProtocolCommunicationFunc
        {
            get {   return _protocolCommunicationFunc;  }
            set 
            {   
                _protocolCommunicationFunc = value;

                if (_protocolCommunicationFunc != null)
                    _canCommunicate = true;
            }
        }

        public bool IsUpdating
        {
            get;
            set;
        }

        public EventHandler<CommErrorEventArgs> CommErrorEvent { get; set; }

    }
}
