using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace OTCode
{
    public class EffectManager : SingletonMonoBehaviour<EffectManager>
    {
        public GameObject _textPrefab;
        public GameObject _playNearmissCreatePrefab;
        public GameObject _playNicePrefab;
        public GameObject _playOopsPrefab;
        void Awake()
        {
            if (!base.Awake())
            {
                return;
            }
            // Awakeメソッドは必ずここから初期化処理を行う
        }

        void OnEnable()
        {
            base.OnEnable();
        }

        void Start()
        {
            base.Start();
        }
        void Update()
        {
            base.Update();
        }

        /* 
        説明: テキストを画面に表示して消えていく
        str: 表示文字
        pos: ローカルポジションを指定
        tran: 親を指定
        */
        public void CreateText(string str, Vector3 pos, Transform tran)
        {
            // ミスアイコン表示
            GameObject textPrefab = Instantiate(_textPrefab);
            textPrefab.transform.SetParent(tran, false);
            textPrefab.transform.position = pos;
            textPrefab.transform.rotation = Quaternion.Euler(0, 0, 0);

            // 文字変更
            textPrefab.GetComponent<TextMeshProUGUI>().text = str;

            // ポイントの拡大縮小
            new ObjLib2D_SetScalingPopIcon(textPrefab, new Vector3(1.2f, 1.2f, 1), 12, 4, delegate
            {
                new ObjLib_DelayAction(10, delegate
                {
                // 縮小処理
                new ObjLib_SetScalingCurve(textPrefab, new Vector3(0, 0, 0), 30, null);
                });
            });
            // 移動処理
            Vector3 localPos = textPrefab.transform.localPosition;
            new ObjLib2D_SetPositionMove(textPrefab, new Vector3(localPos.x, localPos.y + 300, localPos.z), 60, null);

            // 破棄
            Destroy(textPrefab, 2);
        }

        /* 
        説明: Imageを画面に表示して消えていく
        prefab: Imageのプレファブを指定
        pos: ワールド座標を指定
        tran: 親を指定
        */
        public void CreateImage(GameObject prefab, Vector3 pos, Transform tran)
        {
            // アイコン表示
            GameObject prefabObj = Instantiate(prefab);
            prefabObj.transform.SetParent(tran, false);
            prefabObj.transform.position = pos;
            prefabObj.transform.rotation = Quaternion.Euler(0, 0, 0);

            // ポイントの拡大縮小
            new ObjLib2D_SetScalingPopIcon(prefabObj, new Vector3(1.25f, 1.25f, 1), 15, 6, delegate
            {
                new ObjLib_DelayAction(10, delegate
                {
                // 縮小処理
                new ObjLib_SetScalingCurve(prefabObj, new Vector3(0, 0, 0), 30, null);
                });
            });
            // 移動処理
            Vector3 localPos = prefabObj.transform.localPosition;
            new ObjLib2D_SetPositionMove(prefabObj, new Vector3(localPos.x, localPos.y + 300, localPos.z), 60, null);

            // 破棄
            Destroy(prefabObj, 2);
        }
    }
}