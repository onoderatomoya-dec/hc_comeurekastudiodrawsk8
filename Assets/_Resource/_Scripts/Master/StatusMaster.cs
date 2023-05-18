using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusMaster
{
    //public static int _interstitialShowTime = -1;
    //public static int _nextInterstitialShowTime = 10;
    public static string _testName = "test_A";

    public static void SetMaster(JsonData jsonData)
    {
        //_interstitialShowTime = jsonData.GetInt("InterstitialShowTime");
        //_nextInterstitialShowTime = jsonData.GetInt("NextInterstitialShowTime");
    }

    public static void SetTestName(string name)
    {
        _testName = name;
    }
}