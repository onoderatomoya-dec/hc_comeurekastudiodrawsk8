using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
説明: オブジェクトに設定されている全てのマテリアルカラーを徐々に指定の色に近づける

coloyType: 使用するタイプ
           通常は「_Color」を使用
color: 0~255の範囲を指定
flame: フレーム 
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib3D_AllColorSetMove : ObjLib
{
    private int _flame;
    private float _count = 0;
    MeshRenderer _renderer;
    Material[] _mats;
    Color _newColor;
    Color _colorValue;
    String _coloyType;

    public ObjLib3D_AllColorSetMove(GameObject obj, String coloyType, Color color, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _coloyType = coloyType;
        _newColor = color;
        _flame = flame;
        _renderer = obj.transform.GetComponent<MeshRenderer>();
        _mats = obj.transform.GetComponent<MeshRenderer>().materials;
        _colorValue = new Color(((color.r - _renderer.material.color.r) / _flame) * ((float)1 / (float)255), ((color.g - _renderer.material.color.g) / _flame) * ((float)1 / (float)255), ((color.b - _renderer.material.color.b) / _flame) * ((float)1 / (float)255),((color.a - _renderer.material.color.a) / _flame) * ((float)1 / (float)255));
    }

    public override void Update()
    {
        _count++;
        float r = _renderer.material.color.r;
        float g = _renderer.material.color.g;
        float b = _renderer.material.color.b;
        float a = _renderer.material.color.a;
        for (int i = 0; i < _mats.Length; i++)
        {
            _mats[i].SetColor(_coloyType, new Color(r + _colorValue.r, g + _colorValue.g, b + _colorValue.b,a + _colorValue.a));
        }
        if (_count == _flame)
        {
            for (int i = 0; i < _mats.Length; i++)
            {
                _mats[i].SetColor(_coloyType, new Color(_newColor.r * ((float)1 / (float)255), _newColor.g * ((float)1 / (float)255), _newColor.b * ((float)1 / (float)255),_newColor.a * ((float)1 / (float)255)));
            }

            // 終了処理
            SetRelease();
        }
    }
}
