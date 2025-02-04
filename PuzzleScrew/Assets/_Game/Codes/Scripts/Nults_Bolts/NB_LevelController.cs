using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{
    public class NB_LevelController : LevelController
    {
        protected GameObject currentLevel;
        public DataLevel dataLevel;
        public override void LoadLevel(int level)
        {
            level = GetLevel(level);
            dataLevel = DataManager.levelConfigs.dataLevels[level - 1];

            if (currentLevel != null)
            {
                CloseLevel();
            }
            currentLevel = Instantiate(Resources.Load<GameObject>("ScrewNewLevelPrefab/Level " + dataLevel.keyPrefabLevel), this.transform);
            // screwBoard = currentLevel.GetComponentInChildren<ScrewBoard>();
            // screwBoard.SetTheme(level % 5 == 0);
            currentLevel.transform.localPosition = Vector3.zero;
            NultBoltsManager.Instance.gameState = TypeManager.GameState.Playing;
        }

        int GetLevel(int level)
        {

            return level;

        }
        public override void CloseLevel()
        {
            Destroy(currentLevel);
        }

        public override void NextLevel()
        {
            // int res = AdsHelper.Instance.showGift(1, log, state =>
            // {
            //     if (state == AD_State.AD_REWARD_OK)
            //     {
            base.NextLevel();
            //     }
            // });
        }
    }
}
