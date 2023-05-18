using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class StartButtonUiController : SingletonMonoBehaviour<StartButtonUiController>
{
    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        // Awakeメソッドは必ずここから初期化処理を行う
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager._isStart.Subscribe(x =>
        {
            if (x)
            {
                Debug.Log("StartButtonUiController:_isStart:true");
                // チュートリアルを消す
                OprationTutorialManager.Instance.SetContents(false);
            }
            else
            {
                Debug.Log("StartButtonUiController:_isStart:false");
                // チュートリアルを表示
                OprationTutorialManager.Instance.SetContents(true);
            }
        });
    }

    void Update()
    {
        //ここにタップされた時の処理を書く
        if (!GameManager._isStart.Value && Input.GetMouseButtonUp(0))
        {
            Debug.Log("StartButtonUiController:OnStartEvent");
            GameManager.OnStartEvent();
        }
    }
}
