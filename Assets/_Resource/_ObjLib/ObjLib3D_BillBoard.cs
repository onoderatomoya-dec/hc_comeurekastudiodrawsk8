using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
説明:  ビルボード
何かの子クラスに設定していると不具合になるので注意
*/
public class ObjLib3D_BillBoard : ObjLib
{
    public ObjLib3D_BillBoard(GameObject obj)
    {
        base.Init(obj, null);
    }

    public override void Update()
    {
        /*Vector3 p = Camera.main.transform.position;
		p.y = _gameObject.transform.position.y;
		_gameObject.transform.LookAt (p);*/

        // 補完スピードを決める
        float speed = 1000f;
        // ターゲット方向のベクトルを取得
        Vector3 relativePos = _gameObject.transform.position - Camera.main.transform.position;
        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // 現在の回転情報と、ターゲット方向の回転情報を補完する
        _gameObject.transform.rotation = Quaternion.Slerp(_gameObject.transform.rotation, rotation, speed);
    }
}