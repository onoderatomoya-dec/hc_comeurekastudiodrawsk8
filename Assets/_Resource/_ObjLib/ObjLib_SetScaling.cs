using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 指定のscaleまで拡大または縮小

newScale: スケールを指定する
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/

public class ObjLib_SetScaling : ObjLib
{
    private int _flame;
    private Vector3 _value;
    private Vector3 _newScale;
    private float _count = 0;
    public ObjLib_SetScaling(GameObject obj, Vector3 newScale, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _newScale = newScale;
        Vector3 oldScale = obj.transform.localScale;
        _value = new Vector3(( _newScale.x - oldScale.x) / (float)_flame, (_newScale.y - oldScale.y) / (float)_flame, (_newScale.z - oldScale.z) / (float)_flame);
        Debug.LogError("z:" +_value.z);
    }

    public override void Update()
    {
        Vector3 scale = _gameObject.transform.localScale;
        _gameObject.transform.localScale = new Vector3(scale.x + _value.x,scale.y + _value.y,scale.z + _value.z);
        _count++;
        if (_count == _flame)
        {
            _gameObject.transform.localScale = new Vector3(_newScale.x,_newScale.y,_newScale.z);
            // 終了処理
            SetRelease();
        }

    }
}
