using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 対象を左右に少し回転させることで、揺れているような表現を行う

rotVec: 回転のベクトル
flame: 1回転にかかる時間
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/

public class ObjLib_RotShaking : ObjLib
{
    private float _count = 0;
    private Vector3 _rotVec;
    private Vector3 _localAngles; 
    private float _rot = Mathf.PI/ 2;
    private int _flame;
    private int _stopFlame;
    public ObjLib_RotShaking(GameObject obj,Vector3 rotVec,int flame, int stopFlame,Action releaseEvent)
    {
        base.Init(obj,releaseEvent);
        _rotVec = rotVec;
        _flame = flame;
        _stopFlame = stopFlame;
        _localAngles = obj.transform.localEulerAngles;
    }

    public override void Update()
    {
        _count ++;
        if (_stopFlame > 0 && _count >= (_flame * 2))
        {
            _gameObject.transform.localRotation = Quaternion.Euler(_localAngles.x, _localAngles.y, _localAngles.z);
            if (_count == (_flame * 2) + _stopFlame)
            {
                _count = 0;
                _rot = Mathf.PI/ 2;
            }
            return;
        }

        _rot += (Mathf.PI * 2) / _flame;
        Vector3 value = new Vector3(_rotVec.x * Mathf.Sin(_rot),_rotVec.y * Mathf.Sin(_rot),_rotVec.z * Mathf.Sin(_rot));
        _gameObject.transform.Rotate(value);
    }
}
