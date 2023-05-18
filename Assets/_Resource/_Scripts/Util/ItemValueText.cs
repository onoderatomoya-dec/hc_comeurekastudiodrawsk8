using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemValueText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _nowVale = 0;
    private int _newVale = 0;
    
    public void Init(int value)
    {
        _nowVale = value;
        _newVale = value;
        _text.text = CustomUtil.UnitChangeText(_newVale);
    }

    public void SetValue(int value)
    {
        _newVale = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_nowVale == _newVale)
        {
            return;
        }
        
        _nowVale = (int)Mathf.SmoothStep((float)_nowVale, (float)_newVale, 0.25f);
        if (Math.Abs(_newVale - _nowVale) <= 10.0f)
        {
            _nowVale = _newVale;
        }
        _text.text = CustomUtil.UnitChangeText(_nowVale);
    }
}