using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TimBrowser.DataCore.File
{
    public class DesktopFileOperation : IFileOperation
    {
        public IFileItem LoadFile(string filePath)
        {
            IFileItem desktopFileTime = null;

            BinaryFormatter binnaryFormatter = new BinaryFormatter();

            try
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    desktopFileTime = (DesktopFileItem)binnaryFormatter.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoLoadFileNotFound, true);
            }
            catch (Exception)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoLoadFileErr, true);
            }

            return desktopFileTime;
        }

        public bool SaveFile(IFileItem fileItem, string filePath)
        {
            BinaryFormatter binnaryFormatter = new BinaryFormatter();

            DesktopFileItem desktopFileItem = (DesktopFileItem)fileItem;

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
