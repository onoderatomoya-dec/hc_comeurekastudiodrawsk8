using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 指定した角度まで、指定したフレーム時間で回転

newRot: 回転角(eulerAnglesを指定、または-360から360で指定)
flame: フレーム
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib_SetRotation : ObjLib
{
    private float _count = 0;
    private float _flame = 0;
    private bool _isLocal;
    Vector3 _rotValue;
    Vector3 _newRot;

    public ObjLib_SetRotation(GameObject obj, Vector3 newRot, int flame,bool isLocal,Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _isLocal = isLocal;

        Vector3 newRot180 = CustomUtil.GetRotation180(newRot);
        _newRot = newRot180;
        Vector3 oldRot180;
        if (isLocal)
        {
            oldRot180 = CustomUtil.GetRotation180(obj.transform.localRotation.eulerAngles);
        }
        else
        {
            oldRot180 = CustomUtil.GetRotation180(obj.transform.rotation.eulerAngles);
        }
        _rotValue = CustomUtil.GetShortRotation(oldRot180, newRot180, flame);
    }

    public override void Update()
    {
        Vector3 angle = _gameObject.transform.localEulerAngles;
        if (_isLocal)
        {
            _gameObject.transform.localRotation = Quaternion.Euler(angle.x + _rotValue.x, angle.y + _rotValue.y, angle.z + _rotValue.z);
        }
        else
        {
            _gameObject.transform.rotation = Quaternion.Euler(angle.x + _rotValue.x, angle.y + _rotValue.y, angle.z + _rotValue.z);
        }

        _count++;
        if (_count == _flame)
        {
            if (_isLocal)
            {
                _gameObject.transform.localRotation = Quaternion.Euler(_newRot.x, _newRot.y, _newRot.z);
            }
            else
            {
                _gameObject.transform.rotation = Quaternion.Euler(_newRot.x, _newRot.y, _newRot.z);
            }
            // 終了処理
            SetRelease();
        }
    }
}
