using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAnalyticsSDK.Setup;
using UnityEngine;

namespace Main
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private General _geenral;

        [SerializeField] private GameObject _skateObject;

        [SerializeField] private GameObject _smokeEffect;

        // Start is called before the first frame update
        void Start()
        {
            PlayController.Instance._player = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public General GetGeneral()
        {
            return _geenral;
        }

        public void SetSmokeGameObject(bool flag)
        {
            _smokeEffect.SetActive(flag);
        }
        
        // オブジェクトが衝突したとき
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("Block_"))
            {
                Debug.Log("Player:OnCollisionEnter");
                PlayController.Instance._skate.transform.DOKill();
            }
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("Block_"))
            {
                Debug.Log("Player:OnTriggerEnter");
                PlayController.Instance.OnDeathEvent1();
                PlayController.Instance._skate.transform.DOKill();
            }
        }

        public void OnDeathEvent1()
        {
            // プレイヤーを消す
            
            // ラグドール生成 & 初期化
            
            // ラグドールで手前に回転させて落下
        }
    }
}