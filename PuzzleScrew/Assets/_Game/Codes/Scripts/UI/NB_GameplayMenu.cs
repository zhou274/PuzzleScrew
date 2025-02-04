using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;

namespace NultBolts
{
    public class NB_GameplayMenu : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Button btnSetting;

        [SerializeField] private Button btnNext;
        [SerializeField] private Button btnReset;

        [SerializeField] private TextMeshProUGUI levelText;

        public string clickid;
        private StarkAdManager starkAdManager;
        public void Start()
        {
            btnNext.onClick.AddListener(OnNext);
            btnReset.onClick.AddListener(OnReset);
            btnSetting.onClick.AddListener(OnSetting);
        }

        private void OnEnable()
        {
            ShowLevelTitle();
            NultBoltsManager.Instance.actionLoadLevel += ShowLevelTitle;
        }
        private void OnDisable()
        {
            NultBoltsManager.Instance.actionLoadLevel -= ShowLevelTitle;
        }

        void ShowLevelTitle()
        {
            levelText.text = $"关卡 {DataManager.indexLevel_NB + 1}";
        }

        private void OnNext()
        {
            ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    NultBoltsManager.Instance.ChangeGameState(TypeManager.GameState.Win);


                    clickid = "";
                    getClickid();
                    apiSend("game_addiction", clickid);
                    apiSend("lt_roi", clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
            
        }
        private void OnReset()
        {
            NultBoltsManager.Instance.ReloadLevel();
        }
        private void OnSetting()
        {

        }
        private void OnClickShopTheme()
        {

        }
        private void OnClickRemoveAds()
        {

        }
        private void OnListLevel()
        {

        }
        public void getClickid()
        {
            var launchOpt = StarkSDK.API.GetLaunchOptionsSync();
            if (launchOpt.Query != null)
            {
                foreach (KeyValuePair<string, string> kv in launchOpt.Query)
                    if (kv.Value != null)
                    {
                        Debug.Log(kv.Key + "<-参数-> " + kv.Value);
                        if (kv.Key.ToString() == "clickid")
                        {
                            clickid = kv.Value.ToString();
                        }
                    }
                    else
                    {
                        Debug.Log(kv.Key + "<-参数-> " + "null ");
                    }
            }
        }

        public void apiSend(string eventname, string clickid)
        {
            TTRequest.InnerOptions options = new TTRequest.InnerOptions();
            options.Header["content-type"] = "application/json";
            options.Method = "POST";

            JsonData data1 = new JsonData();

            data1["event_type"] = eventname;
            data1["context"] = new JsonData();
            data1["context"]["ad"] = new JsonData();
            data1["context"]["ad"]["callback"] = clickid;

            Debug.Log("<-data1-> " + data1.ToJson());

            options.Data = data1.ToJson();

            TT.Request("https://analytics.oceanengine.com/api/v2/conversion", options,
               response => { Debug.Log(response); },
               response => { Debug.Log(response); });
        }


        /// <summary>
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="closeCallBack"></param>
        /// <param name="errorCallBack"></param>
        public void ShowVideoAd(string adId, System.Action<bool> closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                starkAdManager.ShowVideoAdWithId(adId, closeCallBack, errorCallBack);
            }
        }
    }
}
