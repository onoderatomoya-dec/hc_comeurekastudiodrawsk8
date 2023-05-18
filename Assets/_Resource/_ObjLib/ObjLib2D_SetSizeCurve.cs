using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
説明: 指定のサイズまで２次曲線で変化

newSize: 拡大縮小後のsizeを指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib2D_SetSizeCurve : ObjLib
{
    private int _flame;
    private Vector2 _newSize;
    private Vector2 _sizeValue;
    private RectTransform _rect;
    private float _count = 0;
    private float _timeCount = 1;
    private float _a = 1;
    private float _totalValue;

    public ObjLib2D_SetSizeCurve(GameObject obj, Vector2 newSize, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _count = _flame;
        _newSize = newSize;
        _rect = obj.GetComponent<RectTransform>();
        _sizeValue = new Vector2(newSize.x - _rect.sizeDelta.x,newSize.y - _rect.sizeDelta.y);
        for (int i = 0;i < _flame;i++)
        {
            _totalValue += _a*(i*i);
        }
    }

    public override void Update()
    {
        _count--;
        float value = (_a*(_count*_count)) / _totalValue;
        _rect.sizeDelta = new Vector2(_rect.sizeDelta.x + (_sizeValue.x * value),_rect.sizeDelta.y + (_sizeValue.y * value));

        if (_count == 0)
        {
            _rect.sizeDelta = new Vector2(_newSize.x,_newSize.y);
            // 終了処理
            SetRelease();
        }
    }

    public void AddSize(Vector2 newSize)
    {
        _count = _flame;
        _newSize = newSize;
        _sizeValue = new Vector2(newSize.x - _rect.sizeDelta.x,newSize.y - _rect.sizeDelta.y);
    }
}
