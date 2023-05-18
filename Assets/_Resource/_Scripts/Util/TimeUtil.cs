using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class TimeUtil
{
    private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    private static DateTime _gameStartDateTime;
    
    // インステ
    private static DateTime _showInterstitialDateTime;
    public static int _interstitialShowTime = -1;
    public static int _interstitialIsIdleTime = 90;// 90
    public static int _nextInterstitialShowTime = 60;// 60
    public static int _nextInterstitialRewardedShowTime = 45;// 120
    public static int _nextInterstitialShowTimeTutorial = 10; // 10
    
    // お金回収
    private static DateTime _showfieldMoneysPopupDateTime;
    public static int _nextFieldMoneysPopupShowTime = 240;// 240

    // DateTimeからUnixTimeへ変換
    public static long GetUnixTime(DateTime dateTime)
    {
        return (long)(dateTime - _unixEpoch).TotalSeconds;
    }

    // UnixTimeからDateTimeへ変換
    public static DateTime GetDateTime(long unixTime)
    {
        return _unixEpoch.AddSeconds(unixTime);
    }
    
    // ゲーム開始時間を設定
    public static void SetGameStartDateTime(DateTime gameStartDateTime)
    {
        _gameStartDateTime = gameStartDateTime;
    }
    
    // 直ぐにインステを表示できるように設定
    public static void SetNowInterstitialDateTimeTutorial()
    {
        long unixTime = GetUnixTime(DateTime.Now);
        unixTime += _interstitialShowTime;
        _showInterstitialDateTime = GetDateTime(unixTime);
    }
    
    // チュートリアル終了後の時間設定
    public static void SetShowInterstitialDateTimeTutorial()
    {
        long unixTime = GetUnixTime(DateTime.Now);
        unixTime += _nextInterstitialShowTimeTutorial;
        _showInterstitialDateTime = GetDateTime(unixTime);
    }

    // インステ開始時間を設定
    public static void SetShowInterstitialDateTime()
    {
        long unixTime = GetUnixTime(DateTime.Now);
        unixTime += _nextInterstitialShowTime;
        _showInterstitialDateTime = GetDateTime(unixTime);
    }
    
    // リワード終了後の時間設定
    public static void SetShowInterstitialRewardedDateTime()
    {
        long unixTime = GetUnixTime(DateTime.Now);
        unixTime += _nextInterstitialRewardedShowTime;
        _showInterstitialDateTime = GetDateTime(unixTime);
    }

    // インステ再生可能か調べる
    public static bool IsShowInterstitial(bool isIdle = false)
    {
        long showTime = GetUnixTime(_showInterstitialDateTime);
        if (isIdle)
        {
            
            showTime += _interstitialIsIdleTime;
        }
        long nowTime = GetUnixTime(DateTime.Now);

        if (nowTime > showTime)
        {
            return true;
        }
        return false;
    }
    
    // お金回収Popup表示時間設定
    public static void SetShowFieldMoneysPopupDateTime()
    {
        long unixTime = GetUnixTime(DateTime.Now);
        unixTime += _nextFieldMoneysPopupShowTime;
        _showfieldMoneysPopupDateTime = GetDateTime(unixTime);
    }
    
    // お金回収Popupの表示可能か調べる
    public static bool IsFieldMoneysPopup()
    {
        long showTime = GetUnixTime(_showfieldMoneysPopupDateTime);
        long nowTime = GetUnixTime(DateTime.Now);

        if (nowTime > showTime)
        {
            return true;
        }
        return false;
    }

    // ゲーム開始時間から特定時間の、経過時間を取得
    public static long GetElapsedTime(DateTime dateTime)
    {
        long gameStartTime = GetUnixTime(_gameStartDateTime);
        long time = GetUnixTime(dateTime);


        return time - gameStartTime;
    }
}