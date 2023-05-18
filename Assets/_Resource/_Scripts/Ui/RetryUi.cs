using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class RetryUi : SingletonMonoBehaviour<RetryUi>
{
    void Start()
    {
        GameManager._isStart.Subscribe(x =>
        {
            if (x)
            {
                this.transform.Find("RetryUi").gameObject.SetActive(false);
            }
            else
            {
                this.transform.Find("RetryUi").gameObject.SetActive(false);

            }
        });
    }

    public void OnRetry()
    {
        if (GameManager._isClear.Value || GameManager._isMiss.Value)
        {
            return;
        }
        SceneChangeManager.ChageScene();
    }
}
