using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaster
{
    static Dictionary<int, StageMaster> _master = new Dictionary<int, StageMaster>();

    public StageMaster(int id, string sceneName,int stageId,JsonData jsonData)
    {
        Debug.Log("StageMaster:" + id);
        _id = id;
        _sceneName = sceneName;
        _stageId = stageId;

        _master.Add(_id, this);
    }
    private int _id;
    private string _sceneName;
    private int _stageId;


    public int GetId()
    {
        return _id;
    }

    public string GetSceneName()
    {
        return _sceneName;
    }

    public int GetStageId()
    {
        return _stageId;
    }


    public static int GetSize()
    {
        return _master.Count;
    }
   
    public static StageMaster GetStageMaster(int id)
    {
        return _master[id];
    }
}
