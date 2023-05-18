namespace PT
{
    public static class AttChecker
    {
        public static bool AttRequired()
        {
#if UNITY_IOS && !UNITY_EDITOR
            var version = new System.Version(UnityEngine.iOS.Device.systemVersion);
            var minimumAttSupportedVersion = new System.Version("14.5");
            return version.CompareTo(minimumAttSupportedVersion) >= 0;
#endif
            return false;
        }
    }
}