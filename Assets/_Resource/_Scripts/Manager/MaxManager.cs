using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using GameAnalyticsSDK;

    public class MaxManager : SingletonMonoBehaviour<MaxManager>
    {
        private const string BannerAdUnitId = "8be3a824f3dd1299";
        private const string InterstitialAdUnitId = "77370069da3a90b3";
        private const string RewardedAdUnitId = "3ae957772e9b62d9";
        private const string AOSBannerAdUnitId = "956e36773a4e41dd";
        private const string AOSInterstitialAdUnitId = "fefbdd2133d9c6ac";
        private const string AOSRewardedAdUnitId = "638e3ae74464331d";

        private Action _rewardAction;
        private Action _failedAdAction;
        private float _loadAdsIntarval = 1.0f;
        private bool _showInterstitial = false;

        void Start()
        {
            // デバッガー
            //MaxSdk.ShowMediationDebugger();
            // MaxSdk.SetVerboseLogging(true);
            
            // 初期化
            /*InitializeBannerAds();
            InitializeInterstitialAds();
            InitializeRewardedAds();
            
            // インプレッションデータをGAに取得
            GameAnalyticsILRD.SubscribeMaxImpressions();
            
            // バナー表示
            ShowBanner();
            
            // 次のインステ再生時間設定
            TimeUtil.SetShowInterstitialDateTime();*/
        }
        
        void Update()
        {

        }

        private void Init()
        {
        }

        // Banner
        private void InitializeBannerAds()
        {
            Debug.Log("MAX === Initialize Banner Ads");
#if UNITY_IPHONE
            MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
            MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.black);
#elif UNITY_ANDROID
            MaxSdk.CreateBanner(AOSBannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
            MaxSdk.SetBannerBackgroundColor(AOSBannerAdUnitId, Color.black);
#endif
            
            MaxSdkCallbacks.Banner.OnAdLoadedEvent      += OnBannerAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent  += OnBannerAdLoadFailedEvent;
            MaxSdkCallbacks.Banner.OnAdClickedEvent     += OnBannerAdClickedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
            MaxSdkCallbacks.Banner.OnAdExpandedEvent    += OnBannerAdExpandedEvent;
            MaxSdkCallbacks.Banner.OnAdCollapsedEvent   += OnBannerAdCollapsedEvent;
        }
        public void ShowBanner()
        {
#if UNITY_IPHONE
            MaxSdk.ShowBanner(BannerAdUnitId);
#elif UNITY_ANDROID
            MaxSdk.ShowBanner(AOSBannerAdUnitId);
#endif
        }
        public void HideBanner()
        {
#if UNITY_IPHONE
            MaxSdk.HideBanner(BannerAdUnitId);
#elif UNITY_ANDROID
            MaxSdk.HideBanner(AOSBannerAdUnitId);
#endif
        }
        
        private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

        private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) {}

        private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

        private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

        private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)  {}

        private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}
        
        
        
        
        

        // Interstitial
        private void InitializeInterstitialAds()
        {
            Debug.Log("MAX === Initialize Interstitial Ads");
            // Attach callback
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

            LoadInterstitial();
        }

        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnInterstitialLoadedEvent");
            // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

            // Reset retry attempt
            retryAttempt = 0;
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            Debug.Log("OnInterstitialLoadFailedEvent");
            // Interstitial ad failed to load 
            // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

            retryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
    
            Invoke("LoadInterstitial", (float) retryDelay);
        }

        private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnInterstitialDisplayedEvent");
            // Interstitial ad is hidden. Pre-load the next ad
            
            // 次のインステ再生時間設定
            TimeUtil.SetShowInterstitialDateTime();
            
            // LoadAdsを消す
            LoadAdsUi.Instance.Hide();
            GameManager.SetShowUi(false);
            _showInterstitial = false;

            LoadInterstitial();
        }
        
        private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnInterstitialAdFailedToDisplayEvent");
            // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
            LoadInterstitial();
        }

        private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnInterstitialClickedEvent");
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnInterstitialHiddenEvent");
            
            // 次のインステ再生時間設定
            TimeUtil.SetShowInterstitialDateTime();
            _showInterstitial = false;
            
            // Interstitial ad is hidden. Pre-load the next ad.
            LoadInterstitial();
        }

        void LoadInterstitial()
        {
#if UNITY_IPHONE
            MaxSdk.LoadInterstitial(InterstitialAdUnitId);
#elif UNITY_ANDROID
            MaxSdk.LoadInterstitial(AOSInterstitialAdUnitId);
#endif
        }

         public bool ShowInterstitial()
         {
             // チュートリアル中はインステ再生しない
             if (PlayingDataManager.IsIdleTutorial() || _showInterstitial)
             {
                 return false;
             }
             
            // インステ再生タイミングでない場合は再生しない
            if (!TimeUtil.IsShowInterstitial())
            {
                return false;
            }
            
 #if UNITY_IPHONE
             if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
             {
                 Debug.Log("ShowInterstitial Showing");
                 LoadAdsUi.Instance.Show();
                 GameManager.SetShowUi(true);
                 _showInterstitial = true;

                 this.CallAfter(_loadAdsIntarval, delegate
                 {
                     MaxSdk.ShowInterstitial(InterstitialAdUnitId);
                 
                     // インステ再生イベント送信
                     AnalyticsManager.SendEvent(AnalyticsEventKey.WatchInterstitial);
                 });
                 return true;
             }
             else
             {
                 Debug.Log("Ad not ready");
                 return false;
             }
 #elif UNITY_ANDROID
             if (MaxSdk.IsInterstitialReady(AOSInterstitialAdUnitId))
             {
                 Debug.Log("ShowInterstitial Showing");
                 LoadAdsUi.Instance.Show();
                 GameManager.SetShowUi(true);
                 _showInterstitial = true;

                 
                 this.CallAfter(_loadAdsIntarval, delegate
                 {
                     MaxSdk.ShowInterstitial(AOSInterstitialAdUnitId);
                 
                     // インステ再生イベント送信
                     AnalyticsManager.SendEvent(AnalyticsEventKey.WatchInterstitial);
                 });
                 
                 return true;
             }
             else
             {
                 Debug.Log("Ad not ready");
                 return false;
             }
 #endif
         }

        // Reward Movie
        int retryAttempt;
        private void InitializeRewardedAds()
        {
            Debug.Log("MAX === Initialize Rewarded Ads");
            // Attach callback
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

            LoadRewardedAd();
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

            // Reset retry attempt
            retryAttempt = 0;
        }

        private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            Debug.Log("OnRewardedAdLoadFailedEvent");
            // Rewarded ad failed to load 
            // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

            retryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
    
            Invoke("LoadRewardedAd", (float) retryDelay);
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnRewardedAdDisplayedEvent");
            if (_failedAdAction != null)
            {
                _failedAdAction();
                _failedAdAction = null;
            }
            
            // 次のインステ再生時間設定
            TimeUtil.SetShowInterstitialDateTime();
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnRewardedAdFailedToDisplayEvent");
            // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
            LoadRewardedAd();
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnRewardedAdClickedEvent");
        }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnRewardedAdHiddenEvent");
            // Rewarded ad is hidden. Pre-load the next ad
            if (_failedAdAction != null)
            {
                _failedAdAction();
                _failedAdAction = null;
            }
            
            // 次のインステ再生時間設定
            TimeUtil.SetShowInterstitialRewardedDateTime();
            
            LoadRewardedAd();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            // The rewarded ad displayed and the user should receive the reward.
            Debug.Log("OnRewardedAdReceivedRewardEvent");
            if (_rewardAction != null)
            {
                _rewardAction();
                _rewardAction = null;
                
                // リワード再生イベント送信
                AnalyticsManager.SendEvent(AnalyticsEventKey.WatchReward);
            }
            
            // 次のインステ再生時間設定
            TimeUtil.SetShowInterstitialRewardedDateTime();
           
            LoadRewardedAd();
        }

        private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("OnRewardedAdRevenuePaidEvent");
            LoadRewardedAd();
            // Ad revenue paid. Use this callback to track user revenue.
        }

        public void LoadRewardedAd()
        {
#if UNITY_IPHONE
            MaxSdk.LoadRewardedAd(RewardedAdUnitId);
#elif UNITY_ANDROID
            MaxSdk.LoadRewardedAd(AOSRewardedAdUnitId);
#endif

        }
        public bool ShowRewardedAd()
        {
#if UNITY_IPHONE
            if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
            {
                Debug.Log("Showing");
                MaxSdk.ShowRewardedAd(RewardedAdUnitId);
                return true;
            }
            else
            {
                Debug.Log("Ad not ready");
                return false;
            }
#elif UNITY_ANDROID
            if (MaxSdk.IsRewardedAdReady(AOSRewardedAdUnitId))
            {
                Debug.Log("Showing");
                MaxSdk.ShowRewardedAd(AOSRewardedAdUnitId);
                return true;
            }
            else
            {
                Debug.Log("Ad not ready");
                return false;
            }
#endif
        }
        public bool IsRewardedAdReady()
        {
#if UNITY_IPHONE
            return MaxSdk.IsRewardedAdReady(RewardedAdUnitId);
#elif UNITY_ANDROID
            return MaxSdk.IsRewardedAdReady(AOSRewardedAdUnitId);
#endif
            return false;
        }

        public void SetRewardAction(Action action)
        {
            _rewardAction = action;
        }
        public void SetFailedAdAction(Action failedAdAction)
        {
            _failedAdAction = failedAdAction;
        }
    }