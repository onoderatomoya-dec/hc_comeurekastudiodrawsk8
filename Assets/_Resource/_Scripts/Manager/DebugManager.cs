using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : SingletonMonoBehaviour<DebugManager>
{
    // デバッグのカウント
    private int _debugCount = 0;


    void Awake()
    {
        if (!base.Awake())
        {
            return;
        }
        // Awakeメソッドは必ずここから初期化処理を行う
    }

    void OnEnable()
    {
        base.OnEnable();
    }

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    public void DebugLeft()
    {
        _debugCount++;
        if (_debugCount < 1 || !PlayingDataManager._isDebug)
        {
            return;
        }

        int id = PlayingDataManager._stageMaster.GetId();
        id--;
        PlayingDataManager.SetStageId(id);
        SceneChangeManager.ChageScene();
        Debug.Log("DebugLeft");
    }
    public void DebugMidle()
    {
        _debugCount++;
        if (_debugCount < 1 || !PlayingDataManager._isDebug)
        {
            return;
        }

        SceneChangeManager.ChageScene();
    }

    public void DebugRight()
    {
        _debugCount++;
        if (_debugCount < 1 || !PlayingDataManager._isDebug)
        {
            return;
        }

        int id = PlayingDataManager._stageMaster.GetId();
        id++;
        PlayingDataManager.SetStageId(id);
        SceneChangeManager.ChageScene();
        Debug.Log("DebugRight");
    }
}
