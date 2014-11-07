using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Services.Print
{
    public static class PrintCode
    {
        public static string GetPrintCode(int pageCount)
        {
            Random random = new Random();

            // код состоит из: произвольное число от 100 до 999 + число страниц в печатуемом документе + 
            // текущая дата со временем, написанные слитно
            string code = random.Next(100, 999).ToString() + 
                 pageCount.ToString() +
                 DateTime.Now.ToString("ddMMyyHHmm");

            return code;
        }

    }
}
