using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.States;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.Helper
{
    public class DriverStateFactory
    {
        public static IDriverState GetDriverState(StateId stateId, Driver driver)
        {
            IDriverState state = null;

            switch (stateId)
            {
                case StateId.Initial:

                    state = new InitialState(stateId, driver);

                    break;

                case StateId.Authorization:
                    state = new AuthorizationState(stateId, driver);
                    break;
                case StateId.Authorized:

                    state = new AuthorizedState(stateId, driver);

                    break;

                case StateId.Connected:
                    state = new ConnectedState(stateId, driver);

                    break;

                case StateId.Connecting:

                    state = new ConnectingState(stateId, driver);
                    break;

                case StateId.Disconnected:
                    state = new DisconnectedState(stateId, driver);
                    break;

                case StateId.Disconnecting:

                    state = new DisconnectingState(stateId, driver);
                    break;

                case StateId.Discovered:
                    state = new DiscoveredState(stateId, driver);
                    break;

                case StateId.Discovering:
                    state = new DiscoveringState(stateId, driver);
                    break;
            }

            if (state == null)
                throw new Exception("Bluetooth driver state is null");

            return state;
        }
    }
}
