using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AD_BANNER_POS
{
    TOP,
    BOTTOM
}
public enum RES_type
{
    GOLD = 0,
    CRYSTAL = 1,
    HEART = 2,
    SUGGEST = 3,
    BACKGROUND = 4,
    PLANK = 5,
    NAIL = 6,
    SCREWDRIVE = 7,
    BOMB = 8,
}
[Serializable]
public class DataTypeResource
{
    public DataTypeResource(RES_type type, int id)
    {
        this.type = type;
        this.id = id;
    }
    public RES_type type;
    public int id;
}

[Serializable]
public class DataResource
{

    public DataTypeResource dataTypeResource;
    public int amount;
}
namespace NultBolts
{
    public class SceneConfig
    {
        public const string spl = "Spl";
        public const string Menu = "MenuScene";
        public const string NultBolts = "NultsBoltsScene";
    }

    public class TypeManager
    {
        public enum LevelType
        {
            NORMAL,
            SPECIAL,
            TIME_COUNT
        }

        public enum MiniGameType
        {
            NutNBolt,
            Rescue
        }
        public enum GameState
        {
            Ready,
            Playing,
            Win,
            Lose
        }
        public enum ECutSceneItem
        {
            None,
            Door,
            Heater,
            Window,
            Ceiling,
            Food4Human,
            Food4Cat,
            DrinkingWater,
            Bed,
            Blankets,
            Clothes,
            Wall,
            Floor

        }


        public enum EStatusElement
        {
            DAMAGED,
            NORMAL,
            VIP
        }
    }

    #region  Level Config
    [Serializable]
    public struct DataLevel
    {
        public string keyPrefabLevel;
        public TypeManager.LevelType levelType;
        public float time;

        public DataLevel(string _keyPrefab, TypeManager.LevelType _levelType, float _time)
        {
            keyPrefabLevel = _keyPrefab;
            levelType = _levelType;
            time = _time;
        }
    }

    [Serializable]
    public class LevelConfigs
    {
        public List<DataLevel> dataLevels;
        public LevelConfigs()
        {
            dataLevels = new List<DataLevel>();
            for (int i = 1; i <= 100; i++)
            {
                if (i % 5 == 0)
                {
                    dataLevels.Add(new DataLevel(i.ToString(), TypeManager.LevelType.SPECIAL, 0));
                }
                else
                {
                    dataLevels.Add(new DataLevel(i.ToString(), TypeManager.LevelType.NORMAL, 0));
                }
            }
        }

    }
    #endregion

    #region  Level rescue Config
    [Serializable]
    public struct DataLevelRescue
    {
        // public int indexItemData;
        public string typeItemLevelRescue;
        public string keyPrefabLevel;

        public DataLevelRescue(TypeManager.ECutSceneItem _sceneItem, string _keyPrefabLevel)
        {
            this.typeItemLevelRescue = _sceneItem.ToString();
            this.keyPrefabLevel = _keyPrefabLevel;
        }

        public TypeManager.ECutSceneItem GetTypeItemLevel()
        {
            return Enum.Parse<TypeManager.ECutSceneItem>(typeItemLevelRescue);
        }
    }

    [Serializable]
    public class LevelRescueConfigs
    {
        public List<DataLevelRescue> dataLevels;
        public TypeManager.ECutSceneItem GetItemLevelConfigByIndex(int index)
        {
            if (index >= dataLevels.Count)
            {
                Debug.LogError($"Miss cutScene index: {index} in chapter: 1");
                index = dataLevels.Count - 1;
            }
            return dataLevels[index].GetTypeItemLevel();
        }
        public string Get_KeyPrefabLevelConfig_ByIndex(int index)
        {
            if (index >= dataLevels.Count)
            {
                Debug.LogError($"Miss cutScene index: {index} in chapter: 1");
                index = dataLevels.Count - 1;
            }
            return dataLevels[index].keyPrefabLevel;
        }
        public LevelRescueConfigs()
        {
            dataLevels = new List<DataLevelRescue>();
            for (int i = 1; i <= 12; i++)
            {
                dataLevels.Add(new DataLevelRescue((TypeManager.ECutSceneItem)(i), i.ToString()));
            }
        }

    }

    [Serializable]
    public class DataSaveLevelRescue
    {
        public TypeManager.EStatusElement statusItem = TypeManager.EStatusElement.DAMAGED;
        public TypeManager.ECutSceneItem typeItem;
        public DataSaveLevelRescue(TypeManager.ECutSceneItem _typeItem, TypeManager.EStatusElement _statusItem)
        {
            statusItem = _statusItem;
            typeItem = _typeItem;
        }
    }

    [Serializable]
    public class DatasSaveRescue
    {
        public List<DataSaveLevelRescue> dataSaves;
        public DatasSaveRescue()
        {
            dataSaves = new List<DataSaveLevelRescue>();
        }
        public DatasSaveRescue(List<DataSaveLevelRescue> listData)
        {
            dataSaves = listData;
        }
        public TypeManager.EStatusElement GetStatusElementByTypeItem(TypeManager.ECutSceneItem _typeItem)
        {
            DataSaveLevelRescue _dataSave = dataSaves.Find(data => data.typeItem == _typeItem);
            if (_dataSave != null)
            {
                return _dataSave.statusItem;
            }
            else
            {
                return TypeManager.EStatusElement.DAMAGED;
            }
        }
    }
    #endregion

    #region GameConfig
    [Serializable]
    public class GameConfigRemote
    {
        public int level_StartShowCollapse = 5;
        public int configTutorial = 1;
        public int level_ShowModeRescue = 5;
        public bool activeModeRescue = true;
    }
    #endregion

    #region  Quest
    [Serializable]
    public class DataRewardPoint
    {
        public RES_type typeReward;
        public int value;

        public DataRewardPoint(RES_type _typeReward, int _value)
        {
            typeReward = _typeReward;
            value = _value;
        }

    }
    [Serializable]
    public class ConfigRewardPoint
    {
        public List<DataRewardPoint> dataRewardPoints;
    }
    #endregion
}
