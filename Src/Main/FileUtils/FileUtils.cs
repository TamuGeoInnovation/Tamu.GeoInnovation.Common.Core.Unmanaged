using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace USC.GISResearchLab.Common.Core.Kernel32
{
    public class Kernel32Utils
    {
        // ref: http://www.c-sharpcorner.com/UploadFile/crajesh1981/RajeshPage103142006044841AM/RajeshPage1.aspx?ArticleID=63e02c1f-761f-44ab-90dd-8d2348b8c6d2
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string path, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder shortPath, int shortPathLength);

        public static string GetShortFileName(string DbfFile)
        {
            StringBuilder shortPath = new StringBuilder(255);
            GetShortPathName(DbfFile, shortPath, shortPath.Capacity);
            return shortPath.ToString();
        }
    }
}
