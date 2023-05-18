using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace PT
{
    public class UnityAnalyticsPrivacyTool : IPrivacyTool
    {
        public async Task Init(bool isOptIn)
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

        public void OptIn()
        {
            Debug.Log("#TTL : UnityAnalyticsOptIn");
            UnityEngine.Analytics.Analytics.enabled = true;
            UnityEngine.Analytics.Analytics.deviceStatsEnabled = true;
            UnityEngine.Analytics.Analytics.limitUserTracking = false;
#if UNITY_2018_3_OR_NEWER
            UnityEngine.Analytics.Analytics.initializeOnStartup = true;
#endif
        }

        public void OptOut()
        {
            Debug.Log("#TTL : UnityAnalyticsOptOut");
            UnityEngine.Analytics.Analytics.enabled = false;
            UnityEngine.Analytics.Analytics.deviceStatsEnabled = false;
            UnityEngine.Analytics.Analytics.limitUserTracking = true;
#if UNITY_2018_3_OR_NEWER
            UnityEngine.Analytics.Analytics.initializeOnStartup = false;
#endif
        }
    }
}