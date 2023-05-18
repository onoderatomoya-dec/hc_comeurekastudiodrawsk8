using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjLib3D_LighiColorSetMove : ObjLib
{
/*
説明: ライトのカラーを変更する処理

obj: LighiのGameObjectを指定
color: 0~1の範囲を指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
    private int _flame;
    private float _count = 0;
    Color _newColor;
    Color _colorValue;
    Light _light;

    public ObjLib3D_LighiColorSetMove(GameObject obj, Color color, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _newColor = color;
        _flame = flame;
        _light = obj.GetComponent<Light>();
        _colorValue = new Color((color.r - _light.color.r) / _flame, (color.g - _light.color.g) / _flame, (color.b - _light.color.b) / _flame,(color.a - _light.color.a) / _flame);
    }

    public override void Update()
    {
        _count++;
        float r = _light.color.r;
        float g = _light.color.g;
        float b = _light.color.b;
        float a = _light.color.a;

        _light.color = new Color(r + _colorValue.r, g + _colorValue.g, b + _colorValue.b,a + _colorValue.a);
        
        if (_count == _flame)
        {
            _light.color = new Color(_newColor.r, _newColor.g, _newColor.b,_newColor.a);
        
            // 終了処理
            SetRelease();
        }
    }
}
