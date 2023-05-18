using UnityEngine;

namespace PT
{
    public static class AttSupportedChecker
    {
        /// 許諾ステータス
        /// Appleのドキュメントから、独自のenumを作成
        /// https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanagerauthorizationstatus?language=objc
        enum Status
        {
            Unsupported = -1,  // ATTサポート外（Editor、Android用）
            NotDetermined = 0, // 承認要求前
            Restricted = 1,    // アプリの追跡データを使用する権限が制限されている（ペアレントサポートなど）
            Denied = 2,        // 承認要求後拒否 or 端末の設定で全体のトラッキング拒否
            Authorized = 3,    // 承認要求後承認
        }

        /// ATTが承認されてるかどうか
        public static bool IsAttApproved()
        {
            var status = (int)Status.Unsupported;
#if !UNITY_EDITOR && UNITY_IOS
            status = IOSAttSupportedCheckerPlugin.GetATTStatus();
#endif
            return status == (int)Status.Authorized;
        }
    }
}