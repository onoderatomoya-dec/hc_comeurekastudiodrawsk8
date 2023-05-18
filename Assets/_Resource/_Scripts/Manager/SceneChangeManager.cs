using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChangeManager
{
    public static string _currentSceneName = null;

    // シーンの切り替え
    public static void ChageScene()
    {
        int id = PlayingDataManager.GetStageId();
        if (id > StageMaster.GetSize())
        {
            id = 1;
        }
        if (id < 1)
        {
            id = StageMaster.GetSize();
        }
        
        // 古いシーンの削除
        if (!GameManager._isInGameStart && _currentSceneName != null)
        {
            SceneManager.UnloadSceneAsync(_currentSceneName);
        }

        // ステージデータの読み込み
        if (!GameManager._isInGameStart)
        {
            PlayingDataManager.SetStageId(id);
            StageMaster stageMaster = StageMaster.GetStageMaster(id);
            PlayingDataManager._stageMaster = stageMaster;
            _currentSceneName = stageMaster.GetSceneName();
        }

        // Scene切り替えとGameManagerの初期化処理
        GameManager.Init();
        var asyncLoad = SceneManager.LoadSceneAsync(_currentSceneName, LoadSceneMode.Additive);

        // InGameのScene読み込み完了イベント
        asyncLoad.completed += x => OnInGameSceneLoadedEvent();
    }
    
    // InGameのScene読み込み完了イベント
    public static void OnInGameSceneLoadedEvent()
    {
        // シーンをアクティブに(これを行わないとPrefabの生成で想定外のSceneにPrefabなどが生成される可能性がある)
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentSceneName));
        
        // GameManager初期化
        GameManager.OnInGameSceneLoadedEvent();
    }
    
    // OutGameのScene読み込み完了イベント
    public static void OnOutGameSceneLoadedEvent()
    {
        // GameManager初期化
        GameManager.OnOutGameSceneLoadedEvent();
    }

    // OutGame読み込み
    public static void ChangManagerScene()
    {
        // OutGame読み込み
        var asyncLoad = SceneManager.LoadSceneAsync("LoadOutGame");
        
        // InGameのScene読み込み完了イベント
        asyncLoad.completed += x => OnOutGameSceneLoadedEvent();
        
        // InGame読み込み
        ChageScene();
    }
}
