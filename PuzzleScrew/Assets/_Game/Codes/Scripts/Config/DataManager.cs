using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace NultBolts
{
    public static class DataManager
    {
        #region Levels
        public static LevelConfigs levelConfigs
        {
            get
            {
                if (_levelConfigs == null)
                {
                    _levelConfigs = JsonUtility.FromJson<LevelConfigs>(PlayerPrefs.GetString(Config.KeyRemoteConfig.configLevel, new LevelConfigs().ToJson()));
                }
                return _levelConfigs;
            }
            set
            {
                _levelConfigs = value;
                PlayerPrefs.SetString(Config.KeyRemoteConfig.configLevel, value.ToJson());
            }
        }
        static LevelConfigs _levelConfigs;

        const string keyIndexLevel_NB = "keyIndexLevel_NB";
        public static int indexLevel_NB
        {
            get => PlayerPrefs.GetInt(keyIndexLevel_NB, 0);
            set => PlayerPrefs.SetInt(keyIndexLevel_NB, value);
        }
        const string keyLevelUnlock_NB = "keyLevelUnlock_NB";
        public static int levelUnlock_NB
        {
            get => PlayerPrefs.GetInt(keyLevelUnlock_NB, 0);
            set => PlayerPrefs.SetInt(keyLevelUnlock_NB, value);
        }

        #endregion

        #region list Levels


        #endregion

        #region GameConfigs
        static GameConfigRemote _gameConfigRemote;
        public static GameConfigRemote gameConfigRemote
        {
            get
            {
                if (_gameConfigRemote == null)
                {
                    _gameConfigRemote = JsonUtility.FromJson<GameConfigRemote>(PlayerPrefs.GetString(Config.KeyRemoteConfig.gameConfigs, new GameConfigRemote().ToJson()));
                }
                PlayerPrefs.SetString(Config.KeyRemoteConfig.gameConfigs, _gameConfigRemote.ToJson());
                return _gameConfigRemote;
            }
            set
            {
                _gameConfigRemote = value;
                PlayerPrefs.SetString(Config.KeyRemoteConfig.gameConfigs, value.ToJson());
            }
        }
        #endregion

        #region DataGame
        public static List<DataRewardPoint> dataRewardProgressRescue = new List<DataRewardPoint>() {
            new DataRewardPoint(RES_type.GOLD, 50),
            new DataRewardPoint(RES_type.GOLD, 100),
            new DataRewardPoint(RES_type.GOLD, 200),
            new DataRewardPoint(RES_type.GOLD, 400),
            new DataRewardPoint(RES_type.GOLD, 1000),
            };
        public static List<DataRewardPoint> dataRewardProgressNutsAndBolts = new List<DataRewardPoint>() {
            new DataRewardPoint(RES_type.GOLD, 100),
            new DataRewardPoint(RES_type.GOLD, 150),
            new DataRewardPoint(RES_type.GOLD, 200),
            new DataRewardPoint(RES_type.GOLD, 250),
            new DataRewardPoint(RES_type.BACKGROUND, 0),
            };
        #endregion


        public static string ToJson<T>(this T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }


}
