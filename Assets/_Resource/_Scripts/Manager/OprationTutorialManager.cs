using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class OprationTutorialManager : SingletonMonoBehaviour<OprationTutorialManager>
{
    // チュートリアル
    [SerializeField] private GameObject _contents;
    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }

        // Awakeメソッドは必ずここから初期化処理を行う
        GameManager._tutorialName.Subscribe(s =>
        {
            _contents.SetActive(true);
            if (s == null)
            {
                return;
            }
            foreach (Transform tran in _contents.transform)
            {
                tran.gameObject.SetActive(false);
            }
            if (s == "")
            {
                _contents.transform.Find("TapToStart").gameObject.SetActive(true);
            }
            else
            {
                if (_contents.transform.Find(s).transform != null)
                {
                    _contents.transform.Find(s).gameObject.SetActive(true);
                }
                else
                {
                    _contents.transform.Find("TapToStart").gameObject.SetActive(true);
                }
            }
        });
    }

    void OnEnable()
    {
        base.OnEnable();
    }

    void Start()
    {
        base.Start();
    }

    public void SetContents(bool flag)
    {
        _contents.SetActive(flag);
    }
}
