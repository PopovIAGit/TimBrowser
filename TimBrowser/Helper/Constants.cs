using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Helper
{
    public class Constants
    {
        public const string DATE_TIME_FORMAT_STRING = "dd.MM.yy HH:mm:ss";
        public const string TIME_FORMAT_STRING = "HH:mm:ss";
        public const string DATE_TIME_FILE_FORMAT_STRING = "dd.MM.yy HH.mm";

        public const string CHOOSE_ALL_INDEX = "ChooseAll";

        public const string FILE_EXT = ".tim";
        public const string FILE_EXT_FILTER = "*.tim";
        public const string FILE_FILTER = "Tomzel Informational Module (.tim)|*.tim";


        public static string AppVersion
        {
            get 
            { 
                Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                return v.Major.ToString() + "." + v.Minor.ToString() + "." + v.Build.ToString();
            }
        }

    }
}
