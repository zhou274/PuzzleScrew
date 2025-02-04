using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{
    public class NultBoltsManager : SingletonMonoBehaviour<NultBoltsManager>
    {
        [Header("Setup")]
        [SerializeField] NB_LevelController levelController;

        [Header("Popups")]
        [SerializeField] GameObject popupVictory;


        public TypeManager.GameState gameState;

        public Action actionLoadLevel;

        private void Start()
        {
            Application.targetFrameRate = 60;
            gameState = TypeManager.GameState.Ready;

            LoadLevel();
        }


        void LoadLevel()
        {
            levelController.LoadLevel(DataManager.indexLevel_NB + 1);
            actionLoadLevel?.Invoke();
        }

        public void ReloadLevel()
        {
            levelController.LoadLevel(DataManager.indexLevel_NB + 1);
            actionLoadLevel?.Invoke();
        }

        public void ChangeGameState(TypeManager.GameState _state)
        {
            if (_state != gameState)
            {
                SetGameState(_state);
            }
        }

        void SetGameState(TypeManager.GameState _state)
        {
            gameState = _state;
            switch (gameState)
            {
                case TypeManager.GameState.Ready:

                    break;
                case TypeManager.GameState.Playing:

                    break;
                case TypeManager.GameState.Win:
                    DataManager.indexLevel_NB += 1;
                    if (DataManager.levelUnlock_NB < DataManager.indexLevel_NB)
                    {
                        DataManager.levelUnlock_NB = DataManager.indexLevel_NB;
                    }
                    popupVictory.gameObject.SetActive(true);
                    break;
                case TypeManager.GameState.Lose:

                    break;
            }
        }
    }
}
