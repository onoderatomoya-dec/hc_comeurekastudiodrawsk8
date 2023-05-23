using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Main
{
    public class Skate : MonoBehaviour
    {
        private Rigidbody _rib;
        // Start is called before the first frame update
        void Start()
        {
            PlayController.Instance._skate = this;
            _rib = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

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
            
            // ボードを手前に回転させて落下s
            
        }
    }
}