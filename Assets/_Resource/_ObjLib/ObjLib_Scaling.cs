using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: オブジェクトの拡大縮小処理

flame: フレーム 
moveValue: 拡大縮小量
stopFlame: ループ次に一時停止する場合使用
isLoop: ループ
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib_Scaling : ObjLib
{
    private int _flame;
    private Vector3 _moveValue;
    private float _rot = 0.0f;
    private float _count = 0;
    private int _stopFlame = 0;
    private bool _isLoop;
    private Vector3 _defaultScale;
    Transform _trn;
    public ObjLib_Scaling(GameObject obj, int flame, Vector3 moveValue, bool isLoop, int stopFlame,Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _trn = obj.transform;
        _defaultScale = _trn.localScale;
        _moveValue = moveValue;
        _flame = flame;
        _stopFlame = stopFlame;
        _isLoop = isLoop;
    }

    public override void Update()
    {

        if (_count >= _flame && _count <= _flame + _stopFlame)
        {
            _trn.localScale = _defaultScale;
            if (_count >= _flame + _stopFlame -1)
            {
                _count = 0;
                _rot = 0.0f;
            }
            _count++;
            return;
        }

        _rot += (Mathf.PI * 2) / _flame;
        _trn.localScale = new Vector3(_trn.localScale.x + _moveValue.x * Mathf.Sin(_rot), _trn.localScale.y + _moveValue.y * Mathf.Sin(_rot), _trn.localScale.z + _moveValue.z * Mathf.Sin(_rot));

        _count++;
        if (_count >= _flame)
        {
            if (!_isLoop)
            {
                _trn.localScale = _defaultScale;
                // 終了処理
                SetRelease();
            }
        }
    }

}
