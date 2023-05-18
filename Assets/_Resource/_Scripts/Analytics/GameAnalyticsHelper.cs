using UnityEngine;
using System;
using GameAnalyticsSDK;

    public class GameAnalyticsHelper
    {
        static string[] SubCategoryPriority = {
            AnalyticsParamKey.LevelIndex
        };
        public static void Initialize()
        {
        }

        public static void StartGame(int stageNo)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "stage" + stageNo);
        }

        public static void FinishGame(int score)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", score);
        }

        public static void ClearGame(int stageNo, int score)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "stage" + stageNo, score);
        }

        public static void FailGame(int stageNo)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "stage" + stageNo);
        }

        public static void CompleteEvent(int value)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,"Area_" + value);
        }
        public static void SendEvent(string name)
        {
            GameAnalytics.NewDesignEvent(name);
        }
        
        public static void SendDesignEvents(string name, string[] keys, object[] values)
        {
            var eventName = name;
            var subCategoryIndex = -1;

            foreach(var p in SubCategoryPriority)
            {
                var index = Array.IndexOf(keys, p);
                if(index >= 0)
                {
                    subCategoryIndex = index;
                    break;
                }
            }

            // 第二階層
            if(subCategoryIndex >= 0)
            {
                eventName += ":";
                eventName += keys[subCategoryIndex];
                eventName += "_";
                eventName += values[subCategoryIndex].ToString();
            }

            if(keys.Length > 0)
            {
                for (var i = 0; i < keys.Length; i++)
                {
                    if (i == subCategoryIndex)
                    {
                        continue;
                    }
                    var sendEventName = eventName;
                    sendEventName += ":";
                    sendEventName += keys[i];
                    sendEventName += "_";
                    sendEventName += values[i].ToString();
                    //Debug.Log("Tomoya:" + sendEventName);
                    //GameAnalytics.NewDesignEvent(sendEventName);
                }
            }
            else
            {
                //Debug.Log("Tomoya:" + eventName);
                //GameAnalytics.NewDesignEvent(eventName);
            }
        }
    }