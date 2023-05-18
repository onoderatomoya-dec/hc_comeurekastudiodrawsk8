using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace PT
{
    public class TenjinPrivacyTool : IPrivacyTool
    {
        private const string ApiKey = "YQYYVGSJEQLBRUY5WUSHGYZ37BCVRRXY";

        public async Task Init(bool isOptIn)
        {
            if (isOptIn)
            {
                OptIn();
            }
            else
            {
                OptOut();
            }
        }

        public void OptIn()
        {
            Debug.Log("#TTL : TenjinOptIn");
            BaseTenjin instance = Tenjin.getInstance(ApiKey);
            instance.OptIn();
            instance.Connect();
        }

        public void OptOut()
        {
            Debug.Log("#TTL : TenjinOptOut");
            BaseTenjin instance = Tenjin.getInstance(ApiKey);
            instance.OptOut();
            instance.Connect();
        }
    }
}