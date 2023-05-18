using AudienceNetwork;
using Facebook.Unity;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace PT
{
    public class FacebookPrivacyTool : IPrivacyTool
    {
        // Facebook SDKの対応
        // FB.Init完了後にOptIn、Outが必要となる
        // CCPAの設定方法
        // https://developers.facebook.com/docs/app-events/guides/ccpa
        // GDPRの設定方法
        // https://developers.facebook.com/docs/app-events/gdpr-compliance 
        // Advertising Tracking Enabled for Audience Network(ATE)の設定方法
        // For iOS 14.5 or later users, you must:
        //  - Set the setAdvertiserTrackingEnabled flag.
        // https://developers.facebook.com/docs/audience-network/setting-up/platform-setup/ios/advertising-tracking-enabled/?locale=ja_JP

        public async Task Init(bool isOptIn)
        {
            bool initialized = false;
            // 上記マニュアルにある通り、iOS14.5未満はデフォルトでIDFAが取得がONのため
            // SetAdvertiserTrackingEnabledを呼ぶ必要はない
            #if UNITY_IOS
            if (AttChecker.AttRequired())
            {
                // ATTの許諾状態を渡す
                AdSettings.SetAdvertiserTrackingEnabled(AttSupportedChecker.IsAttApproved());
            }
            #endif
            if (FB.IsInitialized)
            {
                initialized = true;
                Opt(isOptIn);
            }
            else
            {
                FB.Init(() =>
                {
                    Opt(isOptIn);
                    initialized = true;
                });
            }
            
            while (!initialized)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f), CancellationToken.None);
            }
        }

        public void OptIn()
        {
            Debug.Log("#TTL : FacebookOptIn");
            FB.Mobile.SetAutoLogAppEventsEnabled(true);
            // データ使用制限(LDU)モードを明示的に無効にする
            AdSettings.SetDataProcessingOptions(new string[] { });
        }

        public void OptOut()
        {
            Debug.Log("#TTL : FacebookOptOut");
            FB.Mobile.SetAutoLogAppEventsEnabled(false);
            // 地理的位置情報を使ってユーザーに対してデータ使用制限(LDU)モードを有効にする
            AdSettings.SetDataProcessingOptions(new string[] {"LDU"}, 0, 0);
        }

        private void Opt(bool isOptIn)
        {
            if (isOptIn)
            {
                OptIn();
            }
            else
            {
                OptOut();
            }
            FB.ActivateApp();
        }
    }
}