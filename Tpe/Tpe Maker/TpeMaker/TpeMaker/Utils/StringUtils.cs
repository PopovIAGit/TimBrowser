using System;
using TpeMaker.Helper;

namespace TpeMaker.Utils
{
    public class StringUtils
    {
        public static string MakeOutFilePath(string inFilePath)
        {
            int lenght = inFilePath.Length - 3;

            string outFilePath = inFilePath.Remove((lenght), 3).
                    Insert(lenght, Constants.OUT_FILE_EXT);

            return outFilePath;
        }
    }
}
