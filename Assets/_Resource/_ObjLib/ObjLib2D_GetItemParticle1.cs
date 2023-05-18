using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
説明: 指定の位置まで２次曲線で移動

obj: 破棄されることのないオブジェクト指定
tran: パーティクルの親オブジェクト指定
prefab: パーティクルで使用するPrefab
staerPos: パーティクル発生posを指定
flame: パーティクル発生から終了までのフレーム
releaseEvent: Release時に実行されるイベント登録
              必要なければnullを
              利用する場合はdelegate{} でイベント登録
*/
public class ObjLib2D_GetItemParticle1 : ObjLib
{
    public const int MAX_PARTICLE = 5;
    private int _flame;
    private Transform _oldParent;
    private Transform _newParent;
    private Vector3 _staerPos;
    private Vector3 _endPos;
    private float _count = 0;
    private GameObject[] _particleObjs = new GameObject[5];

    public class CustomObject : MonoBehaviour
    {
        public static void Init(Transform oldParent, Transform newParent, GameObject prefab, Vector3 staerPos, Vector3 endPos, int flame, GameObject[] particleObjs)
        {
            // パーティクル生成
            GameObject particleObj;
            for (int i = 0; i < MAX_PARTICLE; i++)
            {
                particleObj = Instantiate(prefab);
                particleObj.transform.parent = oldParent;
                particleObj.transform.localScale = new Vector3(1, 1, 1);
                particleObj.transform.localPosition = staerPos;
                particleObjs[i] = particleObj;
            }

            // 初めのポジションまで移動
            new ObjLib2D_SetPositionCurve(particleObjs[0], new Vector3(particleObjs[0].transform.localPosition.x + (43.19931f), particleObjs[0].transform.localPosition.y + (131.0f), staerPos.z), flame / 2, false, delegate
            {
                // 親変更
                particleObjs[0].transform.parent = newParent;
                // 位置変更
                new ObjLib2D_SetPositionCurve(particleObjs[0], endPos, flame / 2, false, delegate
                      {
                      // 破棄
                      Destroy(particleObjs[0]);
                      });
            });
            new ObjLib2D_SetPositionCurve(particleObjs[1], new Vector3(particleObjs[1].transform.localPosition.x + (-99.80075f), particleObjs[1].transform.localPosition.y + (52.0f), staerPos.z), flame / 2, false, delegate
                    {
                        // 親変更
                        particleObjs[1].transform.parent = newParent;
                        // 位置変更
                        new ObjLib2D_SetPositionCurve(particleObjs[1], endPos, flame / 2, false, delegate
                              {
                        // 破棄
                        Destroy(particleObjs[1]);
                              });
                    });
            new ObjLib2D_SetPositionCurve(particleObjs[2], new Vector3(particleObjs[2].transform.localPosition.x + (-140.8008f), particleObjs[2].transform.localPosition.y + (-108.0f), staerPos.z), flame / 2, false, delegate
                    {
                        // 親変更
                        particleObjs[2].transform.parent = newParent;
                        // 位置変更
                        new ObjLib2D_SetPositionCurve(particleObjs[2], endPos, flame / 2, false, delegate
                              {
                        // 破棄
                        Destroy(particleObjs[2]);
                              });
                    });
            new ObjLib2D_SetPositionCurve(particleObjs[3], new Vector3(particleObjs[3].transform.localPosition.x + (59.19931f), particleObjs[3].transform.localPosition.y + (-86.0f), staerPos.z), flame / 2, false, delegate
                    {
                        // 親変更
                        particleObjs[3].transform.parent = newParent;
                        // 位置変更
                        new ObjLib2D_SetPositionCurve(particleObjs[3], endPos, flame / 2, false, delegate
                              {
                        // 破棄
                        Destroy(particleObjs[3]);
                              });
                    });
            new ObjLib2D_SetPositionCurve(particleObjs[4], new Vector3(particleObjs[4].transform.localPosition.x + (-19.80075f), particleObjs[4].transform.localPosition.y + (-124.0f), staerPos.z), flame / 2, false, delegate
                    {
                        // 親変更
                        particleObjs[4].transform.parent = newParent;
                        // 位置変更
                        new ObjLib2D_SetPositionCurve(particleObjs[4], endPos, flame / 2, false, delegate
                              {
                        // 破棄
                        Destroy(particleObjs[4]);
                              });
                    });
        }
    }

    public ObjLib2D_GetItemParticle1(GameObject obj, Transform oldParent, Transform newParent, GameObject prefab, Vector3 staerPos, Vector3 endPos, int flame, Action releaseEvent)
    {
        base.Init(obj, releaseEvent);
        _flame = flame;
        _count = flame;
        _staerPos = staerPos;
        _endPos = endPos;
        _oldParent = oldParent;
        _newParent = newParent;

        CustomObject.Init(oldParent, newParent, prefab, staerPos, endPos, flame, _particleObjs);
    }

    public override void Update()
    {
        _count++;
        if (_count == _flame)
        {
            // 終了処理
            SetRelease();
        }
    }
}
