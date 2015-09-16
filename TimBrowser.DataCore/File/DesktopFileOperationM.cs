using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using TimBrowser.DataCore.DownloadM.Model;
using System.Text.RegularExpressions;

namespace TimBrowser.DataCore.File
{
    public class DesktopFileOperationM : IFileOperationM
    {
        public bool SaveFile(IFileItemM fileItem, string filePath)
        {
            BinaryFormatter binnaryFormatter = new BinaryFormatter();

            DesktopFileItemM desktopFileItem = (DesktopFileItemM)fileItem;

            bool operationSucceed = false;

            try
            {
                using (Stream fileStream =
                    new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binnaryFormatter.Serialize(fileStream, fileItem);
                    operationSucceed = true;
                }
            }
            catch (Exception ex)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoSaveFileErr, true);
                operationSucceed = false;
            }

            return operationSucceed;
        }
    }
}
