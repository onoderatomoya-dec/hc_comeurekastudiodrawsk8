using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public static class JenkinsBuild
{
    [MenuItem("Build/ApplicationBuild/Android")]
    public static void BuildAndroid()
    {
        //AndroidにSwitch Platform
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        var scene_name_array = CreateBuildTargetScenes().ToArray();
        PlayerSettings.applicationIdentifier = "com.onodera.sample";
        PlayerSettings.productName = "hc_sumple";
        PlayerSettings.companyName = "DefaultCompany";
        
        //Splash Screenをオフにする(Personalだと動かないよ）
        PlayerSettings.SplashScreen.show = false;
        PlayerSettings.SplashScreen.showUnityLogo = false;
        
        //AppBundleを使用しない
        EditorUserBuildSettings.buildAppBundle = true;
        BuildPipeline.BuildPlayer(scene_name_array,"app.apk" , BuildTarget.Android, BuildOptions.None);
        
        //AppBundleを使用しない
        //EditorUserBuildSettings.buildAppBundle = false;
        //BuildPipeline.BuildPlayer(scene_name_array,"app.apk" , BuildTarget.Android, BuildOptions.Development);
    }

    #region Util

    private static IEnumerable<string> CreateBuildTargetScenes()
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
                yield return scene.path;
        }
    }

    #endregion
}
