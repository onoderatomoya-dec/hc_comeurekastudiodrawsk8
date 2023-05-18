using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    public void ShowLink()
    {
        Application.OpenURL("https://app.babangida.be/privacy/en.php");
    }

    public void ShowUnityLink()
    {
        Application.OpenURL("https://dataoptout-ui-prd.uca.cloud.unity3d.com/?token=e1sn7a5noc73hbkodev3s8e37ghf9uc869huqc8o2otr5tln");
    }
}
