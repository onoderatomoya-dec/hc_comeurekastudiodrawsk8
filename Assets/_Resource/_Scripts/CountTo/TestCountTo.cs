using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCountTo : MonoBehaviour
{
    [SerializeField] int _start;   // 開始値
    [SerializeField] int _goal;    // 最終目的値
    [SerializeField] int _time;
    private CountTo _countTo;      // カウントアップ(またはダウン)処理を行うクラス

    // Use this for initialization
    void Start()
    {
        _countTo = this.gameObject.AddComponent<CountTo>();
        _countTo.CountToInt(_start, _goal, _time);
    }

    // Update is called once per frame
    void Update()
    {
        // CountToクラスが動作中ならば、Canvas/Textを更新する
        if (_countTo.IsWorking())
        {
            float value = _countTo.Value;
            // int value = (int)Mathf.Ceil(_countTo.Value);
        }
    }
}
