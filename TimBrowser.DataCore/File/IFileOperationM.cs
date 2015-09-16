using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.File
{
    public interface IFileOperationM
    {
        bool SaveFile(IFileItemM fileItem, string filePath);
    }
}
