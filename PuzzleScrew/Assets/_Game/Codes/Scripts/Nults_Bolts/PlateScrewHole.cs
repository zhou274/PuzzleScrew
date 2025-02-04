using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace NultBolts
{
    [ExecuteAlways]
    public class PlateScrewHole : MonoBehaviour, IHole
    {
        [SerializeField] private Plate plate;
        public Plate Plate { get => plate; set => plate = value; }
        [SerializeField] private bool hasCrew;
        [SerializeField] private Screw currentScrew;
        public bool CanPin => !hasCrew;
        private void Start()
        {
            SpriteRenderer holeRender = GetComponent<SpriteRenderer>();
            SpriteRenderer plateRender = plate.GetComponent<SpriteRenderer>();
            holeRender.sortingLayerID = plateRender.sortingLayerID;
            holeRender.sortingOrder = plateRender.sortingOrder + 1;
        }

        public void Pin(Screw screw)
        {
            hasCrew = true;
            currentScrew = screw;
        }

        public void UnPin()
        {
            hasCrew = false;
            currentScrew = null;
        }
        public void GetBackScrew()
        {
            currentScrew?.PlayAnimUnPin();
            Screw screw = currentScrew;
            currentScrew?.UnPin();
            DOVirtual.DelayedCall(0.5f, () =>
            {
                screw?.gameObject.SetActive(false);
            });
        }

        [ContextMenu("Resize Scale")]
        public void ResizeScale()
        {
            if (plate)
            {
                plate.SetGlobalScale(Vector3.one * 1.2f, this.transform);
            }
        }
    }
}
