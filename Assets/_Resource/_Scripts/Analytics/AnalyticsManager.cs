using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    public static class AnalyticsManager
    {
        public static void Setup()
        {
        }

        public static void StartGame(int stageNo)
        {
            GameAnalyticsHelper.StartGame(stageNo);
            UnityAnalyticsHelper.StartGame(stageNo);
        }

        public static void FinishGame(int score)
        {
            GameAnalyticsHelper.FinishGame(score);
            UnityAnalyticsHelper.FinishGame(score);
        }

        public static void ClearGame(int stageNo, int score)
        {
            GameAnalyticsHelper.ClearGame(stageNo, score);
            UnityAnalyticsHelper.ClearGame(stageNo, score);
        }

        public static void FailGame(int stageNo)
        {
            GameAnalyticsHelper.FailGame(stageNo);
            UnityAnalyticsHelper.FailGame(stageNo);
        }

        public static void QuitGame(int stageNo)
        {
            UnityAnalyticsHelper.QuitGame(stageNo);
        }

        public static void SendEvents(string name, string[] keys, object[] values = null)
        {
            Debug.Log("keys:"+keys.Length);
            Debug.Log("values:"+values.Length);
            if (keys.Length != values.Length)
            {
                return;
            }

            var log = "AnalyticsManager ==== SendEvent: " + name;
            for (var i = 0; i < keys.Length; i++)
            {
                log += "/ " + keys[i] + ":" + values[i];
            }
            UnityAnalyticsHelper.SendEvents(name, keys, values);
            GameAnalyticsHelper.SendDesignEvents(name, keys, values);
            Debug.Log(log);
        }

        public static void SendCompleteEvent()
        {
            return;
            // Id取得
            int _eventAreaId = PlayingDataManager.GetEventAreaId();
            Debug.Log("AnalyticsManager:SendCompleteEvent:" + _eventAreaId);
#if !UNITY_EDITOR
            // 送信
            Debug.Log("AnalyticsManager:SendCompleteEvent:" + _eventAreaId);
            GameAnalyticsHelper.CompleteEvent(_eventAreaId);
#endif
            // Idを進める
            _eventAreaId++;
            PlayingDataManager.SetEventAreaId(_eventAreaId);
        }
        public static void SendEvent(string name)
        {
            return;
            #if !UNITY_EDITOR
            Debug.Log("AnalyticsManager:SendEvent:" + name);
            GameAnalyticsHelper.SendEvent(name);
            #endif
            
        }
    }
