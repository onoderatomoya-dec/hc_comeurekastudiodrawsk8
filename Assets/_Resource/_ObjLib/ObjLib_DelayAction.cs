using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
説明: 指定したFlame後に処理を実行

flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib_DelayAction : ObjLib
{
    private float _count = 0;
    private int _flame;
    public ObjLib_DelayAction(int flame, Action releaseEvent)
    {
        base.Init(null,releaseEvent);
        _flame = flame;
        _isDelayAction = true;
    }

    public override void Update()
    {
        _count++;
        if (_count >= _flame)
        {
            // 終了処理
            SetRelease();
        }
    }
}
