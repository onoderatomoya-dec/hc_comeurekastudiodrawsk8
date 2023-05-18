using System;
using System.Threading;
using System.Threading.Tasks;

namespace ATT
{
    public class MaxAtt : IAtt
    {
        private const string SdkKey =
            "N4t5940RydoHCPKtWURuALE5gkkwDChbGZgQegsaJdtV2BNZgqRs9gyrOfmcsRC7_tXeTODs14M4GldyT4oilk";

        public async Task<bool> ShowAtt()
        {
            MaxSdk.SetSdkKey(SdkKey);
            var authorized = false;
            var initialized = false;
            MaxSdkCallbacks.OnSdkInitializedEvent += configuration =>
            {
#if UNIYT_IOS
                authorized = configuration.AppTrackingStatus == MaxSdkBase.AppTrackingStatus.Authorized;
#endif
                initialized = true;
            };

            MaxSdk.InitializeSdk();
            while (!initialized)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f), CancellationToken.None);
            }

            return authorized;
        }
    }
}