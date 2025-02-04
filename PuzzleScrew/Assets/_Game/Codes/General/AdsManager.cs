using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace NultBolts
{
    public class AdsManager : MonoBehaviour
    {
        private static AD_BANNER_POS lastBnLocation;

        public static void ShowBanner(AD_BANNER_POS location)
        {
            lastBnLocation = location;
            // AdsHelper.Instance.showBanner(location, App_Open_ad_Orien.Orien_Portraid, -1);
        }

        public static void ShowBanner()
        {
            // AdsHelper.Instance.showBanner(lastBnLocation, App_Open_ad_Orien.Orien_Portraid, -1);
        }

        public static void HideBanner()
        {
            // AdsHelper.Instance.hideBanner(0);
        }



        public static void HideCollapseBanner()
        {
            // AdsHelper.Instance.hideBannerCollapse();
        }

        public static void ShowRectBanner(string _place, AD_BANNER_POS location)
        {
            // if (SdkUtil.isiPad())
            // {
            //     AdsHelper.Instance.showBannerRect(_place, location, 1.3f, 0, 0, 1);
            // }
            // else
            // {
            //     AdsHelper.Instance.showBannerRect(_place, location, -1, 0, 0, 1);
            // }

        }
        public static void hideRectBanner()
        {
            // AdsHelper.Instance.hideBannerRect();
        }
    }

}
