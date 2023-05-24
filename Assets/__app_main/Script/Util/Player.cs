using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        private Rigidbody _rib;
        private CapsuleCollider _cc;

        // Start is called before the first frame update
        void Start()
        {
            PlayController.Instance._player = this;
            _rib = this.transform.GetComponent<Rigidbody>();
            _cc = this.transform.GetComponent<CapsuleCollider>();
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
            _geenral.AllHideSkin();
            Destroy(_rib);
            Destroy(_cc);
            
            // ラグドール生成 & 初期化
            GameObject ragdollObject = Instantiate(PlayController.Instance._playerRagdollPrefab);
            ragdollObject.transform.position = this.transform.position;
            ragdollObject.transform.rotation = this.transform.rotation;
            List<Rigidbody> ribList = ragdollObject.transform.GetComponentsInChildren<Rigidbody>().ToList();

            // ラグドールで手前に回転させて落下
            foreach (Rigidbody rib in ribList)
            {
                this.CallAfter(0.0f, delegate
                {
                    rib.AddForce(new Vector3(0,-9,-3.5f),ForceMode.VelocityChange);
                    rib.AddTorque(new Vector3(15,15,-18),ForceMode.VelocityChange);
                });
            }
        }
        
        public void OnDeathEvent2()
        {
            // プレイヤーを消す
            _geenral.AllHideSkin();
            Destroy(_rib);
            Destroy(_cc);
            
            // ラグドール生成 & 初期化
            GameObject ragdollObject = Instantiate(PlayController.Instance._playerRagdollPrefab);
            ragdollObject.transform.position = this.transform.position;
            ragdollObject.transform.rotation = this.transform.rotation;
            List<Rigidbody> ribList = ragdollObject.transform.GetComponentsInChildren<Rigidbody>().ToList();

            // ラグドールで手前に回転させて落下
            foreach (Rigidbody rib in ribList)
            {
                rib.AddForce(new Vector3(0,-3,-3.5f),ForceMode.VelocityChange);
                rib.AddTorque(new Vector3(45,45,-48),ForceMode.VelocityChange);
            }
        }
    }
}