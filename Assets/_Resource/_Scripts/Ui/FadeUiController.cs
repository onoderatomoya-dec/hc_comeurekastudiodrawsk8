using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class FadeUiController : SingletonMonoBehaviour<FadeUiController>
{
    [SerializeField] Image _bg;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _bg.color = new Color(_bg.color.r, _bg.color.g, _bg.color.b, 0);
        _bg.gameObject.SetActive(false);
    }

    public void OnFade(Action action,float time = 0.2f)
    {
        // フェードイン
        _bg.gameObject.SetActive(true);
        DOTween.ToAlpha(() => _bg.color,
                        color => _bg.color = color,
                        1.0f,
                        time).SetEase(Ease.InSine);

        // イベント再生
        this.CallAfter(time,delegate
        {
            DOTween.ToAlpha(() => _bg.color,
                        color => _bg.color = color,
                        0.0f,
                        time).SetEase(Ease.InSine);
            _bg.gameObject.SetActive(false);
            action();
        });
    }
}
