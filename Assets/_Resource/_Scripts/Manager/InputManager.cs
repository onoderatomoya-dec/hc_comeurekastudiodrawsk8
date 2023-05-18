using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] public DynamicJoystick _dynamicJoystick;
    public Vector2 _dynamicJoystickVector2;
    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        // Awakeメソッドは必ずここから初期化処理を行う
        if (_dynamicJoystick != null)
        {
            _dynamicJoystickVector2 = new Vector2(_dynamicJoystick.Horizontal,_dynamicJoystick.Vertical);
        }
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
        
        if (_dynamicJoystick != null)
        {
            _dynamicJoystickVector2 = new Vector2(_dynamicJoystick.Horizontal,_dynamicJoystick.Vertical);
        }
    }
}