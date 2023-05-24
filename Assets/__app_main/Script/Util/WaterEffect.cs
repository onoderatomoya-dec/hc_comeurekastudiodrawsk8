using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;

    [SerializeField] private bool _isMaguma = false;
    // Start is called before the first frame update
    private bool _isPlayerEffect = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_isPlayerEffect)
        {
            Debug.Log("WaterEffect:OnTriggerEnter:Player");
            _isPlayerEffect = true;
            
            // エフェクト
            GameObject effectObject = Instantiate(_effectPrefab);
            ParticleSystem particleSystem = effectObject.GetComponent<ParticleSystem>();
            particleSystem.Play();
            effectObject.transform.position = other.gameObject.transform.position;
            if (_isMaguma)
            {
                effectObject.transform.SetPositionY(effectObject.transform.position.y + 1.0f);
            }
            //Destroy(effectObject,3);
        }
        else if (other.gameObject.name == "Skate")
        {
            Debug.Log("WaterEffect:OnTriggerEnter:Skate");
            
            // エフェクト
            GameObject effectObject = Instantiate(_effectPrefab);
            ParticleSystem particleSystem = effectObject.GetComponent<ParticleSystem>();
            particleSystem.Play();
            effectObject.transform.position = other.gameObject.transform.position;
            if (_isMaguma)
            {
                effectObject.transform.SetPositionY(effectObject.transform.position.y + 1.0f);
            }
            //Destroy(effectObject,3);
        }
    }
}
