using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.File
{
    public interface IFileOperation
    {
        IFileItem LoadFile(string filePath);
        IFileItem LoadFileA(string filePath);
        bool SaveFile(IFileItem fileItem, string filePath);
    }
}
