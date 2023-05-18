using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Collections.Generic;

public class BuildPostProcessor : MonoBehaviour
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            BuildiOS(path: path);
        }
        else if (buildTarget == BuildTarget.Android)
        {
            BuildAndroid(path: path);
        }
    }

    private static void BuildAndroid(string path = "")
    {
        Debug.Log("TenjinSDK: Starting Android Build");
    }

    private static void BuildiOS(string path = "")
    {
#if UNITY_IOS
        string buildUnityMainTarget;
        string buildUnityTestTarget;
        string buildUnityFrameworkTarget;
        
        
        
        Debug.Log("TenjinSDK: Starting iOS Build");
        string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
        string plistPath = path + "/Info.plist";
        
        PBXProject project = new PBXProject();
        PlistDocument plist = new PlistDocument();
        
        project.ReadFromFile(projectPath);
        plist.ReadFromString(File.ReadAllText(plistPath));
        PlistElementDict rootDict = plist.root;

#if UNITY_2019_3_OR_NEWER
        buildUnityMainTarget = project.GetUnityMainTargetGuid();
        buildUnityTestTarget = project.TargetGuidByName(PBXProject.GetUnityTestTargetName());
        buildUnityFrameworkTarget = project.GetUnityFrameworkTargetGuid();
        
#else
        buildUnityFrameworkTarget = project.TargetGuidByName("Unity-iPhone");
#endif

        List<string> frameworks = new List<string>();
        frameworks.Add("AdServices.framework");
        frameworks.Add("AdSupport.framework");
        frameworks.Add("AppTrackingTransparency.framework");
        frameworks.Add("iAd.framework");
        frameworks.Add("StoreKit.framework");

        foreach (string framework in frameworks)
        {
            Debug.Log("TenjinSDK: Adding framework: " + framework);
            project.AddFrameworkToProject(buildUnityFrameworkTarget, framework, true);
        }

        Debug.Log("TenjinSDK: Adding -ObjC flag to other linker flags (OTHER_LDFLAGS)");
        
        // plist対応
        rootDict.SetString("NSPhotoLibraryUsageDescription", "Save the image");
        rootDict.SetString("NSLocationWhenInUseUsageDescription", "Use location information");
        rootDict.SetString("NSCalendarsUsageDescription", "Needs Calendar Permission");
        rootDict.SetString("VoodooAdsPublisherId", "");
        
        // Project Unity-iPhone 対象

        // Target Unity-iPhone 対象
        project.AddBuildProperty(buildUnityMainTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
        
        // Target Unity-iPhone Tests 対象
        project.AddBuildProperty(buildUnityTestTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
        
        // Target UnityFramework 対象
        project.AddBuildProperty(buildUnityFrameworkTarget, "OTHER_LDFLAGS", "-ObjC");
        project.AddBuildProperty(buildUnityFrameworkTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");

        File.WriteAllText(projectPath, project.WriteToString());
        File.WriteAllText(plistPath, plist.WriteToString());
#endif  
    }
}
