using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Transform
{
    public class TransformDownloadDataException : ApplicationException
    {
        public TransformDownloadDataException(TranformErrorCodes tranformErrorCode)
        {
            _tranformErrorCode = tranformErrorCode;
        }

        private TranformErrorCodes _tranformErrorCode;

        public TranformErrorCodes TranformErrorCode
        {
            get { return _tranformErrorCode; }
        }

    }
}
