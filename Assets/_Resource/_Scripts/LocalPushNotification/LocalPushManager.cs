using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPushManager : MonoBehaviour
{
    //「アプリを開始した時」、「Homeボタンを押した時」、「Backボタンを押した時(ボタンはAndroidのみに存在)」、「OvewViewボタンを押した時(ボタンはAndroidのみに存在)」に実行
    private void OnApplicationFocus()
    {
        //プッシュ通知の設定
        SettingPush();
    }
    
    //プッシュ通知の設定
    private void SettingPush()
    {
        //　Androidチャンネルの登録
        // LocalPushNotification.RegisterChannel(引数1,引数２,引数３);
        // 引数１ Androidで使用するチャンネルID なんでもいい LocalPushNotification.AddSchedule()で使用する
        // 引数2　チャンネルの名前　なんでもいい　アプリ名でも入れておく
        // 引数3　通知の説明 なんでもいい　自分がわかる用に書いておくもの　
        LocalPushNotification.RegisterChannel("MakeaprisonId", "Makeaprison", "Makeaprison is Notification");

        // 通知のクリア
        LocalPushNotification.AllClear();

        // プッシュ通知の登録
        // LocalPushNotification.AddSchedule(引数１,引数2,引数3,引数4,引数5);
        // 引数１ プッシュ通知のタイトル
        // 引数2　通知メッセージ
        // 引数3　表示するバッジの数(バッジ数はiOSのみ適用の様子 Androidで数値を入れても問題無い)
        // 引数4　何秒後に表示させるか？
        // 引数5　Androidで使用するチャンネルID　「Androidチャンネルの登録」で登録したチャンネルIDと合わせておく
        // 注意　iOSは45秒経過後からしかプッシュ通知が表示されない
        LocalPushNotification.AddSchedule("Make a prison", "prisoners await your return ❤️", 1, 86400, "MakeaprisonId"); // 86400
        LocalPushNotification.AddSchedule("Make a prison", "prisoners await your return ❤️", 1, 259200, "MakeaprisonId"); // 259200
    }
}
