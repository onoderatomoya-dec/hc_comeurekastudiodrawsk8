using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUi : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.material.renderQueue = 3001;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
