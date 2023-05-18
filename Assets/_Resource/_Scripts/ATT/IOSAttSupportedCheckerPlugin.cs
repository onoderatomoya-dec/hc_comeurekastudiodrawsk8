#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace PT
{
    public static class IOSAttSupportedCheckerPlugin
    {
        public static int GetATTStatus()
        {
            return getATTSupported();
        }
        #region DllImport
        const string DllName = "__Internal";
        [DllImport(DllName)]
        static extern int getATTSupported();
        #endregion
    }
}
#endif