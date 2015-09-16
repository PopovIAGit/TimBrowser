using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.File
{
    public interface IFileItemM
    {
        string Name { get; }
        string TimBrowserVersion { get; }
        FuncDownloadDataM FuncDownloadData { get; }  
    }
}
