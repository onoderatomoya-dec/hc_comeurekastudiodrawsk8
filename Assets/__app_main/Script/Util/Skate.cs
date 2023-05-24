using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAnalyticsSDK.Setup;
using UnityEngine;

namespace Main
{
    public class Skate : MonoBehaviour
    {
        [SerializeField] private GameObject _smokeEffect;
        
        private Rigidbody _rib;
        private BoxCollider _bc;

        private bool _isFall = false;
        // Start is called before the first frame update
        void Start()
        {
            PlayController.Instance._skate = this;
            _rib = this.GetComponent<Rigidbody>();
            _bc = this.GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {

            if (this.transform.localRotation.eulerAngles.z > 150 && !_isFall)
            {
                Debug.Log("Skate:OnDeathEvent2");
                _isFall = true;
                PlayController.Instance.OnDeathEvent2();
            }
        }
        
        // オブジェクトが衝突したとき
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("Block_"))
            {
                Debug.Log("Skate:OnCollisionEnter");
                this.transform.DOKill();
            }
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("Block_"))
            {
                Debug.Log("Skate:OnTriggerEnter");
                PlayController.Instance.OnDeathEvent1();
                this.transform.DOKill();
            }
        }
        
        public void OnDeathEvent1()
        {
            // ボードの物理を入れる
            //PlayController.Instance._skate.transform.DOKill();
            _bc.isTrigger = false;
            _rib.isKinematic = false;
            _smokeEffect.SetActive(false);

            // ボードを手前に回転させて落下
            _rib.AddForce(new Vector3(-10,0,-15.0f),ForceMode.VelocityChange);
            _rib.AddTorque(new Vector3(25,25,-25),ForceMode.VelocityChange);
        }
    }
}