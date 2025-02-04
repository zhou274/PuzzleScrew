using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace NultBolts
{
    public static class ButtonAnimation
    {
        public static void Anim_Scale_In_Out(MonoBehaviour behaviour)
        {
            if (!behaviour.isActiveAndEnabled)
            {
                return;
            }
            Animator anim;
            anim = behaviour.GetComponent<Animator>();
            if (anim)
            {
                anim.enabled = false;
            }
            behaviour.StartCoroutine(IEAnim_Scale_In_Out(behaviour));
        }
        public static IEnumerator IEAnim_Scale_In_Out(MonoBehaviour behaviour)
        {
            if (!behaviour.isActiveAndEnabled)
            {
                Anim_Normal(behaviour);
                yield break;
            }
            Transform tf = behaviour.transform;
            tf.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.8f);
            yield return new WaitForSeconds(0.5f);
            tf.DOScale(new Vector3(1f, 1f, 1f), 0.8f);
            yield return new WaitForSeconds(0.5f);
            behaviour.StartCoroutine(IEAnim_Scale_In_Out(behaviour));
        }
        public static void Anim_Normal(MonoBehaviour behaviour)
        {
            behaviour.transform.DOKill();
            behaviour.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f);
            Animator anim;
            anim = behaviour.GetComponent<Animator>();
            if (anim)
            {
                anim.enabled = true;
            }
            behaviour.StopAllCoroutines();
        }
    }
}
