using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NultBolts
{
    public class ScrewBoard : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer boardRender;

        public List<BoardScrewHole> boardScrewHoles;
        public List<Screw> screws;
        public List<Plate> plates;

        public List<BoardScrewHole> adScrewHoles;

        public Transform ScrewHoleHolder, ScrewHolder;
        public Transform plateLayer1, plateLayer2, plateLayer3, plateLayer4, plateLayer5, plateLayer6, plateLayer7, plateLayer8;
        float timeToCheckDrop;
        private int indexAdUnlock;
        private SpriteRenderer crBackground;
        // [SerializeField] private ScrewModeType modeType;
        // public static ScrewModeType ModeType;

        private void Start()
        {
            if (Config.Screw.EnableAdsUnlockHoleBtn)
            {
                for (int i = 0; i < adScrewHoles.Count; i++)
                {
                    adScrewHoles[i].gameObject.SetActive(false);
                }
            }
        }

        private void Update()
        {
            CheckPlatesDrop();

            // #if UNITY_EDITOR
            //             if (Input.GetKeyDown(KeyCode.L))
            //             {
            //                 MiniGameMaster.Instance.CurrentGame.OnLose();
            //             }
            // #endif
        }



        public void UnlockNewScrewHole()
        {
            if (indexAdUnlock < adScrewHoles.Count)
            {
                adScrewHoles[indexAdUnlock].gameObject.SetActive(true);
                indexAdUnlock++;
            }
        }

        public bool HasAdScrewSlot()
        {
            return indexAdUnlock < adScrewHoles.Count;
        }

        // private void Awake()
        // {
        //     transform.localScale = Vector3.one * .8f;
        //     transform.position += Vector3.up * .3f;
        // }

        private void CheckPlatesDrop()
        {

            timeToCheckDrop -= Time.deltaTime;
            if (timeToCheckDrop < 0)
            {
                timeToCheckDrop = .2f;
                for (int i = 0; i < plates.Count; i++)
                {
                    if (plates[i].transform.position.y < -6)
                    {
                        plates.RemoveAt(i);
                        i--;
                    }
                }

                if (plates.Count == 0)
                {

                    NultBoltsManager.Instance.ChangeGameState(TypeManager.GameState.Win);

                }
            }
        }


        [ContextMenu("Resize Hole In Plates")]
        public void ResizeHoleInPlates()
        {
            for (int i = 0; i < plates.Count; i++)
            {
                plates[i].ResizeScale();
            }
        }
    }

    public enum ScrewModeType
    {
        Normal, Rescue
    }
}
