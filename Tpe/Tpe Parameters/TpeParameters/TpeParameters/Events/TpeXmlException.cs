using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Helpers;

namespace TpeParameters.Events
{
    public class TpeXmlException : ApplicationException
    {
        public TpeXmlException(TpeXmlErrorCodes tpeXlmErrorCode)
        {
            _tpeXlmErrorCode = tpeXlmErrorCode;
        }

        private TpeXmlErrorCodes _tpeXlmErrorCode;

        public TpeXmlErrorCodes TpeXmlErrorCode
        {
            get { return _tpeXlmErrorCode; }
        }

    }
}
