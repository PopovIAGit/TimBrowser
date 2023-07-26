using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.DownloadBLE.Model
{
    [Serializable]
    public class FuncSixData
    {
        public FuncSixData(List<int> parametersValues)
        {
            _parametersValues = parametersValues;
        }

        private List<int> _parametersValues;

        /// <summary>
        /// Значения параметров
        /// </summary>
        public List<int> ParametersValues
        {
            get { return _parametersValues; }
        }
    }
}
