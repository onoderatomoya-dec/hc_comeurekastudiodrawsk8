using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using MiniJSON;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class MasterDataManager : SingletonMonoBehaviour<MasterDataManager>
{
    // ファイナル定数(外部からアクセスされる変数はstaticとする)
    static string API_DATA_TABLE = "https://storage.spiritduck.be/hc/com_nana_castlebattle.json";
    public const string MaxSdkKey = "FTca20TycVH8BArkyW-ewbhy6dPiD2USa51d5aZi4ssG1g-zCyc7TJ3eySM9Vsb4ydi2CHlH7aX9GW6TTka01e";
    public static bool _initSDK = false;

    // 通信のステータス
    enum CONNECTION_STATE_ENUM
    {
        CONNECTION_NULL = 0,
        CONNECTION_START,
        CONNECTION_SUCCESS,
        CONNECTION_ERROR
    }

    /// メンバ
    private int _connectionStatus = (int)CONNECTION_STATE_ENUM.CONNECTION_NULL;
    private float _connectionProgress = 0.0f;
    private string _jsonText = null;

    // リモートセッティング
    private static bool _isDataLoadSucces = false;
    public const string VERSION_STAGE_NAME = "stage_1";
    public const string VERSION_STATUS_NAME = "status_1";
    private string _testingStatus = "A";
    private bool _isUnityAnalyticsRemoteConfigCompleted = true;
    private bool _isNetwork = true;

    // ネットワークチェック
    [SerializeField] private GameObject _notReachableText;
    protected bool _isNetworkChack = true;
    protected float _networkChackInterval = 3.0f;
    
    // GameAnalyticsチェック
    protected bool _isGameAnalyticsChack = true;
    protected bool _isGameAnalyticsRemoteConfigCompleted = false;
    protected float _gameAnalyticsInterval = 1.0f;
    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        // Awakeメソッドは必ずここから初期化処理を行う
        // PlayerPrefs.DeleteAll();
    }

    void OnEnable()
    {
        base.OnEnable();
    }

    void Start()
    {
        base.Start();
        
        // ネットワーク状況を確認
        _notReachableText.SetActive(false);
        //_isNetwork = true;
        CheckNetworkState();
        
        // GameAnalytics確認
        GameAnalytics.OnRemoteConfigsUpdatedEvent += MyOnRemoteConfigsUpdateFunction;
#if UNITY_EDITOR
        //_isGameAnalyticsRemoteConfigCompleted = true;
        //StatusMaster.SetTestName("test_A");
#endif
        string testName = "test_A";
        _isGameAnalyticsRemoteConfigCompleted = true;
        StatusMaster.SetTestName(testName);
        // カスタム ディメンション設定
        if (!PlayingDataManager.IsCustomDimension(testName))
        {
            Debug.Log("GameAnalytics:SetCustomDimension:" + testName);
            GameAnalytics.SetCustomDimension01(testName);
            PlayingDataManager.SetCustomDimension(testName);
        }
        //CheckGameAnalytics();

        // サーバーからjsonデータ取得
        //StartCoroutine("JsonLoad");
        _jsonText = "";

        // UnityAnalyticsのリモートコンフィグ用
        /*
        _isRemoteCompleted = false;
        RemoteSettings.Completed += Completed;
        Invoke("FailedFetched", 5.0f);
        RemoteSettings.ForceUpdate();*/
    }

    void Update()
    {
        base.Update();
        
        // ネットワークチェック
        OnCheckNetworkState();

        // GameAnalyticsチェック
        OnCheckGameAnalytics();
        
        // データが読み込まれたら次のSceneを読み込む
        if (!_isDataLoadSucces && _jsonText != null && _isNetwork && _isUnityAnalyticsRemoteConfigCompleted && _isGameAnalyticsRemoteConfigCompleted)
        {
            // Status Load
            StatusMasterLoad(_jsonText);
            
            // Data読み込み完了
            _isDataLoadSucces = true;

            // Scene Load
            SceneChangeManager.ChangManagerScene();
        }
    }

    private IEnumerator JsonLoad()
    {
        // 通信ステータスを設定
        _connectionStatus = (int)CONNECTION_STATE_ENUM.CONNECTION_START;
        
        // urlにタイムスタンプを付与
        var url = URLAddTimeStump.urlAddTimeStump(API_DATA_TABLE);
        using (WWW www = new WWW(url))
        {
            yield return www;
            // コルーチンの開始
            StartCoroutine(startWWWTimeout(www, 20.0f));

            // 正常な処理が行われた場合
            try
            {
                // 通信エラーを検知した場合は例外を投げる
                Debug.Log("Connection Good");
                if (www.error != null)
                {
                    Debug.Log(www.error);
                    Debug.Log("Connection Lost");
                    throw new System.Exception();
                }
                Debug.Log("通信結果 = " + www.text);
                _jsonText = www.text;
                
                // 通信ステータスを設定
                _connectionStatus = (int)CONNECTION_STATE_ENUM.CONNECTION_SUCCESS;
            }
            catch
            {
                // 通信ステータスを設定
                _connectionStatus = (int)CONNECTION_STATE_ENUM.CONNECTION_ERROR;
            }

            // wwwオブジェクトは破棄
            if (www != null)
            {
                www.Dispose();
                System.GC.Collect();
            }
        }
    }

    ////////////////////////////////////////////////////////////////////
    // www処理のタイムアウト用コルーチン
    ////////////////////////////////////////////////////////////////////
    IEnumerator startWWWTimeout(WWW www, float timeout)
    {
        float requestTime = Time.time;
        // 通信が完了していない間は繰り返す
        while (!www.isDone)
        {
            // プログレスの値を取得する
            _connectionProgress = www.progress;
            // なぜかジャスト0.5の値が通信開始のタイミングで入る場合がある
            if (Mathf.Approximately(_connectionProgress, 0.5f))
            {
                _connectionProgress = 0.0f;
            }

            if (www.isDone)
            {
                Debug.Log("www isDone");// 完了
                break;
            }
            else
            {
                if (Time.time - requestTime < timeout)
                {
                    yield return null;
                }
                else
                {
                    Debug.Log("www Timeout"); //タイムアウト
                    break;
                }
            }
        }
        yield return null;
    }

    private void StatusMasterLoad(string jsonText)
    {
        // ステージ情報登録
        Debug.Log("StatusMasterLoad:" + jsonText);
        new StageMaster(1, "Main", 1, null);
    }

    // UnityAnalyticsのロード成功処理
    private void Completed(bool wasUpdatedFromServe, bool settingsChange, int serverResponse)
    {
        Debug.Log("Completed:" + serverResponse);
        _isUnityAnalyticsRemoteConfigCompleted = true;
    }

    // UnityAnalyticsのロード失敗処理
    private void FailedFetched()
    {
        Debug.LogError("not remote data");
        _isUnityAnalyticsRemoteConfigCompleted = true;
    }
    
    // ネットワークの状態確認
    private void OnCheckNetworkState()
    {
        // ネットワークチェック
        if (!_isNetworkChack || _isNetwork)
        {
            return;
        }
        _isNetworkChack = false;
        CheckNetworkState();
        this.CallAfter(_networkChackInterval, delegate
        {
            _isNetworkChack = true;
        });
    }
    private void CheckNetworkState()
    {
        // ネットワークの状態を確認する
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // ネットワークに接続されていない状態
            Debug.Log("ネットワーク:未接続");
            _isNetwork = false;
            _notReachableText.SetActive(true);
        }
        else
        {
            // ネットワークに接続されている状態
            Debug.Log("ネットワーク:接続");
            _isNetwork = true;
        }
    }
    
    
    // GameAnalyticsのロード確認
    public void OnCheckGameAnalytics()
    {
        // GameAnalyticsチェック
        if (!_isGameAnalyticsChack || _isGameAnalyticsRemoteConfigCompleted)
        {
            return;
        }
        _isGameAnalyticsChack = false;
        CheckGameAnalytics();
        this.CallAfter(_gameAnalyticsInterval, delegate
        {
            _isGameAnalyticsChack = true;
        });
    }
    public void CheckGameAnalytics()
    {
        if (!_isGameAnalyticsRemoteConfigCompleted && GameAnalytics.IsRemoteConfigsReady())
        {
            // Remote Config を呼び出す
            _isGameAnalyticsRemoteConfigCompleted = true;
            // string testName = GameAnalytics.GetRemoteConfigsValueAsString("test_name", "test_A");
            string testName = "test_E";
            StatusMaster.SetTestName(testName);
            
            // カスタム ディメンション設定
            if (!PlayingDataManager.IsCustomDimension(testName))
            {
                Debug.Log("GameAnalytics:SetCustomDimension:" + testName);
                GameAnalytics.SetCustomDimension01(testName);
                PlayingDataManager.SetCustomDimension(testName);
            }
            Debug.Log("GameAnalytics:Check:" + testName);
        }
    }
    
    // GameAnalyticsのA・Bテストのイベントが更新されるたびに呼び出される
    private static void MyOnRemoteConfigsUpdateFunction() 
    {
        Debug.Log("GameAnalytics:MyOnRemoteConfigsUpdateFunction:");
        if (GameAnalytics.IsRemoteConfigsReady())
        {
        }
    } 
}