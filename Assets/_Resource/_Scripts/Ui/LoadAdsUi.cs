using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;
using DG.Tweening;

public class LoadAdsUi : SingletonMonoBehaviour<LoadAdsUi>
{
    [SerializeField] private GameObject _uiObject;

    [SerializeField] private GameObject _loadImageObject;
    
    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        // Awakeメソッドは必ずここから初期化処理を行う
    }
    void Start()
    {
        _uiObject.SetActive(false);
        
        // ロードUIの回転
        _loadImageObject.transform.DOLocalRotate(new Vector3(0, 0, 360), 1.2f, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCirc)
            .SetLoops(-1, LoopType.Restart);
    }

    public void Show()
    {
        _uiObject.SetActive(true);
    }
    
    public void Hide()
    {
        _uiObject.SetActive(false);
    }
}
