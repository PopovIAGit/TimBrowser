using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Helpers
{
    public class Constants
    {
        public const int ReadParamsPacketNum = 20;             // Число параметров на считывания за один запрос

        public const string TemplatesDirectoryName = "Templates";

        public static string ApplicationDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

    }
}
