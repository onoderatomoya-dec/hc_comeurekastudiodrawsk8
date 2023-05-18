using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: オブジェクトをバウンドさせる

boundCount: バウンド回数
value: バウンド量
flame: 1バウンド時間
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/

public class ObjLib3D_Bound : ObjLib
{
    private int _flame;
    private float _value;
    private float _weakenValue;
    private float _rot = (Mathf.PI / 2);
    private float _count = 0;
    private float _boundCount = 0;
    private float _maxBound;
    private float _defaultPosY;

    private float _a = 1;
    private float _totalValue = 0;
    private int _curveCount = 0;
    private bool _curveFlag = true;
    public ObjLib3D_Bound(GameObject obj, float value, float weakenValue,int maxBound, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _value = value;
        _maxBound = maxBound;
        _weakenValue = weakenValue;
        _defaultPosY = obj.transform.position.y;
        _curveCount = (int)(_flame / 2);

        for (int i = 0; i < (int)(_flame / 2); i++)
        {
            _totalValue += _a * (i * i);
        }
    }

    public override void Update()
    {
        if (_curveFlag)
        {
            _curveCount--;
        }
        else
        {
            _curveCount++;
        }
        float carveValue = (_a * (_curveCount * _curveCount)) / _totalValue;

        _count++;
        // 上昇処理
        if (_count <= (int)(_flame / 2))
        {
            _rot += ((Mathf.PI / 2) / (float)_flame / 2);
            _gameObject.transform.position = new Vector3(_gameObject.transform.position.x, _gameObject.transform.position.y + (_value * Mathf.Sin(_rot)) * carveValue, _gameObject.transform.position.z);
        }
        if (_count == (int)(_flame / 2))
        {
            _rot = Mathf.PI + Mathf.PI / 2;
            _curveCount = 0;
        }

        // 加工処理
        if (_count > (int)(_flame / 2) && _count <= _flame)
        {
            _rot += (Mathf.PI / 2.0f) / (float)_flame / 2.0f;
            _gameObject.transform.position = new Vector3(_gameObject.transform.position.x, _gameObject.transform.position.y + (_value * Mathf.Sin(_rot)) * carveValue, _gameObject.transform.position.z);
        }
        if (_count == _flame)
        {
            _gameObject.transform.position = new Vector3(_gameObject.transform.position.x, _defaultPosY, _gameObject.transform.position.z);
            _count = 0;
            _rot = Mathf.PI / 2;
            if (_weakenValue > 1.0f)
            {
                _value = _value / _weakenValue;
                _flame = (int)(_flame / _weakenValue);
            }
            _boundCount++;

            _curveCount = (int)(_flame / 2);
            for (int i = 0; i < (int)(_flame / 2); i++)
            {
                _totalValue += _a * (i * i);
            }
        }
        if (_boundCount == _maxBound)
        {
            // 終了処理
            SetRelease();
        }

    }
}
