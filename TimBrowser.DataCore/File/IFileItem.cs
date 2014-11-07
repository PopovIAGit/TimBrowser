using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.File
{
    public interface IFileItem
    {
        string Name { get; }
        string TimBrowserVersion { get; }
        FuncDownloadData FuncDownloadData { get; }  
    }
}
