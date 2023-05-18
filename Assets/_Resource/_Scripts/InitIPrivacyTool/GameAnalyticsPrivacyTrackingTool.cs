using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using System.Threading.Tasks;

namespace PT
{
    public class GameAnalyticsPrivacyTrackingTool : IPrivacyTool
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
            GameAnalytics.Initialize();
            GameAnalyticsILRD.SubscribeMaxImpressions();
        }

        public void OptIn()
        {
            Debug.Log("#TTL : GameAnalyticeOptIn");
            GameAnalytics.SetEnabledEventSubmission(true);
        }

        public void OptOut()
        {
            Debug.Log("#TTL : GameAnalyticsOptOut");
            GameAnalytics.SetEnabledEventSubmission(false);
        }
    }
}