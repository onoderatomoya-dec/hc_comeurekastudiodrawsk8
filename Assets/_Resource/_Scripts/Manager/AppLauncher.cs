using PT;
using System.Threading.Tasks;
using ATT;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AppLauncher : MonoBehaviour
{
    void Awake()
    {
        // 初期化処理
        GameManager._isLaunchar = true;
        Initialize();
    }
    
    // 初期化処理
    async Task Initialize()
    {
        // Maxの初期化とATT表示
        bool isOptIn = true;
        IAtt iAtt = GetAtt();
        if (AttChecker.AttRequired())
        {
            // await、asyncで入力が完了するまで停止
            await iAtt.ShowAtt();
        }
#if UNITY_IOS
        // ATTのOptin、Optout確認
        if (AttChecker.AttRequired())
        {
            // ATTの許諾状態を取得
            isOptIn = AttSupportedChecker.IsAttApproved();
        }
#endif
        
        
        
        // GDPR/CCPAが対象の場合確認(1度確認している場合は2度目は入らない || ATT確認済みの場合も入らない)
        // canvasのPrefabなど生成してawait、asyncで入力が完了するまで停止
        
        
        
        
        // Toolsを取得
        IPrivacyTool[] _IPrivacyTools = GetPrivacyTools();
        
        // Optin、Optout設定
        foreach (var value in _IPrivacyTools)
        {
            await value.Init(isOptIn);
        }
        OnInitialized();
    }
    
    // 利用するAttのインスタンスを返す
    protected IAtt GetAtt()
    {
        return new MaxAtt();
    }

    // 利用するPrivacyToolsの配列を返す
    protected  IPrivacyTool[] GetPrivacyTools()
    {
        return new IPrivacyTool[]
        {
            new FacebookPrivacyTool(),
            new TenjinPrivacyTool(),
            new UnityAnalyticsPrivacyTool(),
            new GameAnalyticsPrivacyTrackingTool(),
            new MaxPrivacyTrackingTool()
        };
    }

    // 初期化が完了したときに呼ばれる
    protected void OnInitialized()
    {
        // ここで次のシーンに遷移したり、広告をロードしたりする
        SceneManager.LoadScene("LoadRemote");
    }
}