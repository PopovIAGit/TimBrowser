using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.File
{
    [Serializable]
    public class DesktopFileItemM : IFileItemM
    {
        public DesktopFileItemM(string name, string timBrowserVersion, 
            FuncDownloadDataM funcDownloadData)
        {
            _name = name;
            _timBrowserVersion = timBrowserVersion;
            _funcDownloadData = funcDownloadData;
        }

        private string _name;
        private string _timBrowserVersion;
        private FuncDownloadDataM _funcDownloadData;

        public string Name
        {
            get { return _name; }
        }

        public string TimBrowserVersion
        {
            get { return _timBrowserVersion; }
        }

        public FuncDownloadDataM FuncDownloadData
        {
            get { return _funcDownloadData; }
        }
    }
}
