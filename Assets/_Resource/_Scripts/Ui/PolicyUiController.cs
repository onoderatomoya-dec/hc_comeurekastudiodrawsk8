using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using OTCode;

public class PolicyUiController : SingletonMonoBehaviour<PolicyUiController>
{
    [SerializeField] private GameObject _settingUiObject;
    [SerializeField] private Button[] _soundButtons;
    [SerializeField] private Button[] _vibrationButtons;
    [SerializeField] private Button _informationButton;
    // Start is called before the first frame update
    void Start()
    {
        // セッティングの表示
        _settingUiObject.SetActive(false);
        this.transform.Find("SettingButton").GetComponent<ButtonEvent>().SetEvent(delegate
        {
            // Ui開く
            GameManager.SetShowUi(true);
            _settingUiObject.SetActive(true);
        });

        // サウンドボタン切り替え
        _soundButtons[0].onClick.AddListener(OffSound);
        _soundButtons[1].onClick.AddListener(OnSound);
        
        // バイブレーション切り替え
        _vibrationButtons[0].onClick.AddListener(OffVibration);
        _vibrationButtons[1].onClick.AddListener(OnVibration);
        
        // インフォメーション表示
        _informationButton.onClick.AddListener(ShowInformation);
        
        // サウンドのボタン初期化
        if (PlayingDataManager.IsSound())
        {
            OnSound();
        }
        else
        {
            OffSound();
        }
        
        // バイブレーションのボタン初期化
        if (PlayingDataManager.IsVibration())
        {
            OnVibration();
        }
        else
        {
            OffVibration();
        }

        // Start次のコンフィグボタン切り替え
        GameManager._isStart.Subscribe(x =>
        {
            if (x)
            {
                this.transform.Find("SettingButton").gameObject.SetActive(true);
            }
            else
            {
                this.transform.Find("SettingButton").gameObject.SetActive(true);
            }
        });
    }

    public void OnClose()
    {
        // Ui閉じる
        GameManager.SetShowUi(false);
        _settingUiObject.SetActive(false);
        
    }
    
    // サウンドOff
    private void OffSound()
    {
        _soundButtons[0].gameObject.SetActive(false);
        _soundButtons[1].gameObject.SetActive(true);
        PlayingDataManager.SetSound(1);
        if (SoundManager.instance == null)
        {
            return;
        }
        SoundManager.instance.SetBGMVolume(0);
        SoundManager.instance.SetSEVolume(0);
    }
    // サウンドOn
    private void OnSound()
    {
        _soundButtons[0].gameObject.SetActive(true);
        _soundButtons[1].gameObject.SetActive(false);
        PlayingDataManager.SetSound(0);
        if (SoundManager.instance == null)
        {
            return;
        }
        SoundManager.instance.SetBGMVolume(1);
        SoundManager.instance.SetSEVolume(1);
    }
    
    // 振動Off
    private void OffVibration()
    {
        _vibrationButtons[0].gameObject.SetActive(false);
        _vibrationButtons[1].gameObject.SetActive(true);
        PlayingDataManager.SetVibration(1);
    }
    // 振動On
    private void OnVibration()
    {
        _vibrationButtons[0].gameObject.SetActive(true);
        _vibrationButtons[1].gameObject.SetActive(false);
        PlayingDataManager.SetVibration(0);
    }
    
    // インフォメーション表示
    public void ShowInformation()
    {
        Application.OpenURL("https://eurekastudio.co.jp/policy/");
    }
}
