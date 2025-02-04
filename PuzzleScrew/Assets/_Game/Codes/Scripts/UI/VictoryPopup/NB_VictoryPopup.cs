using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using StarkSDKSpace;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;

namespace NultBolts
{
    public class NB_VictoryPopup : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] TextMeshProUGUI txt_ValueReward;
        [SerializeField] Button btn_Next;
        private StarkAdManager starkAdManager;

        public string clickid;
        public void OnEnable()
        {
            ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.LogError("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
        }
        private void Start()
        {
            btn_Next.onClick.AddListener(Next);
        }

        void Next()
        {
            NultBoltsManager.Instance.ReloadLevel();
            gameObject.SetActive(false);

        }
        /// <summary>
        /// 播放插屏广告
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="errorCallBack"></param>
        /// <param name="closeCallBack"></param>
        public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
                mInterstitialAd.Load();
                mInterstitialAd.Show();
            }
        }
    }
}
