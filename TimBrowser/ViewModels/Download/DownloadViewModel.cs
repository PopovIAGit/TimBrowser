using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

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

        public DownloadViewModel(BluetoothViewModel bluetoothViewModel, ModBusViewModel modBusViewModel,
            DownloadInformationModuleViewModel downloadInformationModuleViewModel)
        {
            _bluetoothViewModel = bluetoothViewModel;
            _modBusViewModel = modBusViewModel;

            _bluetoothViewModel.ConnectAction = OnConnectAction;
            _modBusViewModel.ConnectAction = OnConnectAction;

            _downloadInformationModuleViewModel = downloadInformationModuleViewModel;
            _downloadInformationModuleViewModel.DownloadAction = OnDownloadAction;
        }

        private readonly DownloadCommBase _bluetoothViewModel;
        private readonly DownloadCommBase _modBusViewModel;
        private readonly DownloadInformationModuleViewModel _downloadInformationModuleViewModel;


        private void OnDownloadAction(bool isDownloading)
        {
            _bluetoothViewModel.DownloadBehaviour(isDownloading);
        }

        protected override void OnActivate()
        {
            _bluetoothViewModel.Activate();
        }

        protected override void OnDeactivate(bool close)
        {
            _downloadInformationModuleViewModel.Deactivate();
            _bluetoothViewModel.Deactivate();
        }

        private void OnConnectAction(bool canDownload)
        {
            if (canDownload)
                _downloadInformationModuleViewModel.ActivateCommunication(_bluetoothViewModel.Communication);
            else
                _downloadInformationModuleViewModel.ActivateCommunication(null);
        }

        public DownloadCommBase BluetoothViewModelObj
        {
            get { return _bluetoothViewModel; }
        }

        public DownloadCommBase ModBusViewModelObj
        {
            get { return _modBusViewModel; }
        }

        public DownloadInformationModuleViewModel DownloadInformationModuleObj
        {
            get { return _downloadInformationModuleViewModel; }
        }
    }
}
