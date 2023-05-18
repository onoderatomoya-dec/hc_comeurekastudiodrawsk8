using UnityEngine;

public class CountTo : MonoBehaviour
{
    private int _mode = 0;             // -1:値を減少させる(動作中), 0:動作していない 1:値を増加させる(動作中)
    private float _value = 0.0f;       // 現在値
    private float _start = 0.0f;       // 開始値
    private float _goal = 0.0f;        // 最終目的値
    private decimal _perTime = 0.0m;   // 値が"1"(または-1)変化するのに必要な時間
    private decimal _time = 0.0m;      // 経過時間

    public float Value
    {
        get
        {
            return _value;
        }
    }

	// Use this for initialization
	void Awake ()
    {
        ResetVariable();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // カウントアップ
        if (_mode > 0)
        {
            if (_value == _goal)
            {
                // 終了
                _mode = 0;
                return;
            }
            _value = (float)((decimal)_start + _time / _perTime);   // ※１ 割る数(_perTime)が0だと例外発生するので注意
            if (_value >= _goal)
            {
                // 終了予告
                _value = _goal;
            }
        }
        // カウントダウン
        else if (_mode < 0)
        {
            if (_value == _goal)
            {
                // 終了
                _mode = 0;
                return;
            }
            _value = (float)((decimal)_start - _time / _perTime);   // ※１ 割る数(_perTime)が0だと例外発生するので注意
            if (_value <= _goal)
            {
                // 終了予告
                _value = _goal;
            }
        }
        // 動作していないとき
        else
        {
            return;
        }
           
        _time += (decimal)Time.deltaTime;  // 初回は"_value==_start"にしたいので、_timeは最後に更新する
	}

    // 各変数を初期化する
    private void ResetVariable()
    {
        _mode = 0;
        _value = 0.0f;
        _start = 0.0f;
        _goal = 0.0f;
        _perTime = 0.0m;
        _time = 0.0m;
    }

    // カウントアップ(またはダウン)を開始する
    public void CountToInt(int start, int goal, float time)
    {
        // 起動中ならば実行できないものとする
        if (IsWorking())
        {
            return;
        }
           
        // カウントアップ(またはダウン)の各種値を設定①
        if (start < goal)
        {
            _mode = 1;
        }
        else if (start > goal)
        {
            _mode = -1;
        }
        else
        {
            _mode = 0;
            return;
        }
        _perTime = (decimal)(goal - start) / (decimal)time;// 1.0秒毎の変化量
        _perTime = 1.0m / _perTime;                        // 値"1"(または-1)変化するのに必要な時間
        if (_perTime < 0)
        {
            _perTime *= (-1.0m);                           // _perTimeは時間なので、"_perTime>=0"
        }
        // _perTime==0.0mになると、※１で例外が発生するので実行せずに終了する。
        if (_perTime <= 0.0m)
        {
            ResetVariable();
        }

        // カウントアップ(またはダウン)の各種値を設定②
        _value = start;    // 現在値(開始値)を設定
        _start = start;    // 開始値を設定
        _goal = goal;      // 最終目的値を設定
        _time = 0.0m;      // 経過時間をリセット
    }

    // このクラスが動作中か否か
    public bool IsWorking()
    {
        return (_mode==0) ? false : true;
    }
}