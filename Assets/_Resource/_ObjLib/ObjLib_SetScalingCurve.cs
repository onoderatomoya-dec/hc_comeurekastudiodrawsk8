using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
説明: 指定のスケールまで２次曲線で変化

newSize: 拡大縮小後のsizeを指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib_SetScalingCurve : ObjLib
{
    private int _flame;
    private Vector3 _newScale;
    private Vector3 _scaleValue;
    private Vector3 _nowScale;
    private float _count = 0;
    private float _timeCount = 1;
    private float _a = 1;
    private float _totalValue;

    public ObjLib_SetScalingCurve(GameObject obj, Vector3 newScale, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _count = _flame;
        _newScale = newScale;
        _nowScale = obj.transform.localScale;
        _scaleValue = new Vector3(_newScale.x - _nowScale.x,_newScale.y - _nowScale.y,_newScale.z - _nowScale.z);
        for (int i = 0;i < _flame;i++)
        {
            _totalValue += _a*(i*i);
        }
    }

    public override void Update()
    {
        _count--;
        float value = (_a*(_count*_count)) / _totalValue;
        Transform tran = _gameObject.transform;
        tran.localScale = new Vector3(tran.localScale.x + (_scaleValue.x * value),tran.localScale.y + (_scaleValue.y * value),tran.localScale.z + (_scaleValue.z * value));

        if (_count == 0)
        {
            tran.localScale = new Vector3(_newScale.x,_newScale.y,_newScale.z);
            // 終了処理
            SetRelease();
        }

    }
}
