using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    private static int _scene;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }

    virtual protected bool Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        return CheckInstance();
    }

    virtual protected void OnEnable()
    {
    }

    virtual protected void Start()
    {
    }

    virtual protected void Update()
    {
    }

    virtual protected void LateUpdate()
    {
    }

    public void SetTxt(GameObject txtObj, string txt)
    {
        TMPro.TextMeshProUGUI label;
        label = txtObj.GetComponent<TMPro.TextMeshProUGUI>();
        label.font.TryAddCharacters(txt);

        txtObj.GetComponent<TextMeshProUGUI>().text = TxtReplaceCode(txt);
    }
    // 改行コード修正
    public string TxtReplaceCode(string txt)
    {
        return txt.Replace("\\n", "\n");
    }
}