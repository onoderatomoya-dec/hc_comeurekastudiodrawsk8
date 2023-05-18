using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLibManager : SingletonMonoBehaviour<ObjLibManager>
{
    List<ObjLib> _objLibList = new List<ObjLib>();
    List<ObjLib> _storageList  = new List<ObjLib>();
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

        // Listに追加
        foreach (ObjLib obj in _storageList)
        {
            _objLibList.Add(obj);
        }

        // Storege削除
        if (_storageList.Count > 0)
        {
            _storageList.Clear();
        }

        // Update処理
        foreach (ObjLib obj in _objLibList)
        {
            if (obj.IsRelease())
            {
                continue;
            }
            obj.Update();
        }

        // Release処理
        _objLibList.RemoveAll(s => s.IsRelease());
    }

    public void SetObjLib(ObjLib obj)
    {
        _storageList.Add(obj);
    }
}
