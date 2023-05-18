using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
説明: 指定の位置までベジェ曲線で移動

P0: StartPosに作用する点
P1: EndPosに作用する点
EndPos: 到着点
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/

public class ObjLib_BezierCurveUpdate : ObjLib
{
    private int _flame;
    private float _count = 0;
    private Transform _tran;
    float _px = 0;
    float _py = 0;
    float _pz = 0;
    float[] _x, _y, _z;

    Vector3 _pos;
    Vector3 _p0;
    GameObject _p1Obj;
    GameObject _endObj;

    public ObjLib_BezierCurveUpdate(GameObject obj, Vector3 p0, GameObject p1Obj, GameObject endObj, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _tran = obj.transform;
        _flame = flame;
        _pos = obj.transform.position;
        _p0 = p0;
        _p1Obj = p1Obj;
        _endObj = endObj;

    }
    public override void Update()
    {
        _x = new float[] { _pos.x, _p0.x, _p1Obj.transform.position.x, _endObj.transform.position.x };
        _y = new float[] { _pos.y, _p0.y, _p1Obj.transform.position.y, _endObj.transform.position.y };
        _z = new float[] { _pos.z, _p0.z, _p1Obj.transform.position.z, _endObj.transform.position.z };

        _count++;
        float b = (float)_count / _flame;
        float a = 1 - b;
        _px = a * a * a * _x[0] + 3 * a * a * b * _x[1] + 3 * a * b * b * _x[2] + b * b * b * _x[3];
        _py = a * a * a * _y[0] + 3 * a * a * b * _y[1] + 3 * a * b * b * _y[2] + b * b * b * _y[3];
        _pz = a * a * a * _z[0] + 3 * a * a * b * _z[1] + 3 * a * b * b * _z[2] + b * b * b * _z[3];

        _tran.position = new Vector3(_px, _py, _pz);
        if (_count == _flame)
        {
            // 終了処理
            SetRelease();
        }
    }

}
