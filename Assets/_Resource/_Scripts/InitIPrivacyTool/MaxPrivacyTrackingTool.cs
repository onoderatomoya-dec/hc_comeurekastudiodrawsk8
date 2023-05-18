using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace PT
{
    public class MaxPrivacyTrackingTool : IPrivacyTool
    {
        private const string SdkKey = "N4t5940RydoHCPKtWURuALE5gkkwDChbGZgQegsaJdtV2BNZgqRs9gyrOfmcsRC7_tXeTODs14M4GldyT4oilk";

        public async Task Init(bool isOptIn)
        {
            // 初期化
            if (!MaxSdk.IsInitialized())
            {
                bool initialized = false;
                MaxSdk.SetSdkKey(SdkKey);
                MaxSdkCallbacks.OnSdkInitializedEvent += configuration =>
                {
                    initialized = true;
                    if (isOptIn)
                    {
                        OptIn();
                    }
                    else
                    {
                        OptOut();
                    }
                };
                MaxSdk.InitializeSdk();
                while (!initialized)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.1f), CancellationToken.None);
                }
            }
            else
            {
                if (isOptIn)
                {
                    OptIn();
                }
                else
                {
                    OptOut();
                }
            }
        }

        public void OptIn()
        {
            Debug.Log("#TTL : MaxOptIn");
            MaxSdk.SetDoNotSell(false);
            MaxSdk.SetHasUserConsent(true);
        }

        public void OptOut()
        {
            Debug.Log("#TTL : MaxOptOut");
            MaxSdk.SetDoNotSell(true);
            MaxSdk.SetHasUserConsent(false);
        }
    }
}