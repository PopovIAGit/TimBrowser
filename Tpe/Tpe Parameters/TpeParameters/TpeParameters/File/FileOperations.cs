using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TpeParameters.File
{
    public class FileOperations
    {

        public FileStream LoadFileStream(string filePath)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            }
            catch
            { }

            return fileStream;
        }

        public void SaveMemoryStream(MemoryStream memoryStream, string path)
        {
            if (memoryStream == null)
                return;

            try
            {
                using (System.IO.FileStream output = new System.IO.FileStream(path, FileMode.Create))
                {
                    memoryStream.CopyTo(output);
                }

            }
            catch
            { }
        }

    }
}
