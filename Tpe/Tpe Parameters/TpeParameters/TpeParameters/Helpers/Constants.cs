using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TpeParameters.Helpers
{
    public class Constants
    {
        internal static readonly byte[] CsKey = new byte[] { 0x54, 0x1F, 0x10, 0x65, 0x20, 0x6B, 0x65, 0x79, 0x78, 0x01, 0x6C, 0x54, 0x75, 0x2A, 0x69, 0x64 };
        internal static readonly byte[] CsIV = new byte[] { 0x20, 0x6B, 0x15, 0x79, 0x7C, 0x61, 0x5C, 0x45 };


        public static string FilePath
        {
            get { return System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); }
        }

        public const string TpeFileExtention = ".tpe";
    }
}
