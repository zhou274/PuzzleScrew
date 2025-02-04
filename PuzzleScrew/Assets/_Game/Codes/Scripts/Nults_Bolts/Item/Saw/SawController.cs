using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace NultBolts
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(SawController))]
    public class SawControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SawController sawController = target as SawController;
            SetUp(sawController);
            if (GUILayout.Button("SetLine"))
            {
                sawController.GetPoints();
                sawController.SetLine();
            }
            GUILayout.Space(3);
            EditorUtility.SetDirty(target);
        }


        private void SetUp(SawController sawController)
        {
            if (sawController.targetPlate != null)
            {
                if (sawController.targetPlate.GetComponent<SpriteRenderer>().sortingOrder != 3)
                {
                    sawController.targetPlate.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    foreach (var screwHole in sawController.targetPlate.listScrewHole)
                    {
                        screwHole.GetComponent<SpriteMask>().backSortingOrder = 2;
                    }
                }

                if (sawController.lineRenderer == null)
                {
                    sawController.lineRenderer = sawController.targetPlate.GetComponentInChildren<LineRenderer>();
                }
                if (sawController.fixedJoint2D == null)
                {

                    FixedJoint2D joint2D = sawController.targetPlate.GetComponent<FixedJoint2D>();
                    if (joint2D != null)
                    {
                        sawController.fixedJoint2D = joint2D;
                    }
                    else
                    {
                        sawController.fixedJoint2D = sawController.targetPlate.gameObject.AddComponent<FixedJoint2D>();
                    }
                }
            }
        }
    }
#endif


    public class SawController : MonoBehaviour
    {
        public Plate targetPlate;
        public FixedJoint2D fixedJoint2D;
        public LineRenderer lineRenderer;
        [SerializeField] private Transform saw;
        [SerializeField] private Animation sawAnim;
        [SerializeField] private Transform[] points;
        private float timeCheckUpdate;
        private bool canCheck = true;
        private bool canRotate = false;
        private void Update()
        {
            CoverCheck();
            SawRotate();
        }

        private void FixedUpdate()
        {
            SetLine();
        }

        public void GetPoints()
        {
            points = new Transform[2];
            points[0] = lineRenderer.transform.GetChild(1);
            points[1] = lineRenderer.transform.GetChild(0);
        }
        public void SetLine()
        {
            lineRenderer.positionCount = 2;
            for (int i = 0; i < 2; i++)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }

        public void StartSawing()
        {
            SpriteRenderer spriteRenderer = saw.GetChild(0).GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerID = targetPlate.GetComponent<SpriteRenderer>().sortingLayerID;
            spriteRenderer.sortingOrder = 4;

            saw.DOMove(points[0].position, .5f).OnComplete(() =>
            {
                AudioManager.Instance.PlaySound(Audio.Sawing, true);
                canRotate = true;
                sawAnim.Play("Sawing", PlayMode.StopAll);
                saw.DOMove(points[1].position, 1.5f).OnComplete(() =>
                {
                    StopSawing();
                });
            });
        }

        public void StopSawing()
        {
            AudioManager.Instance.StopSound();
            sawAnim.Stop();
            canRotate = false;
            fixedJoint2D.enabled = false;
            lineRenderer.gameObject.SetActive(false);
            saw.gameObject.SetActive(false);
        }

        private void CoverCheck()
        {
            if (canCheck)
            {
                timeCheckUpdate += Time.deltaTime;
                if (timeCheckUpdate > .5f)
                {
                    timeCheckUpdate = 0;
                    if (!IsCover())
                    {
                        StartSawing();
                        canCheck = false;
                    }
                }
            }
        }

        private void SawRotate()
        {
            if (canRotate)
            {
                float angle = GetAngle2Points(points[0].position, points[1].position);
                saw.eulerAngles = new Vector3(0, 0, angle + 180);
            }
        }
        public static float GetAngle2Points(Vector2 point1, Vector2 point2)
        {
            return Mathf.Atan2(point2.y - point1.y, point2.x - point1.x) * 180 / Mathf.PI;
        }
        private bool IsCover()
        {
            Collider2D[] cols = Physics2D.OverlapPointAll(transform.position);
            foreach (var col in cols)
            {
                if (col.TryGetComponent(out Plate plate))
                {
                    if (plate.gameObject.layer >= saw.gameObject.layer) return true;
                }
            }
            return false;
        }
    }
}
