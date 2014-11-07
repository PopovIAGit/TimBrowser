using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.File
{
    [Serializable]
    public class DesktopFileItem : IFileItem
    {
        public DesktopFileItem(string name, string timBrowserVersion, 
            FuncDownloadData funcDownloadData)
        {
            _name = name;
            _timBrowserVersion = timBrowserVersion;
            _funcDownloadData = funcDownloadData;
        }

        private string _name;
        private string _timBrowserVersion;
        private FuncDownloadData _funcDownloadData;

        public string Name
        {
            get { return _name; }
        }

        public string TimBrowserVersion
        {
            get { return _timBrowserVersion; }
        }

        public FuncDownloadData FuncDownloadData
        {
            get { return _funcDownloadData; }
        }
    }
}
