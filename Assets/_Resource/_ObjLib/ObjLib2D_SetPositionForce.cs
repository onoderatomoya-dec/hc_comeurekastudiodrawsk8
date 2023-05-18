using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 指定のPositionまで移動量を加算

newPos: 移動先のlocalPositionを指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/

public class ObjLib2D_SetPositionForce : ObjLib
{
    private int _flame;
    private Vector3 _vec;
    private float _count = 0;
    private float _timeValur;
    private float _timeCount = 1;

    public ObjLib2D_SetPositionForce(GameObject obj, Vector3 vec, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _vec = vec;
        _timeValur = 1.0f / (float)_flame;
    }

    public override void Update()
    {
        Vector3 pos = _gameObject.transform.localPosition;
        _gameObject.transform.localPosition = new Vector3(pos.x + _vec.x,pos.y + _vec.y,pos.z + _vec.z);
        _timeCount -= _timeValur;
        _vec = new Vector3(_vec.x * _timeCount,_vec.y * _timeCount,_vec.z * _timeCount);

        _count++;
        if (_count == _flame)
        {
            // 終了処理
            SetRelease();
        }

    }
}
