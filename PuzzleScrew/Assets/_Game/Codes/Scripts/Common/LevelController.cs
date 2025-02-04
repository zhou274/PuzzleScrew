using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{
    public class LevelController : MonoBehaviour
    {
        public virtual void LoadLevel(int level) { }

        public virtual void Reset()
        {
            // int level = MiniGameMaster.Instance.levelData.GetLevel();
            // LoadLevel(level);
        }

        public virtual void NextLevel() { }

        public virtual void CloseLevel() { }

        public virtual void IncreaseLevel() { }
    }
}
