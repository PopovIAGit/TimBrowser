using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using TimBrowser.ViewModels;

namespace TimBrowser.ViewModels
{
    public class DownloadViewModel : Screen
    {

        #if DESIGN_TIME

        public DownloadViewModel() : this(new BluetoothViewModel(), new ModBusViewModel(),
            new DownloadInformationModuleViewModel())
        {

        }

        #endif

        public DownloadViewModel(BluetoothViewModel bluetoothViewModel, ModBusViewModel modBusViewModel, BLEViewModel bleViewModel,
            DownloadInformationModuleViewModel downloadInformationModuleViewModel)
        {
            _bluetoothViewModel = bluetoothViewModel;
            _modBusViewModel = modBusViewModel;
            _bleViewModel = bleViewModel;

            _bluetoothViewModel.ConnectAction = OnConnectAction;
            _modBusViewModel.ConnectAction = OnConnectAction;
            _bleViewModel.ConnectAction = OnConnectAction;

            _downloadInformationModuleViewModel = downloadInformationModuleViewModel;
            _downloadInformationModuleViewModel.DownloadAction = OnDownloadAction;
        }

        private readonly DownloadCommBase _bluetoothViewModel;
        private readonly DownloadCommBase _modBusViewModel;
        private readonly DownloadCommBase _bleViewModel;
        private readonly DownloadInformationModuleViewModel _downloadInformationModuleViewModel;


        private void OnDownloadAction(bool isDownloading)
        {
            if (_downloadInformationModuleViewModel.TypeComm == 1) _bluetoothViewModel.DownloadBehaviour(isDownloading);
            else if (_downloadInformationModuleViewModel.TypeComm == 2) _modBusViewModel.DownloadBehaviour(isDownloading);
            else if (_downloadInformationModuleViewModel.TypeComm == 3) _bleViewModel.DownloadBehaviour(isDownloading);
        }

        protected override void OnActivate()
        {
            if (_downloadInformationModuleViewModel.TypeComm == 1) _bluetoothViewModel.Activate();
            else if (_downloadInformationModuleViewModel.TypeComm == 2) _modBusViewModel.Activate();
            else if (_downloadInformationModuleViewModel.TypeComm == 3) _bleViewModel.Activate();
            
        }

        protected override void OnDeactivate(bool close)
        {
            _downloadInformationModuleViewModel.Deactivate();
            if (_downloadInformationModuleViewModel.TypeComm == 1) _bluetoothViewModel.Deactivate();
            else if (_downloadInformationModuleViewModel.TypeComm == 2) _modBusViewModel.Deactivate();
            else if (_downloadInformationModuleViewModel.TypeComm!= 3) _bleViewModel.Deactivate();
            
        }

        private void OnConnectAction(bool canDownload)
        {
            if (canDownload){
                if (_downloadInformationModuleViewModel.TypeComm == 1) _downloadInformationModuleViewModel.ActivateCommunication(_bluetoothViewModel.Communication);
                else if (_downloadInformationModuleViewModel.TypeComm == 2) _downloadInformationModuleViewModel.ActivateCommunicationM(_modBusViewModel.CommunicationM);
                else if (_downloadInformationModuleViewModel.TypeComm == 3) _downloadInformationModuleViewModel.ActivateCommunicationBLE(_bleViewModel.CommunicationBLE);
            }
                
            else
                _downloadInformationModuleViewModel.ActivateCommunication(null);
        }

        public DownloadCommBase BluetoothViewModelObj
        {
            get {
                if (_downloadInformationModuleViewModel.TypeComm == 1) return _bluetoothViewModel;
                else return null;
            }
        }

        public DownloadCommBase ModBusViewModelObj
        {
            get {
                if (_downloadInformationModuleViewModel.TypeComm == 2)  return _modBusViewModel; 
                else return null;
            }
        }

        public DownloadInformationModuleViewModel DownloadInformationModuleObj
        {
            get { return _downloadInformationModuleViewModel; }
        }
    }
}
