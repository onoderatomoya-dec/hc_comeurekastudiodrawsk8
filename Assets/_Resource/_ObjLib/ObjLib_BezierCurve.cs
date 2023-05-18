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
public class ObjLib_BezierCurve : ObjLib
{
    private int _flame;
    private float _count = 0;
    private Transform _tran;
    float _px = 0;
    float _py = 0;
    float _pz = 0;
    float[] _x,_y,_z;

    Vector3 _p0;
    Vector3 _p1;
    Vector3 _endPos;

    public ObjLib_BezierCurve(GameObject obj, Vector3 p0,Vector3 p1,Vector3 endPos, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _tran = obj.transform;
        _flame = flame;
        Vector3 pos = obj.transform.position;
        _p0 = p0;
        _p1 = p1;
        _endPos = endPos;
        _x = new float[]{pos.x,_p0.x,_p1.x,_endPos.x}; 
        _y = new float[]{pos.y,_p0.y,_p1.y,_endPos.y};
        _z = new float[]{pos.z,_p0.z,_p1.z,_endPos.z};
    }
    public override void Update()
    {
        _count++;
        float b = (float)_count/_flame;
        float a = 1-b;
        _px = a*a*a*_x[0] + 3*a*a*b*_x[1] + 3*a*b*b*_x[2] + b*b*b*_x[3];
        _py = a*a*a*_y[0] + 3*a*a*b*_y[1] + 3*a*b*b*_y[2] + b*b*b*_y[3];
        _pz = a*a*a*_z[0] + 3*a*a*b*_z[1] + 3*a*b*b*_z[2] + b*b*b*_z[3];

        _tran.position = new Vector3(_px,_py,_pz);
        if (_count == _flame)
        {
            // 終了処理
            SetRelease();
        }
    }

}
