using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace USC.GISResearchLab.Common.Utils.Marshalling
{
    public class MarshallingUtils
    {
        public static void ReleaseObject(object o)
        {
            if (o != null)
            {
                Marshal.FinalReleaseComObject(o);
                o = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static int ReleaseObjectOld(object o)
        {
            int ret = 1;

            if (o != null)
            {
                while (ret > 0)
                {
                    ret = Marshal.ReleaseComObject(o);
                }
            }
            else
            {
                ret = 0;
            }

            return ret;
        }
    }
}
