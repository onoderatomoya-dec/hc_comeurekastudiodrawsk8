using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;


public class StageUiController : SingletonMonoBehaviour<StageUiController>
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager._id.Subscribe(x =>
        {
            // 数値変更
            TextMeshProUGUI text = this.transform.Find("StageUi/Text").GetComponent<TextMeshProUGUI>();
            text.text = "STAGE " + x.ToString();
        });

        this.transform.Find("StageUi").gameObject.SetActive(true);
        GameManager._isStart.Subscribe(x =>
        {
            if (x)
            {
                this.transform.Find("StageUi").gameObject.SetActive(false);
            }
            else
            {
                this.transform.Find("StageUi").gameObject.SetActive(true);

                // コイン量表示更新
                // CoinUi._coinUi.SetCoinText();
            }
        });
    }
}
