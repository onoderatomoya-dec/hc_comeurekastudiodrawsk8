using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;
#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public static class GameManager
{
    // ゲーム開始
    public static BoolReactiveProperty _isStart = new BoolReactiveProperty(false);
    // 現在の進捗ID
    public static IntReactiveProperty _id = new IntReactiveProperty();
    // 現在のステージID
    public static IntReactiveProperty _stageId = new IntReactiveProperty();
    // ステージのIndex
    public static int _stageIndexId;
    // クリア判定
    public static BoolReactiveProperty _isClear = new BoolReactiveProperty(false);
    // ミス判定
    public static BoolReactiveProperty _isMiss = new BoolReactiveProperty(false);

    // スタートボタンを押した時の処理
    public static Action _startEvent = null;

    // チュートリアル
    public static StringReactiveProperty _tutorialName = new StringReactiveProperty();
    public static string _savedTutorialName = null;

    // WaveUI表示
    public static BoolReactiveProperty _showWaveUi = new BoolReactiveProperty(false);

    // 所持コイン
    public static IntReactiveProperty _coin = new IntReactiveProperty(PlayingDataManager.GetCoin());

    // Launcharが呼ばれている
    public static bool _isLaunchar = false;
    
    // InGameから開始された
    public static bool _isInGameStart = false;
    
    // UI表示中
    public static bool _isShowUi = false;
    public static Action _showUiAction = null;
    
    // インターステイシャル表示イベント
    public static Action _showInterstitialAction = null;


    // 初期化
    public static void Init()
    {
        _isStart.Value = false;
        _isClear.Value = false;
        _isMiss.Value = false;
        
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }
    
    // IngameSceneの読み込み完了イベント
    public static void OnInGameSceneLoadedEvent()
    {
        Debug.Log("OnInGameSceneLoadedEvent");
        LoadUi.Instance.Hide();
    }
    
    // OutGameSceneの読み込み完了イベント
    public static void OnOutGameSceneLoadedEvent()
    {
        Debug.Log("OnOutGameSceneLoadedEvent");
        // チュートリアル表示
        _tutorialName.Value = _savedTutorialName;
    }

    // ゲーム開始処理の設定
    public static void SetStartEvent(string sceneName,string tutorialName,Action initAction, Action startAction)
    {
        _startEvent = startAction;
        _savedTutorialName = tutorialName;

        // InGameからSceneをLoadした場合の初期化処理
        if (!_isLaunchar)
        {
            _isInGameStart = true;
            SceneChangeManager._currentSceneName = sceneName;
            SceneManager.LoadSceneAsync("Launcher");
        }
        // Launcherから起動した場合
        else
        {
            // 通常通り読み込む
            _isInGameStart = false;
            initAction();
        }
    }

    // 画面タップでゲームを開始
    // OutGameで画面のタッチ判定を行っている
    public static void OnStartEvent()
    {
        _startEvent();
        _isStart.Value = true;
    }
    
    // 広告表示設定
    public static void SetShowUi(bool flag)
    {
        // プレイヤーをIdleに
        _isShowUi = flag;
        if (flag && _showUiAction != null)
        {
            _showUiAction();
        }
        
        // Uiが閉じられたらインターステイシャル表示
        if (!flag && _showInterstitialAction != null)
        {
            _showInterstitialAction();
        }
    }
    
    // アクションUI設定
    public static void SetShowUiAction(Action action)
    {
        _showUiAction = action;
    }
    
    // アクションInterstitial設定
    public static void SetShowInterstitialAction(Action action)
    {
        _showInterstitialAction = action;
    }
}