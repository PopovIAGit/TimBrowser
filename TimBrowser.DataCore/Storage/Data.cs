using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;
using TimBrowser.DataCore.Model;

namespace TimBrowser.DataCore.Storage
{

    public class Data
    {
        #region Definitions

        static Data()
        { }

        public Data()
        { }

        private static Data instance;
        public static Data Instance
        {
            get
            {
                if (instance == null)
                    instance = new Data();
                return instance;
            }
        }

        #endregion

        private FuncDownloadData _funcDownloadData;
        private InformationModuleData _informationModuleData;

        public void SetFuncDownloadData(FuncDownloadData funcDownloadData)
        {
            _funcDownloadData = funcDownloadData;

            if (OnFuncDownloadDataChanged != null)
                OnFuncDownloadDataChanged(this, new EventArgs());
        }

        public void SetInformationModuleData(InformationModuleData informationModuleData)
        {
            _informationModuleData = informationModuleData;

            if (OnInformationModuleChanged != null)
                OnInformationModuleChanged(this, new EventArgs());
        }

        public FuncDownloadData FuncDownloadData
        {
            get { return _funcDownloadData; }
        }

        public InformationModuleData InformationModuleData
        {
            get { return _informationModuleData; }
        }



        public event EventHandler OnFuncDownloadDataChanged;
        public event EventHandler OnInformationModuleChanged;
    }

}
