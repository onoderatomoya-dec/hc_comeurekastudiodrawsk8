using System.Collections.Generic;
using UnityEngine.Analytics;


    public static class UnityAnalyticsHelper
    {
        public static void StartGame(int stageNo)
        {
            AnalyticsEvent.GameStart();
            AnalyticsEvent.LevelStart("stage" + stageNo);
        }

        public static void FinishGame(int score)
        {
            AnalyticsEvent.GameOver();
        }

        public static void ClearGame(int stageNo, int score)
        {
            AnalyticsEvent.LevelComplete("stage" + stageNo);
        }

        public static void FailGame(int stageNo)
        {
            AnalyticsEvent.LevelFail("stage" + stageNo);
        }

        public static void QuitGame(int stageNo)
        {
            AnalyticsEvent.LevelQuit("stage" + stageNo);
        }

        public static void SendEvent(string name, string key, object value)
        {
            Analytics.CustomEvent(
                name,
                new Dictionary<string, object>
                {
                    { key, value }
                }
            );
        }
        public static void SendEvents(string name, string[] keys, object[] values)
        {
            var param = new Dictionary<string, object>();
            for(var i = 0; i < keys.Length; i++)
            {
                param[keys[i]] = values[i];
            }

            Analytics.CustomEvent(
                    name,
                    param
                );
        }
    }
