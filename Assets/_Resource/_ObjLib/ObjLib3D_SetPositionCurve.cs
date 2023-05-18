using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
説明: 指定の位置まで２次曲線で移動

newPos: 移動後のposを指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib3D_SetPositionCurve : ObjLib
{
    private int _flame;
    private Vector3 _newPos;
    private Vector3 _posValue;
    private Transform _tran;
    private float _count = 0;
    private float _timeCount = 1;
    private float _a = 1;
    private float _totalValue;
    private bool _isFirstSlow;

    public ObjLib3D_SetPositionCurve(GameObject obj, Vector3 newPos, int flame,bool isFirstSlow, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _newPos = newPos;
        _tran = obj.transform;
        _isFirstSlow = isFirstSlow;
        _posValue = new Vector3(newPos.x - _tran.position.x, newPos.y - _tran.position.y, newPos.z - _tran.position.z);
        for (int i = 0; i < _flame; i++)
        {
            _totalValue += _a * (i * i);
        }

        if(!_isFirstSlow)
        {
            _count = _flame;
        }
    }
    public override void Update()
    {
        if(_isFirstSlow)
        {
            _count++;
        }
        else
        {
            _count--;
        }
        float value = (_a * (_count * _count)) / _totalValue;
        _tran.position = new Vector3(_tran.position.x + (_posValue.x * value), _tran.position.y + (_posValue.y * value),_tran.position.z + (_posValue.z * value));

        if (_count == 0 || _count == _flame)
        {
            //_tran.position = new Vector3(_newPos.x, _newPos.y,_newPos.z);
            // 終了処理
            SetRelease();
        }

    }
}
