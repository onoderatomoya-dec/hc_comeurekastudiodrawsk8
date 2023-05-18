using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public static class CustomUtil
{
    // お金の単位変換
    public static string UnitChangeText(int value)
    {
        string s;
        float newValue = 0;
        s = "$ " + value.ToString();
            
        if (value / 1000 >= 1)
        {
            newValue = value / 1000;
            float remainder =  value % 1000;
            remainder = remainder * 0.001f;
            newValue += remainder;
            s = "$ " + string.Format("{0:0.00}", newValue) + "k";
        }

        return s;
    }
    
    // Listを全て初期化
    public static void ReleaseList<Type>(List<Type> objectList)
    {
        int i = objectList.Count;
        for (int j = 0; j < i; j++)
        {
            objectList.RemoveAt(0);
        }
        objectList.Clear();
    }

    // 空の文字列チェック
    public static bool IsEmptyString(string str)
    {
        return str == string.Empty;
    }

    // 振動
    /*public static void TapticVibration(TapticPlugin.ImpactFeedback type)
    {
        if (SystemInfo.supportsVibration)
        {
            TapticPlugin.TapticManager.Impact(type);
        }
    }*/
    
    // 文字列をint型配列で取得 
    public static int[] GetInts(string str)
    {
        string array = str;
        array = array.Replace("[", "");
        array = array.Replace("]", "");
        array = array.Replace("n", "");
        array = array.Replace("\\", "");
        Debug.Log("array:" + array);
        string[] arraySplit = array.Split(',');
        int[] Ints = new int[arraySplit.Length];
        for (int i = 0; i < arraySplit.Length; i++)
        {
            int number;
            if (Int32.TryParse(arraySplit[i], out number))
            {
                Ints[i] = Int32.Parse(arraySplit[i]);
            }
        }
        return Ints;
    }

    // オブジェクトの角度を-180 ~ 180の範囲で取得
    public static Vector3 GetRotation180(Vector3 rot)
    {
        // X補正
        float x = rot.x;
        x += 180;
        x %= 360;
        if (x < 0)
        {
            x += 180;
        }
        else
        {
            x -= 180;
        }

        // y補正
        float y = rot.y;
        y += 180;
        y %= 360;
        if (y < 0)
        {
            y += 180;
        }
        else
        {
            y -= 180;
        }

        // y補正
        float z = rot.z;
        z += 180;
        z %= 360;
        if (z < 0)
        {
            z += 180;
        }
        else
        {
            z -= 180;
        }
        
        return new Vector3(x, y, z);
    }

    // 引数は-180から180を渡す。nowRotに対して-または+のどちら向きが最短値が求める。
    // flameは分割量を求める際に使うので、分割する必要がない場合は1を設定する
    public static Vector3 GetShortRotation(Vector3 nowRot, Vector3 newRot, int flame)
    {
        // 初期化
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;
        float newRotAdd = 0.0f;
        float newRotSub = 0.0f;
        float nowRotAdd = 0.0f;
        float nowRotSub = 0.0f;

        // x補正
        nowRotAdd = nowRot.x;
        nowRotSub = nowRot.x;
        if (newRot.x > 0.0f)
        {
            newRotAdd = newRot.x;
            newRotSub = newRot.x - 360.0f;
        }
        else
        {
            newRotAdd = newRot.x + 360.0f;
            newRotSub = newRot.x;
        }
        while (true)
        {
            nowRotAdd += 0.1f;
            nowRotSub -= 0.1f;

            // +向きの方が近い
            if (nowRotAdd >= newRotAdd)
            {
                x = (newRotAdd - nowRot.x) / (float)flame;
                break;
            }
            // -向きの方が近い
            if (nowRotSub <= newRotSub)
            {
                x = (newRotSub - nowRot.x) / (float)flame;
                break;
            }
        }

        // y補正
        nowRotAdd = nowRot.y;
        nowRotSub = nowRot.y;
        if (newRot.y > 0.0f)
        {
            newRotAdd = newRot.y;
            newRotSub = newRot.y - 360.0f;
        }
        else
        {
            newRotAdd = newRot.y + 360.0f;
            newRotSub = newRot.y;
        }
        while (true)
        {
            nowRotAdd += 0.1f;
            nowRotSub -= 0.1f;

            // +向きの方が近い
            if (nowRotAdd >= newRotAdd)
            {
                y = (newRotAdd - nowRot.y) / (float)flame;
                break;
            }
            // -向きの方が近い
            if (nowRotSub <= newRotSub)
            {
                y = (newRotSub - nowRot.y) / (float)flame;
                break;
            }
        }

        // z補正
        nowRotAdd = nowRot.z;
        nowRotSub = nowRot.z;
        if (newRot.z > 0.0f)
        {
            newRotAdd = newRot.z;
            newRotSub = newRot.z - 360.0f;
        }
        else
        {
            newRotAdd = newRot.z + 360.0f;
            newRotSub = newRot.z;
        }
        while (true)
        {
            nowRotAdd += 0.1f;
            nowRotSub -= 0.1f;

            // +向きの方が近い
            if (nowRotAdd >= newRotAdd)
            {
                z = (newRotAdd - nowRot.z) / (float)flame;
                break;
            }
            // -向きの方が近い
            if (nowRotSub <= newRotSub)
            {
                z = (newRotSub - nowRot.z) / (float)flame;
                break;
            }
        }

        return new Vector3(x, y, z);
    }

    // ベクトルの向きにY軸で回転(xとzのベクトルが分かれば良いのでyは0でも大ょうぶ)
    public static void SetVectorRotationY(GameObject obj,Vector3 vec)
    {
        // ベクトルから角度を求めて回転処理
        Quaternion q = Quaternion.LookRotation(vec);
        obj.transform.rotation = Quaternion.Euler(obj.transform.localEulerAngles.x, q.eulerAngles.y, obj.transform.localEulerAngles.z);
    }

    public static Dictionary<Tkey, TValue> ToDictionary<Tkey, TValue>(this IEnumerable<KeyValuePair<Tkey, TValue>> source)
    => source.ToDictionary(
        keySelector: kv => kv.Key,
        elementSelector: kv => kv.Value);
}
