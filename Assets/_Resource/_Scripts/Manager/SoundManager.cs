using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{

    // サウンド用プレハブ
    public AudioClip[] bgmClip;                // 複製するプレハブ(UNITY上でPrefabに対象プレハブを指定する必要がある)
    public AudioClip[] seClip;                // 複製するプレハブ(UNITY上でPrefabに対象プレハブを指定する必要がある)

    // サウンド用ゲームオブジェクト
    private List<AudioSource> _soudBgmList = new List<AudioSource>();
    private List<AudioSource> _soudSeList = new List<AudioSource>();

    // サウンド用オーディオクリップオブジェクト
    private List<AudioClip> _audioClipBgmList = new List<AudioClip>();
    private List<AudioClip> _audioClipSeList = new List<AudioClip>();

    // プレファブ
    public GameObject _audioPrefab;

    // メンバ
    private GameObject _parentBgm;
    private GameObject _parentSe;

    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        // ここから初期化処理を行う
        _parentBgm = this.transform.Find("BGM").gameObject;
        _parentSe = this.transform.Find("SE").gameObject;

        for (int i = 0; i < bgmClip.Length; i++)
        {
            _soudBgmList.Add(Instantiate(_audioPrefab, _parentBgm.transform).GetComponent<AudioSource>());
            _audioClipBgmList.Add(bgmClip[i]);
        }
        for (int i = 0; i < seClip.Length; i++)
        {
            _soudSeList.Add(Instantiate(_audioPrefab, _parentSe.transform).GetComponent<AudioSource>());
            _audioClipSeList.Add(seClip[i]);
        }


        // 音量設定
        // PlayingDataManager.SetSound(1);
        // PlayingDataManager.SetVibration(1);
        if (!PlayingDataManager.IsSound())
        {
            // ボリューム設定を反映
            SetBGMVolume(0);
            SetSEVolume(0);
        }
        
        // Bgm再生
        PlayBGM(0,true);
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
    }

    ////////////////////////////////////////////////////////////
    // BGM再生処理
    ////////////////////////////////////////////////////////////
    public void PlayBGM(int num, bool loopFlag)
    {
        // サウンドソース
        AudioSource audioSource = _soudBgmList[num].GetComponent<AudioSource>();

        // サウンドをコンポーネントにセットして再生する
        audioSource.clip = _audioClipBgmList[num];
        audioSource.loop = loopFlag;
        audioSource.Play();
    }
    
    ////////////////////////////////////////////////////////////
    // BGMのピッチを変更
    ////////////////////////////////////////////////////////////
    public void SetBGMPitch(float speed)
    {
        // コンポーネントにセットされたサウンドの音量を操作する
        foreach (AudioSource audioSource in _soudBgmList)
        {
            audioSource.pitch = speed;
        }
    }

    ////////////////////////////////////////////////////////////
    // BGM停止処理
    ////////////////////////////////////////////////////////////
    public void StopBGM(int num)
    {
        // サウンドソース
        AudioSource audioSource = _soudBgmList[num];

        // コンポーネントにセットされたサウンドを停止する
        audioSource.Stop();
    }

    ////////////////////////////////////////////////////////////
    // 全てのBGM停止処理
    ////////////////////////////////////////////////////////////
    public void StopAllBGM()
    {
        // コンポーネントにセットされたサウンドを停止する
        foreach (AudioSource audioSource in _soudBgmList)
        {
            audioSource.Stop();
        }
    }

    ////////////////////////////////////////////////////////////
    // SE再生処理
    ////////////////////////////////////////////////////////////
    public bool PlaySE(int num,bool isPlaying = false)
    {
        // サウンドソース
        AudioSource audioSource = _soudSeList[num];

        if (isPlaying && audioSource.isPlaying)
        {
            return false;
        }

        // サウンドをコンポーネントにセットして再生する(0,1を利用)
        audioSource.clip = _audioClipSeList[num];
        audioSource.loop = false;
        audioSource.Play();
        return true;
    }
    
    ////////////////////////////////////////////////////////////
    // SEのピッチ変更
    ////////////////////////////////////////////////////////////
    public void SetSEPitch(int num,float speed)
    {
        // サウンドソース
        AudioSource audioSource = _soudSeList[num];

        // サウンドをコンポーネントにセットして再生する(0,1を利用)
        audioSource.clip = _audioClipSeList[num];
        audioSource.pitch = speed;
    }

    ////////////////////////////////////////////////////////////
    // SE停止処理
    ////////////////////////////////////////////////////////////
    public void StopSE()
    {
        // コンポーネントにセットされたサウンドを停止する
        foreach (AudioSource audioSource in _soudSeList)
        {
            audioSource.Stop();
        }
    }

    ////////////////////////////////////////////////////////////
    // BGM音量処理
    ////////////////////////////////////////////////////////////
    public void SetBGMVolume(float data)
    {
        // コンポーネントにセットされたサウンドの音量を操作する
        foreach (AudioSource audioSource in _soudBgmList)
        {
            audioSource.volume = data;
        }
    }

    ////////////////////////////////////////////////////////////
    // SE音量処理
    ////////////////////////////////////////////////////////////
    public void SetSEVolume(float data)
    {
        // コンポーネントにセットされたサウンドの音量を操作する
        foreach (AudioSource audioSource in _soudSeList)
        {
            audioSource.volume = data;
        }
    }
}
