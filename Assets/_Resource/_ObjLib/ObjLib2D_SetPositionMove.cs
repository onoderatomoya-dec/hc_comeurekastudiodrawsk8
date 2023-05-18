using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 指定のPositionまで移動

newPos: 移動先のlocalPositionを指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/

public class ObjLib2D_SetPositionMove : ObjLib
{
    private int _flame;
    private Vector3 _moveValue;
    private Vector3 _newPos;
    private float _count = 0;
    public ObjLib2D_SetPositionMove(GameObject obj, Vector3 newPos, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _newPos = newPos;
        Vector3 oldPos = obj.transform.localPosition;
        _moveValue = new Vector3(( newPos.x - oldPos.x) / _flame, (newPos.y - oldPos.y) / _flame, (newPos.y - oldPos.y) / _flame);
    }

    public override void Update()
    {
        Vector3 pos = _gameObject.transform.localPosition;
        _gameObject.transform.localPosition = new Vector3(pos.x + _moveValue.x,pos.y + _moveValue.y,pos.z + _moveValue.z);
        _count++;
        if (_count == _flame)
        {
            _gameObject.transform.localPosition = new Vector3(_newPos.x,_newPos.y,_newPos.z);
            // 終了処理
            SetRelease();
        }

    }
}
