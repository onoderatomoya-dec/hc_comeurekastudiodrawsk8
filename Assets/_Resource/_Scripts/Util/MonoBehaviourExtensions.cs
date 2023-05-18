using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void CallAfter(this MonoBehaviour self, float delay, Action action)
    {
        self.StartCoroutine(DelayMethod(delay, () =>
        {
            action();
        }));
    }

    private static IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}