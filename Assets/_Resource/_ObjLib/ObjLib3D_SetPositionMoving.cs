using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 前後左右にループの動きを行う

value: 移動量
flame: 1ループにかかるフレーム 
isLoop: ループするかどうか
loopCount: ループする回数(-1を指定すれば無限にループする)
loopRate: ループする際にflameとvalueの減衰量を指定(半分にする場合は、0.5と記載)
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib3D_SetPositionMoving : ObjLib
{
    private int _flame;
    private Vector3 _value;
    private float _count = 0;
    private bool _isLoop;
    private int _loopCount;
    private float _loopRate;
    private Vector3 _rot;
    private Vector3 _defaultRot;
    private Vector3 _oldPos;
    public ObjLib3D_SetPositionMoving(GameObject obj, Vector3 value, Vector3 defaultRot, int flame, bool isLoop, int loopCount, float loopRate, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _value = value;
        _rot = _defaultRot = defaultRot;
        _isLoop = isLoop;
        _loopCount = loopCount;
        _loopRate = loopRate;
        _oldPos = obj.transform.position;
    }

    public override void Update()
    {
        _rot.x += (float)(Math.PI * 2.0f) / (float)_flame;
        _rot.y += (float)(Math.PI * 2.0f) / (float)_flame;
        _rot.z += (float)(Math.PI * 2.0f) / (float)_flame;
        Vector3 pos = _gameObject.transform.position;
        _gameObject.transform.position = new Vector3(pos.x + (float)Math.Sin(_rot.x) * _value.x, pos.y + (float)Math.Sin(_rot.y) * _value.y, pos.z + (float)Math.Sin(_rot.z) * _value.z);
        _count++;
        if (_count == _flame && !_isLoop)
        {
            _count = 0;
            _rot = _defaultRot;
            _gameObject.transform.position = _oldPos;


            if (_isLoop)
            {
                return;
            }
            // 終了処理
            SetRelease();
        }
        else if (_count == _flame)
        {
            // ループ回数を指定していた場合の処理
            _count = 0;
            _rot = _defaultRot;
            _loopCount--;
            //_gameObject.transform.position = _oldPos;

            if (_loopCount > 0)
            {
                _flame = (int)(_flame * _loopRate);
                _rot.x += (float)(Math.PI * 2.0f) / (float)_flame;
                _rot.y += (float)(Math.PI * 2.0f) / (float)_flame;
                _rot.z += (float)(Math.PI * 2.0f) / (float)_flame;

                _value = _value * _loopRate;

            }
            else if (_loopCount == 0)
            {
                // 終了処理
                SetRelease();
                return;
            }
        }
    }
}
