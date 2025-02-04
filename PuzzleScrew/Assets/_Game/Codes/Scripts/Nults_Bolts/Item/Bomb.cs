using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NultBolts
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] float distanExplodeBoom = 1f;

        Plate plateOwner;
        private void Awake()
        {
            plateOwner = transform.parent.GetComponent<Plate>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Plate plate;
            if (collision.TryGetComponent<Plate>(out plate))
            {
                if (plate == plateOwner)
                {
                    return;
                }
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanExplodeBoom);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].TryGetComponent<Plate>(out plate))
                    {
                        if (plate == plateOwner)
                        {
                            continue;
                        }
                        plate.GetBomb();
                        break;
                    }
                    // PlateScrewHole plateScrewHole;
                    // if (colliders[i].TryGetComponent<PlateScrewHole>(out plateScrewHole))
                    // {
                    //     // if (plate == plateOwner)
                    //     // {
                    //     //     continue;
                    //     // }
                    //     // plate.GetBomb();
                    //     // break;
                    //     if (!plateScrewHole.CanPin)
                    //     {
                    //         Debug.Log("HasScrew: " + plateScrewHole.gameObject.GetInstanceID());
                    //     }
                    // }
                }
                Explode();

                //plate.GetBomb();
                //Explode();
            }
        }

        public void Explode()
        {
            //effect here
            // ParticleSystem effect = BombStorage.Instance.GetBomb().GetComponent<ParticleSystem>();
            // effect.transform.position = transform.position;
            // effect.Play();
            AudioManager.Instance.PlaySound(Audio.Bomb);
            // BombStorage.Instance.ReturnBomb(effect.gameObject, 1);
            gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, distanExplodeBoom);

        }
#endif
    }
}
