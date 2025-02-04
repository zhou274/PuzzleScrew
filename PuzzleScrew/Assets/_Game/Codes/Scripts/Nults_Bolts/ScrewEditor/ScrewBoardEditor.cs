using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{
    using Unity.VisualScripting;

#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(ScrewBoard))]
    public class ScrewBoardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ScrewBoard screwBoard = target as ScrewBoard;

            GUILayout.Space(3);
            if (GUILayout.Button("GenHole"))
            {
                GenHole(screwBoard);
            }
            GUILayout.Space(3);
            if (GUILayout.Button("GenScrew"))
            {
                GenScrew(screwBoard);
            }
            GUILayout.Space(3);
            if (GUILayout.Button("fix rotate"))
            {
                FixRotationPlate(screwBoard);
            }
            GUILayout.Space(3);
            if (GUILayout.Button("Reset Rotate"))
            {
                ResetRotation(screwBoard);
            }
            GUILayout.Space(3);
            if (GUILayout.Button("Finish"))
            {
                Finish(screwBoard);
            }

            EditorUtility.SetDirty(screwBoard);
        }

        public void GenScrew(ScrewBoard screwBoard)
        {
            Screw screw = Resources.Load<Screw>("ScrewMapItem/Screw");
            var obj = PrefabUtility.InstantiatePrefab(screw, screwBoard.ScrewHolder);
            screw = obj.GetComponent<Screw>();
            screwBoard.screws.Add(screw);
            EditorUtility.SetDirty(screw);
        }

        public void Finish(ScrewBoard screwBoard)
        {
            screwBoard.screws.Clear();
            screwBoard.boardScrewHoles.Clear();
            screwBoard.plates.Clear();
            screwBoard.adScrewHoles.Clear();
            BoardScrewHole[] boardScrewHoles = screwBoard.ScrewHoleHolder.GetComponentsInChildren<BoardScrewHole>();
            Screw[] screws = screwBoard.ScrewHolder.GetComponentsInChildren<Screw>();

            List<Plate> plates = new List<Plate>();
            SetPlateLayer(plates);
            foreach (var item in boardScrewHoles)
            {
                screwBoard.boardScrewHoles.Add(item);
                if (item.adsLocked)
                {
                    screwBoard.adScrewHoles.Add(item);
                }
            }

            foreach (var plate in plates)
            {
                screwBoard.plates.Add(plate);
                plate.listScrewHole.Clear();
                PlateScrewHole[] plateScrewHoles = plate.GetComponentsInChildren<PlateScrewHole>();
                foreach (var item in plateScrewHoles)
                {
                    plate.listScrewHole.Add(item);
                }
            }

            foreach (var screw in screws)
            {
                screwBoard.screws.Add(screw);
                screw.UnPin(true);
                screw.EditorPin();
            }

            //Set Plate Layer
            void SetPlateLayer(List<Plate> plates)
            {
                Plate[] platesLayer1 = screwBoard.plateLayer1.GetComponentsInChildren<Plate>();
                Plate[] platesLayer2 = screwBoard.plateLayer2.GetComponentsInChildren<Plate>();
                Plate[] platesLayer3 = screwBoard.plateLayer3.GetComponentsInChildren<Plate>();
                Plate[] platesLayer4 = screwBoard.plateLayer4.GetComponentsInChildren<Plate>();
                Plate[] platesLayer5 = screwBoard.plateLayer5.GetComponentsInChildren<Plate>();
                Plate[] platesLayer6 = screwBoard.plateLayer6.GetComponentsInChildren<Plate>();
                Plate[] platesLayer7 = screwBoard.plateLayer7.GetComponentsInChildren<Plate>();
                Plate[] platesLayer8 = screwBoard.plateLayer8.GetComponentsInChildren<Plate>();
                foreach (var plateLayer in platesLayer1)
                {
                    FixPlate(plateLayer, plates, "Layer 1", Config.Screw.PlateLayer1);
                }
                foreach (var plateLayer in platesLayer2)
                {
                    FixPlate(plateLayer, plates, "Layer 2", Config.Screw.PlateLayer2);

                }
                foreach (var plateLayer in platesLayer3)
                {
                    FixPlate(plateLayer, plates, "Layer 3", Config.Screw.PlateLayer3);

                }
                foreach (var plateLayer in platesLayer4)
                {
                    FixPlate(plateLayer, plates, "Layer 4", Config.Screw.PlateLayer4);

                }
                foreach (var plateLayer in platesLayer5)
                {
                    FixPlate(plateLayer, plates, "Layer 5", Config.Screw.PlateLayer5);
                }
                foreach (var plateLayer in platesLayer6)
                {
                    FixPlate(plateLayer, plates, "Layer 6", Config.Screw.PlateLayer6);

                }
                foreach (var plateLayer in platesLayer7)
                {
                    FixPlate(plateLayer, plates, "Layer 7", Config.Screw.PlateLayer7);

                }
                foreach (var plateLayer in platesLayer8)
                {
                    FixPlate(plateLayer, plates, "Layer 8", Config.Screw.PlateLayer8);
                }

            }

        }

        public void FixPlate(Plate plateLayer, List<Plate> plates, string LayerName, int plateLayerID)
        {
            plates.Add(plateLayer);
            plateLayer.gameObject.layer = plateLayerID;
            plateLayer.GetComponent<SpriteRenderer>().sortingLayerName = LayerName;
            plateLayer.FixHoleSize();
            foreach (var hole in plateLayer.listScrewHole)
            {
                hole.GetComponent<SpriteMask>().backSortingLayerID = SortingLayer.NameToID(LayerName);
                hole.GetComponent<SpriteMask>().frontSortingLayerID = SortingLayer.NameToID(LayerName);
            }
        }
        public void GenHole(ScrewBoard screwBoard)
        {
            BoardScrewHole boardScrewHole = Resources.Load<BoardScrewHole>("ScrewMapItem/BoardScrewHole");
            var obj = PrefabUtility.InstantiatePrefab(boardScrewHole, screwBoard.ScrewHoleHolder);
            boardScrewHole = obj.GetComponent<BoardScrewHole>();
            screwBoard.boardScrewHoles.Add(boardScrewHole);
        }

        public void FixRotationPlate(ScrewBoard screwBoard)
        {
            ResetRotation(screwBoard);

            FixHandle(screwBoard.plateLayer1.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer2.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer3.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer4.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer5.GetComponentsInChildren<Plate>());
            void FixHandle(Plate[] plates)
            {
                foreach (var plate in plates)
                {
                    plate.transform.eulerAngles += new Vector3(0, 0, Random.Range(-1f, 1f));
                }
            }
        }
        public void ResetRotation(ScrewBoard screwBoard)
        {
            FixHandle(screwBoard.plateLayer1.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer2.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer3.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer4.GetComponentsInChildren<Plate>());
            FixHandle(screwBoard.plateLayer5.GetComponentsInChildren<Plate>());

            void FixHandle(Plate[] plates)
            {
                foreach (var plate in plates)
                {
                    plate.transform.eulerAngles = plate.OriginRot;
                }
            }
        }

    }
#endif
}
