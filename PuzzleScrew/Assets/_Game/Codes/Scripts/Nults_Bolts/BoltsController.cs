using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace NultBolts
{
    public class BoltsController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] Transform trans_Bolts;
        [SerializeField] MeshFilter meshFilter;
        [SerializeField] Renderer meshRenderer;

        [Space]
        [Header("Configs")]
        [SerializeField] Vector3 localPos_UnPin = new Vector3(0, 0.1f, 0);
        [SerializeField] Vector3 localRot_UnPin = new Vector3(40, 0, 360);

        [SerializeField] float speedMove = 1;
        [SerializeField] float speedRotate = 1000;

        [SerializeField] Ease ease;


        public void PlayAnimUnPin()
        {
            DOTween.Kill(trans_Bolts);
            AudioManager.Instance.PlaySound(Audio.Unpin);
            trans_Bolts.DOLocalRotate(localRot_UnPin, speedRotate, RotateMode.FastBeyond360).SetSpeedBased().SetEase(ease);
            trans_Bolts.DOLocalMove(localPos_UnPin, speedMove).SetSpeedBased().SetEase(ease);
            // trans_Bolts.DOScale(Vector3.one * 1.2f, speedAction);
        }

        public void PlayAnimPin()
        {
            DOTween.Kill(trans_Bolts);
            AudioManager.Instance.PlaySound(Audio.Pin);
            trans_Bolts.DOLocalRotate(new Vector3(0, 0, -360), speedRotate, RotateMode.FastBeyond360).SetSpeedBased().SetEase(ease);
            trans_Bolts.DOLocalMove(Vector3.zero, speedMove).SetSpeedBased().SetEase(ease);
            // trans_Bolts.DOScale(Vector3.one * 1, .3f);
        }
    }
}
