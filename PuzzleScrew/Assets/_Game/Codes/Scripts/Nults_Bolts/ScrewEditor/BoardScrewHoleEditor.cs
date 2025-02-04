using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{

#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(BoardScrewHole))]
    public class BoardScrewHoleEditor : Editor
    {
        void OnEnable()
        {
            SceneView.duringSceneGui -= this.OnSceneGUI;
            SceneView.duringSceneGui += this.OnSceneGUI;
        }

        void OnDisable()
        {
            SceneView.duringSceneGui -= this.OnSceneGUI;
        }

        void OnSceneGUI(SceneView sceneView)
        {
            CheckKey();
        }
        void CheckKey()
        {
            Event e = Event.current;
            if (e.type == EventType.MouseUp)
            {
                FixScrewBoardPosition();
            }
        }

        void FixScrewBoardPosition()
        {
            BoardScrewHole boardScrewHole = target as BoardScrewHole;
            ScrewBoard screwBoard = FindObjectOfType<ScrewBoard>();

            Plate[] plates = screwBoard.GetComponentsInChildren<Plate>();
            foreach (var plate in plates)
            {
                foreach (var screwHole in plate.listScrewHole)
                {
                    if (Vector2.Distance(screwHole.transform.position, boardScrewHole.transform.position) < .2f)
                    {
                        boardScrewHole.transform.position = screwHole.transform.position;
                        return;
                    }
                }
            }
        }
    }
#endif
}
