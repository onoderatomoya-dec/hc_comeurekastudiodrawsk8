using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayingDataManager
{
    // 変数
    public static StageMaster _stageMaster = null;
    public static bool _isDebug = true;
    public static float _addResultTime = 5.5f;
    
    // お金を背負う処理
    public static bool _isMoneyHold = false;

    // レベル名
    public static string _workingTimeLevelName = "_working_time_level";
    public static string _workerSpeedLevelName = "_worker_speed_level";
    
    public static string _playerCapacityLevelName = "_player_capacity_level";
    public static string _playerCollectSpeedLevelName = "_player_collect_speed_level";
    
    public static string _helperHireLevelName = "_helper_hire_level";
    public static string _helperCapacityLevelName = "_helper_capacity_level";
    public static string _helperMoveSpeedLevelName = "_helper_move_speed_level";
    
    public static string _gaugeValueName = "_gauge_value";
    
    
    // 初めバージョン記録
    public static bool IsFirstVersion()
    {

        bool isFirstversion = (PlayerPrefs.GetInt("is_first_version", 0)) == 0;
        if (isFirstversion)
        {
            PlayerPrefs.SetInt("is_first_version", 1);
            return true;
        }

        return false;
    }

    // 初めのバージョン受け取り
    public static string GetFirstVersion()
    {
        return PlayerPrefs.GetString("first_version", "");;
    }

    // 初めてそのステージをプレイしたかのチェック
    public static bool IsFirstStage(int stageId)
    {

        bool isFirstStage = (PlayerPrefs.GetInt("is_first_stage_" + stageId.ToString(), 0)) == 0;
        if (isFirstStage)
        {
            PlayerPrefs.SetInt("is_first_stage_" + stageId.ToString(), 1);
            return true;
        }

        return false;
    }


    // 初めてインステ見た
    public static bool IsFirstInste()
    {

        bool isFirstInste = (PlayerPrefs.GetInt("is_first_inste", 0)) == 0;
        if (isFirstInste)
        {
            PlayerPrefs.SetInt("is_first_inste", 1);
            return true;
        }

        return false;
    }
    // 初めてリワード見た
    public static bool IsFirstReward()
    {

        bool isFirstReward = (PlayerPrefs.GetInt("is_first_reward", 0)) == 0;
        if (isFirstReward)
        {
            PlayerPrefs.SetInt("is_first_reward", 1);
            return true;
        }

        return false;
    }


    // 初プレイか取得
    public static bool IsFirst()
    {
        bool isFirst = PlayerPrefs.GetInt("first_play", 0) == 0;
        PlayerPrefs.SetInt("first_play", 1);
        return isFirst;
    }

    // 初めてステージをクリアした
    public static bool IsFirstClear()
    {

        bool isClear = (PlayerPrefs.GetInt("first_clear", 0)) == 0;
        if (isClear)
        {
            PlayerPrefs.SetInt("first_clear", 1);
            return true;
        }
        
        return false;
    }

    // ステージID取得
    public static int GetStageId()
    {
        return PlayerPrefs.GetInt("stage_id", 1);
    }

    // ステージIDの保存
    public static void SetStageId(int stageId)
    {
        PlayerPrefs.SetInt("stage_id", stageId);
    }

    // デバッグ情報取得
    public static int GetDebugId()
    {
        return PlayerPrefs.GetInt("debug_count", 0);
    }

    // デバッグ情報保存
    public static void SetDebugId(int count)
    {
        PlayerPrefs.SetInt("debug_count", count);
    }

    // ウェーブ情報取得
    public static int GetWaveId()
    {
        return PlayerPrefs.GetInt("wave_id",0);
    }

    // ウェーブIDの保存
    public static void SetWaveId(int waveId)
    {
        PlayerPrefs.SetInt("wave_id", waveId);
    }

    // タイトル画像読み込み完了s
    public static void SetTitleImageName(int scenarioId,string name)
    {
        PlayerPrefs.SetInt("title_name_" + scenarioId.ToString() + "_" + name, 1);
    }

    public static bool IsTitleImageName(int scenarioId,string name)
    {
        return PlayerPrefs.GetInt("title_name_" + scenarioId.ToString() + "_" + name,0) == 1;
    }

    // サウンド設定
    public static void SetSound(int  flag)
    {
        PlayerPrefs.SetInt("sound", flag);
    }
    public static bool IsSound()
    {
        return PlayerPrefs.GetInt("sound", 0) == 0;
    }
    
    // バイブレーション
    public static void SetVibration(int flag)
    {
        PlayerPrefs.SetInt("vibration", flag);
    }
    public static bool IsVibration()
    {
        return PlayerPrefs.GetInt("vibration", 0) == 0;
    }
    

    // プロローグ設定
    public static void SetPrologue(int flag)
    {
        PlayerPrefs.SetInt("is_prologue", flag);
    }
    public static bool IsPrologue()
    {
        return PlayerPrefs.GetInt("is_prologue", 0) == 0;
    }

    // チュートリアル設定
    public static void Setreview(int  flag)
    {
        PlayerPrefs.SetInt("is_review", flag);
    }
    public static bool IsReview()
    {
        return PlayerPrefs.GetInt("is_review", 0) == 0;
    }

    // ステージの解放チェック
    public static void SetStageRelease(int id,int flag)
    {
        PlayerPrefs.SetInt("is_stage_release_" + id, flag);
    }
    public static bool IsStageRelease(int id)
    {
        // 解放されていればtrue
        return PlayerPrefs.GetInt("is_stage_release_" + id, 0) == 1;
    }

    // ステージクリアチェック
    public static void SetStageClear(int id, int flag)
    {
        PlayerPrefs.SetInt("is_stage_clear_" + id, flag);
    }
    public static bool IsStageClear(int id)
    {
        // クリアされていればtrue
        return PlayerPrefs.GetInt("is_stage_clear_" + id, 0) == 1;
    }

    // ヒント解放判定
    public static void SetHitFreeCount()
    {
        int freeCount = PlayerPrefs.GetInt("hint_free_Count", 0);
        freeCount++;
        PlayerPrefs.SetInt("hint_free_Count", freeCount);
    }
    public static int GetHitFreeCount()
    {
        return PlayerPrefs.GetInt("hint_free_Count", 0);
    }
    public static bool IsHitFree()
    {
        // まだ無料でヒントを見れればtrue
        int freeCount = PlayerPrefs.GetInt("hint_free_Count", 0);
        return freeCount < 3;
    }

    public static void SetOpenHint(int stageId,int sectionId, int flag)
    {
        PlayerPrefs.SetInt("is_open_hint_" + stageId + "_" + sectionId, flag);
    }
    public static bool IsOpenHin(int stageId, int sectionId)
    {
        // オープンされていればtrue
        return PlayerPrefs.GetInt("is_open_hint_" + stageId + "_" + sectionId, 0) == 1;
    }

    // コイン取得
    public static int GetCoin()
    {
        return PlayerPrefs.GetInt("coin" , 0);
    }

    // コインの保存
    public static void SetCoin(int value)
    {
        PlayerPrefs.SetInt("coin", value);
    }

    // レベル取得
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 1);
    }

    // レベルの保存
    public static void SetLevel(int value)
    {
        PlayerPrefs.SetInt("level", value);
    }

    // 初めてステージをクリアした
    public static bool IsFirstClearStage(int stageId)
    {

        bool isClear = (PlayerPrefs.GetInt("first_clear_" + stageId, 0)) == 0;
        if (isClear)
        {
            PlayerPrefs.SetInt("first_clear_" + stageId, 1);
            return true;
        }

        return false;
    }

    // ゲージ取得
    public static void SetGauge(float value)
    {
        PlayerPrefs.SetFloat("gauge", value);
    }

    // ゲージ設定
    public static float GetGauge()
    {
        return PlayerPrefs.GetFloat("gauge", 0.0f);
    }
    
    // 生成されたGimmick名設定
    public static void SetGenerateName(string name)
    {
        PlayerPrefs.SetInt("generate_name_" + name, 1);
    }
    
    // 生成されているか確認
    public static bool IsGenerateName(string name)
    {
        return PlayerPrefs.GetInt("generate_name_" + name, 0) == 1;
    }
    
    // レベル取得
    public static int GetLevel(string name)
    {
        return PlayerPrefs.GetInt(name, 0);
    }
    // レベル設定
    public static void SetLevel(string name,int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    
    // ゲージ値保存
    public static void SetGaugeValue(string name,int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    
    // ゲージ値取得
    public static int GetGaugeValue(string name)
    {
        return PlayerPrefs.GetInt(name, -1);
    }

    
    // お金取得
    public static int GetMoney()
    {
        return PlayerPrefs.GetInt("money" , 0);
    }

    // お金保存
    public static void SetMoney(int value)
    {
        PlayerPrefs.SetInt("money", value);
    }
    
    // Prisonチュートリアル取得
    public static int TUTORIAL_IDLE_INDEX_TASK_RELEACE = 0;
    public static int TUTORIAL_IDLE_INDEX_GENERATE_RELEACE = 1;
    public static int TUTORIAL_IDLE_INDEX_GET_ITEM = 2;
    public static int TUTORIAL_IDLE_INDEX_SET_ITEM = 3;
    public static int TUTORIAL_IDLE_INDEX_END = 4;
    
    public static int GetIdleTutorial()
    {
        return PlayerPrefs.GetInt("tutorial" , TUTORIAL_INDEX_PRISON_RELEACE);
    }

    // チュートリアル設定
    public static void SetIdleTutorial(int value)
    {
        PlayerPrefs.SetInt("tutorial" , value);
    }
    
    // チュートリアルかどうか
    public static bool IsIdleTutorial()
    {
        return PlayerPrefs.GetInt("tutorial" , TUTORIAL_IDLE_INDEX_TASK_RELEACE) <= TUTORIAL_IDLE_INDEX_SET_ITEM;
    }
    
    
    // Prisonチュートリアル取得
    public static int TUTORIAL_INDEX_PRISON_RELEACE = 0;
    public static int TUTORIAL_INDEX_GET_BATTON = 1;
    public static int TUTORIAL_INDEX_GET_CULPRIT = 2;
    public static int TUTORIAL_INDEX_SET_CULPRIT = 3;
    public static int TUTORIAL_INDEX_ITEM_GENERATE_RELEACE = 4;
    public static int TUTORIAL_INDEX_GET_ITEM = 5;
    public static int TUTORIAL_INDEX_SET_ITEM = 6;
    public static int TUTORIAL_INDEX_HELPER_RELEACE = 7;
    public static int TUTORIAL_INDEX_STAGE1_AREA2_RELEACE = 8;
    public static int TUTORIAL_INDEX_GET_PISTOL = 9;
    public static int TUTORIAL_INDEX_BUTTOLE_PISTOL = 10;
    public static int TUTORIAL_INDEX_SET_PISTOL_CULPRIT = 11;
    public static int TUTORIAL_INDEX_STAGE1_AREA3_RELEACE = 12;
    public static int TUTORIAL_INDEX_GET_LAUNCHER = 13;
    public static int TUTORIAL_INDEX_BUTTOLE_LAUNCHAR = 14;
    public static int TUTORIAL_INDEX_GET_LAUNCHAR_CULPRIT = 15;
    public static int TUTORIAL_INDEX_SET_LAUNCHAR_CULPRIT = 16;
    public static int TUTORIAL_INDEX_END = 17;
    public static int GetTutorial()
    {
        return PlayerPrefs.GetInt("tutorial" , TUTORIAL_INDEX_PRISON_RELEACE);
    }

    // チュートリアル設定
    public static void SetTutorial(int value)
    {
        PlayerPrefs.SetInt("tutorial" , value);
    }
    
    // チュートリアルかどうか
    public static bool IsTutorial()
    {
        return PlayerPrefs.GetInt("tutorial" , TUTORIAL_INDEX_PRISON_RELEACE) <= TUTORIAL_INDEX_HELPER_RELEACE;
    }
    
    // 解放エリアのイベント用パラメータ取得
    public static int EVENT_AREA_ID_NONE = 0;
    public static int EVENT_AREA_ID_START = 1;
    public static int EVENT_AREA_ID_TUTORIAL = 2;
    public static int EVENT_AREA_ID_STAGE1_CLEAR = 3;
    public static int EVENT_AREA_ID_STAGE2_CLEAR = 4;
    public static int EVENT_AREA_ID_STAGE3_CLEAR = 5;
    public static int GetEventAreaId()
    {
        return PlayerPrefs.GetInt("event_area" , 0);
    }
    // 解放エリアのイベント用パラメータ設定
    public static void SetEventAreaId(int value)
    {
        PlayerPrefs.SetInt("event_area" , value);
    }
    
    // 牢屋に逃げない囚人がセットされた場合にフラグを設定
    public static void SetNotEscape(string name)
    {
        PlayerPrefs.SetInt("not_escape_prison_" + name , 1);
    }
    
    // 牢屋に逃げない囚人がセットされているか取得
    public static bool IsNotEscape(string name)
    {
        return PlayerPrefs.GetInt("not_escape_prison_" + name , 0) == 1;
    }
    
    // 取得武器のId設定
    public static int WEAPON_ID_NONE = 0;
    public static int WEAPON_ID_BATTON = 1;
    public static int WEAPON_ID_PISTOL = 2;
    public static int WEAPON_ID_LAUNCHER = 3;
    public static int WEAPON_ID_MAX = 4;
    
    public static void SetWeaponId(int id)
    {
        PlayerPrefs.SetInt("weapon_id_" +  id, 1);
    }
    
    // 指定の武器を所持しているか確認
    public static bool IsWeaponId(int id)
    {
        return PlayerPrefs.GetInt("weapon_id_" + id, 0) == 1;
    }
    
    // 最後に所持している武器のID取得
    public static int GetLastWeaponId()
    {
        for (int i = WEAPON_ID_MAX -1; i > 0; i--)
        {
            if (PlayerPrefs.GetInt("weapon_id_" + i, 0) == 1)
            {
                return i;
            }
        }
        return 0;
    }

    // カスタムディメンションが一致するか確認
    public static bool IsCustomDimension(string name)
    {
        return PlayerPrefs.GetString("custom_dimension", "") == name;
    } 
    
    // カスタムディメンション登録
    public static void SetCustomDimension(string name)
    {
        PlayerPrefs.SetString("custom_dimension", name);
    }
}
