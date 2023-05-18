using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using OTCode;

public class ClearUiController : SingletonMonoBehaviour<ClearUiController>
{
    private float _time = 1.0f;
    private Vector3 _defaultPos;
    
    // 初期位置
    protected void Init()
    {
        this.transform.position = _defaultPos;
    }
    
    public void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        _defaultPos = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager._isClear.Subscribe(x =>
        {
            if (x)
            {
                // ハップティック
                //TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Success);

                // ステージを進める
                OnNextStage();

                // 表示
                OnClear(_time);

                // ボタン表示
                this.transform.Find("Button").gameObject.SetActive(true);
            }
            else
            {
                Init();
                this.transform.Find("Button").gameObject.SetActive(false);
            }
        });

        // シーン切り替え
        this.transform.Find("Button").GetComponent<ButtonEvent>().SetEvent(delegate
        {
            SceneChangeManager.ChageScene();
        });
    }
    
    protected float OnClear(float uiTime)
    {
        float addResultTime = 0;
        if (PlayingDataManager._isDebug)
        {
            addResultTime = PlayingDataManager._addResultTime;
        }

        this.CallAfter(uiTime + addResultTime, delegate
        {
            // UIを画面中央に移動
            new ObjLib2D_SetPositionCurve(this.gameObject, new Vector3(0, 0, 0), 20, false, null);
        });

        return uiTime + addResultTime;
    }

    void Update()
    {
    }

    // クリア処理
    public void OnNextStage()
    {
        // ステージを進める
        if (PlayingDataManager._stageMaster != null)
        {
            // 初めてステージをクリアした場合イベント送信
            int id = PlayingDataManager._stageMaster.GetId();
            id++;
            PlayingDataManager.SetStageId(id);
        }
    }
}
