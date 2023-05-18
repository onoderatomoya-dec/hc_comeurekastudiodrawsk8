using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using OTCode;

public class MissUiController : SingletonMonoBehaviour<MissUiController>
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

    void Start()
    {
        GameManager._isMiss.Subscribe(x =>
        {
            if (x)
            {
                // ハップティック
                //TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Error);

                // 表示
                OnMiss(_time);
            }
            else
            {
                Init();
            }
        });

        // シーン切り替え
        this.transform.Find("Button").GetComponent<ButtonEvent>().SetEvent(delegate
        {
            SceneChangeManager.ChageScene();
        });

    }
    
    void Update()
    {
    }

    
    protected void OnMiss(float uiTime)
    {
        float addResultTime = 0;
        if (PlayingDataManager._isDebug)
        {
            addResultTime = PlayingDataManager._addResultTime;
        }

        this.CallAfter(uiTime + addResultTime,delegate
        {
            // UIを画面中央に移動
            new ObjLib2D_SetPositionCurve(this.gameObject, new Vector3(0, 0, 0), 20, false, null);
        });
    }
}
