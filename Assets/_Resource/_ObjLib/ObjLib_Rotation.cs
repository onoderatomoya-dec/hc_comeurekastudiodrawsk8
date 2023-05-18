using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 指定した向きに回転

rotVec: 回転のベクトル
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib_Rotation : ObjLib
{
    private float _count = 0;
    private Vector3 _rotVec;
    public ObjLib_Rotation(GameObject obj,Vector3 rotVec, Action releaseEvent)
    {
        base.Init(obj,releaseEvent);
        _rotVec = rotVec;
    }

    public override void Update()
    {
        _gameObject.transform.Rotate(_rotVec);
    }
}
