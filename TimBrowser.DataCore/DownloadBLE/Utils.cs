using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.DownloadBLE
{
    public static class Utils
    {


        public static int CalculateProgress(int currentValue, int maxValues, int maxProgress)
        {
            return (maxProgress * currentValue) / maxValues;
        }


    }
}
