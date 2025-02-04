using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{
    public class ItemScrewdrivers : Item
    {
        public override IEnumerator LoopCheck()
        {
            while (active)
            {
                yield return new WaitForEndOfFrame();
                if (Input.GetMouseButton(0))
                {
                    Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D col = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("InputPlayer"));

                    if (col != null && col.transform.parent.TryGetComponent(out Screw screw))
                    {
                        // var cols = Physics2D.OverlapPointAll(mousePosition , Config.Screw.HoleLayerMask);
                        // foreach (var col2 in cols)
                        // {
                        //     if (col2.TryGetComponent(out BoardScrewHole hole) && hole.isLocked)
                        //     {
                        //         yield break;
                        //     }
                        // }
                        Used();
                        screw.ClearScrew();
                        yield break;
                    }

                }
            }
        }

    }
}