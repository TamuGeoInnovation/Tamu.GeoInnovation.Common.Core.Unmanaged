using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace USC.GISResearchLab.Common.Core.Utils.ScreenSavers
{
    public class ScreenSaverUtils
    {
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, ref bool pvParam, int fWinIni);
        public const int screensaverrunning = 0x72;

        public static bool ScreenSaverRunning()
        {
            bool ret = false;
            int i = SystemParametersInfo(screensaverrunning, 0, ref ret, 0);
            return ret;
        }
    }
}
