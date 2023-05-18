using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Globalization;
public class ObjLib 
{
    protected GameObject _gameObject;
    protected event Action _releaseEvent = null;
    private bool _isRelease = false;
    private bool _isEvent = false;
    protected bool _isDelayAction = false;

    private bool _skipEvent = false;
    public ObjLib()
    {
    }

    public void Init(GameObject obj,Action releaseEvent)
    {
        _gameObject = obj;
        ReleaseEvent += releaseEvent;
        ObjLibManager.Instance.SetObjLib(this);
    }

    public virtual void Update()
    {
    }

    protected void SetRelease()
    {
        if (_isRelease)
        {
            return;
        }
        _isRelease = true;
        UpdateEvent();
    }
    protected void ResetEvent()
    {
        ReleaseEvent -= null;
    }

    protected void UpdateEvent()
    {
        if (_releaseEvent != null && !_skipEvent)
        {
            _releaseEvent();
        }
    }

    public bool IsRelease()
    {
        if ((_gameObject == null && _isDelayAction == false) || _isRelease == true)
        {
            return true;
        }
        return false;
    }

    public void Destry()
    {
        // 終了処理
        _skipEvent = true;
        SetRelease();
    }

    private event Action ReleaseEvent
    {
        add
        {
            if (_releaseEvent == null || !_releaseEvent.GetInvocationList().Contains(value))
            {
                _releaseEvent += value;
            }
        }
        remove
        {
            if (_releaseEvent.GetInvocationList().Contains(value))
            {
                _releaseEvent -= value;
            }
        }
    }
}
