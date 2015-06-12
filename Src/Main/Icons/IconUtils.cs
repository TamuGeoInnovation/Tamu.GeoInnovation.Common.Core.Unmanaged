using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace USC.GISResearchLab.Common.Utils.Icons
{
    public class IconUtils
    {

        // Constants that we need in the function call
        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;

        // This structure will contain information about the file
        public struct SHFILEINFO
        {
            // Handle to the icon representing the file
            public IntPtr hIcon;

            // Index of the icon within the image list
            public int iIcon;

            // Various attributes of the file
            public uint dwAttributes;

            // Path to the file
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDisplayName;

            // File type
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;

        };

        // The signature of SHGetFileInfo (located in Shell32.dll)
        [DllImport("Shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);


        public static Icon ExtractSmallIconFromExe(string filename)
        {
            Icon ret = null;

            try
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                IntPtr hImgSmall;
                hImgSmall = SHGetFileInfo(filename, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
                ret = Icon.FromHandle(shinfo.hIcon);
            }
            catch (Exception e)
            {
                throw new Exception("Error occurrec extracting small icon: " + filename, e);
            }

            return ret;
        }

        public static IntPtr GetSmallIconResource(string filename)
        {
            IntPtr ret;

            try
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                ret = SHGetFileInfo(filename, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
            }
            catch (Exception e)
            {
                throw new Exception("Error occurrec get small icon pointer: " + filename, e);
            }

            return ret;
        }

        public static Icon ExtractLargeIconFromExe(string filename)
        {
            Icon ret = null;

            try
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                IntPtr hImgLarge;
                hImgLarge = SHGetFileInfo(filename, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
                ret = Icon.FromHandle(shinfo.hIcon);

            }
            catch (Exception e)
            {
                throw new Exception("Error occurrec extracting small icon: " + filename, e);
            }

            return ret;
        }

    }
}
