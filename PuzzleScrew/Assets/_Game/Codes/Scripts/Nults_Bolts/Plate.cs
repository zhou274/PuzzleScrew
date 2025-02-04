using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
namespace NultBolts
{
    [SelectionBase]
    public class Plate : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        private Collider2D collider;
        public Rigidbody2D Rigidbody2D { get => rigidbody2D; }
        public List<PlateScrewHole> listScrewHole;

#if UNITY_EDITOR
        public Vector3 OriginRot = Vector3.zero;
#endif
        private void Awake()
        {
            collider = GetComponent<Collider2D>();

            rigidbody2D.gravityScale = .75f;
            rigidbody2D.angularDrag = .125f;

        }
        public void FixHoleSize()
        {
            foreach (PlateScrewHole plateScrew in listScrewHole)
            {
                SetGlobalScale(Vector3.one * 1.2f, plateScrew.transform);
            }
        }
        public void GetBomb()
        {
            for (int i = 0; i < listScrewHole.Count; i++)
            {
                listScrewHole[i].GetBackScrew();
            }
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.WorldAxisAdd).SetLoops(10, LoopType.Restart);
            collider.isTrigger = true;
        }

        public void SetGlobalScale(Vector3 newScale, Transform myTransform)
        {
            Vector3 scaleRatio;
            scaleRatio.x = newScale.x / transform.localScale.x;
            scaleRatio.y = newScale.y / transform.localScale.y;
            scaleRatio.z = newScale.z / transform.localScale.z;

            myTransform.localScale = scaleRatio;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (ForceImpact(collision) > Config.Screw.forceImpactMinForAudio)
            {
                AudioManager.Instance.PlaySound(AudioManager.RandomHitPlateAudio());
            }
        }

        private float ForceImpact(Collision2D collision)
        {
            Vector3 relativeVelocity = collision.relativeVelocity;
            return relativeVelocity.magnitude;
        }

        [ContextMenu("Resize Scale Holes")]
        public void ResizeScale()
        {
            for (int i = 0; i < listScrewHole.Count; i++)
            {
                SetGlobalScale(Vector3.one * 1.2f, listScrewHole[i].transform);
            }
        }
    }
}
