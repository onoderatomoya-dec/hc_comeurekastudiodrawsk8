using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
説明: オブジェクトに設定されている一つのマテリアルカラーを徐々に指定の色に近づける
     複数のマテリアルが使用されている場合は予期せぬ挙動をする可能性あり

obj: Materialが使用されているGameObjectを指定
mat: 変更したいオブジェクトのマテリアルを指定
coloyType: 使用するタイプ
           通常は「_Color」を使用
color: 0~1の範囲を指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib3D_ColorSetMove : ObjLib
{
    private int _flame;
    private float _count = 0;
    Material _mat;
    Color _newColor;
    Color _colorValue;
    String _coloyType;

    public ObjLib3D_ColorSetMove(GameObject obj,Material mat, String coloyType, Color color, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _coloyType = coloyType;
        _newColor = color;
        _flame = flame;
        _mat = mat;
        _colorValue = new Color((color.r - mat.GetColor(_coloyType).r) / _flame, (color.g - mat.GetColor(_coloyType).g) / _flame, (color.b - mat.GetColor(_coloyType).b) / _flame,(color.a - mat.GetColor(_coloyType).a) / _flame);
    }

    public override void Update()
    {
        _count++;
        float r =_mat.GetColor(_coloyType).r;
        float g =_mat.GetColor(_coloyType).g;
        float b =_mat.GetColor(_coloyType).b;
        float a =_mat.GetColor(_coloyType).a;

        _mat.SetColor(_coloyType, new Color(r + _colorValue.r, g + _colorValue.g, b + _colorValue.b,a + _colorValue.a));
        
        if (_count == _flame)
        {
            _mat.SetColor(_coloyType, new Color(_newColor.r, _newColor.g, _newColor.b,_newColor.a));
        
            // 終了処理
            SetRelease();
        }
    }
}
