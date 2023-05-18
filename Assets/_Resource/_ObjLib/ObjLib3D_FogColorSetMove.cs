using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjLib3D_FogColorSetMove : ObjLib
{
    /*
    説明: ライトのカラーを変更する処理

    obj: 消えることのないオブジェクト指定
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

    public ObjLib3D_FogColorSetMove(GameObject obj, Color color, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _newColor = color;
        _flame = flame;
        _colorValue = new Color((color.r - RenderSettings.fogColor.r) / _flame, (color.g - RenderSettings.fogColor.g) / _flame, (color.b - RenderSettings.fogColor.b) / _flame, (color.a - RenderSettings.fogColor.a) / _flame);
    }

    public override void Update()
    {
        _count++;
        float r = RenderSettings.fogColor.r;
        float g = RenderSettings.fogColor.g;
        float b = RenderSettings.fogColor.b;
        float a = RenderSettings.fogColor.a;

        RenderSettings.fogColor = new Color(r + _colorValue.r, g + _colorValue.g, b + _colorValue.b, a + _colorValue.a);

        if (_count == _flame)
        {
            RenderSettings.fogColor = new Color(_newColor.r, _newColor.g, _newColor.b, _newColor.a);

            // 終了処理
            SetRelease();
        }
    }

}
