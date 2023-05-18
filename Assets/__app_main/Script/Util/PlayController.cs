using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

namespace Main
{
    public class PlayController :  SingletonMonoBehaviour<PlayController>
    {
        // メインカメラ
        [SerializeField] public Camera _mainCamera;
        
        // クリアエフェクト
        [SerializeField] private GameObject _clearEffect;
        
        // バーチャルカメラ
        [SerializeField] private Cinemachine.CinemachineVirtualCamera[] _virtualCameras;

        // Ingame
        [SerializeField] public GameObject _gameContents;
        
        // プレイヤー
        //[SerializeField] public Player _player;
        
        // 操作ができるかフラグ
        private bool _isOperation = false;
        
        void Awake()
        {
            if (!base.Awake())
            {
                return;
            }
            // Awakeメソッドは必ずここから初期化処理を行う
        }

        void OnEnable()
        {
            base.OnEnable();
        }
        
        void Start()
        {
            base.Start();

            // ゲームデータ初期化
            InitData();
        }
        
        public void InitData()
        { 
            GameManager._id.Value = 1;
            GameManager._stageId.Value = 2;
            GameManager._stageIndexId = GameManager._stageId.Value - 1;

            // 初期化
            if (PlayingDataManager._stageMaster != null)
            {
                GameManager._id.Value = PlayingDataManager._stageMaster.GetId();
                GameManager._stageId.Value = PlayingDataManager._stageMaster.GetStageId();
                GameManager._stageIndexId = GameManager._stageId.Value - 1;
            }
            else
            {
                // ステージデータ
                /*JSONObject json = new JSONObject(_testJson);
                string abText = new JSONObject(json.GetField("stage_1").ToString()).GetField("B").ToString();
                JSONObject dataJson = new JSONObject(abText);
                JsonData jsonData;
                foreach (string key in dataJson.keys)
                {
                    int id = int.Parse(key);
                    jsonData = new JsonData(new JSONObject(dataJson.GetField(key).ToString()));
                    new StageMaster(id, "Castle", id, jsonData);
                }*/
            }

            // ゲーム情報登録
            GameManager.SetStartEvent(SceneManager.GetActiveScene().name,/*"IO"*/"", delegate
            {
                // 初期化処理
                Init();
            }, delegate
            {
                // ゲーム開始処理
                GameStart();
            });
        }
        
        public void Init()
        {
            Debug.Log("PlayController:Init");

            // インターステイシャル設定
            GameManager.SetShowInterstitialAction(delegate
            {
                ShowInterstitial();
            }); 
            
            // 即時ゲーム開始
            GameStart();

            // 初回機動処理
            if (PlayingDataManager.IsFirstVersion())
            {
                // 初回起動時のバージョン指定
                AnalyticsManager.SendEvent("DlVersion_" + Application.version);
                
                // ゲーム進捗イベント送信
                AnalyticsManager.SendCompleteEvent();
            }

            // 画角調整
            if (IsVertically(Screen.width, Screen.height))
            {
                for (int i = 0; i < _virtualCameras.Length; i++)
                {
                    _virtualCameras[i].m_Lens.FieldOfView =  _virtualCameras[i].m_Lens.FieldOfView + 5.0f;
                }
            }
            
#if UNITY_IOS
#endif
#if UNITY_ANDROID
#endif
        }
        
        public void GameStart()
        {
            if (GameManager._isStart.Value)
            {
                return;
            }
            Debug.Log("GameStart");
            _isOperation = true;
        }
        
        void Update()
        {
            base.Update();
            
            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchPhaseBegan();
            }
            // 離した瞬間
            if (Input.GetMouseButtonUp(0))
            {
                OnTouchPhaseEnded();
            }
            // 押し続けている
            if (Input.GetMouseButton(0))
            {
                OnTouchPhaseMoved();
            }
        }

        // 押した瞬間
        private void OnTouchPhaseBegan()
        {
            
        }
        
        // 離した瞬間
        private void OnTouchPhaseEnded()
        {
            if (!GameManager._isStart.Value)
            {
                return;
            }
        }
        
        // 押し続けている
        private void OnTouchPhaseMoved()
        {
            if (!GameManager._isStart.Value)
            {
                return;
            }
        }
        
        // レイキャスト判定
        private List<GameObject> OnRaycast()
        {
            // クリックした位置からRayを飛ばす
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance = 50; // 飛ばす&表示するRayの長さ
            float duration = 3;   // 表示期間（秒）
            Debug.DrawRay (ray.origin, ray.direction * distance, Color.red, duration, false);
            List<GameObject> objectList = new List<GameObject>();
            foreach (RaycastHit hitRay in Physics.RaycastAll(ray))
            {
                objectList.Add(hitRay.transform.gameObject);
            }
            return objectList;
        }
        
        void FixedUpdate()
        {
        }
        
        private bool IsVertically(float width, float height)
        {
            if ((float)(height / width) >= 2.0)
            {
                Debug.Log("縦長");
                return true;
            }
            else
            {
                return false;
            }
        }
        
        // クリア処理
        public void OnClear()
        {
            if (GameManager._isClear.Value || GameManager._isMiss.Value)
            {
                return;
            }

            this.CallAfter(0, delegate
            {
                _clearEffect.SetActive(true);
            });
            
            GameManager._isClear.Value = true;
            _isOperation = false;
        }
        
        // 失敗処理
        public void OnMiss()
        {
            if (GameManager._isMiss.Value || GameManager._isClear.Value)
            {
                return;
            }

            GameManager._isMiss.Value = true;
            _isOperation = false;
        }
        

        // リワードの読み込みが完了しているか確認
        public bool IsRewardedAdReady()
        {
            if (MaxManager.instance == null)
            {
                return true;
            }

            return MaxManager.Instance.IsRewardedAdReady();
        }
        
        // リワード表示
        public void ShowRewardedAd(Action action,Action failedAdAction = null)
        {
            MaxManager.Instance.SetRewardAction(action);
            MaxManager.Instance.SetFailedAdAction(failedAdAction);
            MaxManager.Instance.ShowRewardedAd();
        }
        
        // インステ表示
        public void ShowInterstitial()
        {
            MaxManager.Instance.ShowInterstitial(); 
        }
    }
}