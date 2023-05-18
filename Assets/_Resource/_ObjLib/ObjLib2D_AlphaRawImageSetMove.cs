using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/* 
説明: 指定したFlameで指定のAlpha値に変更

alpha: 遷移後のAlpha値指定(0.0f〜1.0f)
flame: フレーム 
loop: ループ
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib2D_AlphaRawImageSetMove : ObjLib
{
    private float _count = 0;
    private int _flame;
    private float _alpha;
    private float _value;
    private float _objAlpha;
    private RawImage _image;
    private bool _loop;
    private int _loopCount = 0;
    public ObjLib2D_AlphaRawImageSetMove(GameObject obj,float alpha,int flame,bool loop, Action releaseEvent)
    {
        base.Init(obj,releaseEvent);
        _flame = flame;
        _alpha = alpha;
        _loop = loop;
        _image = obj.transform.GetComponent<RawImage>();
        _objAlpha =  _image.color.a;
        _value = (alpha - _image.color.a) / _flame;
    }

    public override void Update()
    {
        _count ++;
        _image.color = new Color(_image.color.r,_image.color.g,_image.color.b,_image.color.a + _value);
        if (_count == _flame)
        {
            if (_loop)
            {
                // 偶数の処理
                if (_loopCount % 2 == 0)
                {
                    _image.color = new Color(_image.color.r,_image.color.g,_image.color.b,_alpha);
                }
                else
                {
                    _image.color = new Color(_image.color.r,_image.color.g,_image.color.b,_objAlpha);
                }
                _loopCount ++;
                _value = _value * -1;
                _count = 0;
            }
            else
            {
                _image.color = new Color(_image.color.r,_image.color.g,_image.color.b,_alpha);
                // 終了処理
                SetRelease();
            }
        }
    }
}
