using System.Collections;
using System.Collections.Generic;
using UnityEngine;using System;

/* 
説明: スケールを0にし、徐々に指定のスケールまで拡大して少し小さくする

newScale: スケールを指定する
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/


public class ObjLib2D_SetScalingPopIcon : ObjLib
{
    private int _flame;
    private int _subFlame;
    private Vector3 _addScale;
    private Vector3 _nowScale;
    private float _count = 0;
    private float _rot = 0;
    public ObjLib2D_SetScalingPopIcon(GameObject obj, Vector3 newScale, int flame,int subFlame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _subFlame = subFlame;
        _nowScale = obj.transform.localScale;
        _addScale = new Vector3(newScale.x - _nowScale.x, newScale.y - _nowScale.y, newScale.z - _nowScale.z);
    }

    public override void Update()
    {
        _rot += (Mathf.PI / 2) / _flame;
        _gameObject.transform.localScale = new Vector3(_nowScale.x + _addScale.x * Mathf.Sin(_rot), _nowScale.y + _addScale.y * Mathf.Sin(_rot), _nowScale.z + _addScale.z * Mathf.Sin(_rot));
        _count++;
        if (_count == _flame + _subFlame)
        {
            // 終了処理
            SetRelease();
        }

    }
}
