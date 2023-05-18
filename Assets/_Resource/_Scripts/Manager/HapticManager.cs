using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public static class HapticManager 
{
    // ほんとに軽いイベント時。連続してアイテムを取得するなど。
    public static void OnLight()
    {
        if (!PlayingDataManager.IsVibration())
        {
            return;
        }
        
        #if UNITY_ANDROID
        HapticPatterns.PlayConstant(0.03f,0.06f,0.03f);
        #elif UNITY_IOS
        #endif
    }
    
    // 中程度のイベント時。レベルアップや強化など。
    public static void OnMedium()
    {
        if (!PlayingDataManager.IsVibration())
        {
            return;
        }
        
#if UNITY_ANDROID
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
#elif UNITY_IOS
#endif
    }
    
    // 成功イベント時。クリアや大きめのイベント達成など。
    public static void OnSuccess()
    {
        if (!PlayingDataManager.IsVibration())
        {
            return;
        }
        
#if UNITY_ANDROID
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
#elif UNITY_IOS
#endif
    }
}
