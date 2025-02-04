using System.Runtime.ConstrainedExecution;
using UnityEngine;

public static class Config
{

    public static bool shouldShowFull = true;

    public static int ConfigBannerPosition
    {
        get
        {
            int ret = PlayerPrefs.GetInt("cf_banner_position", 0);
            return Mathf.Clamp(ret, 0, 1);
        }

    }

    public static int ConfigShowQuestProgress
    {
        get
        {
            int ret = PlayerPrefs.GetInt("cf_show_quest_progress", 1);
            return Mathf.Clamp(ret, 0, 1);
        }

    }

    // 0 -> win -> quest
    // 1 -> quest -> win
    public static int ConfigShowScreenOrder
    {
        get
        {
            int ret = PlayerPrefs.GetInt("cf_show_screen_order", 0);
            return Mathf.Clamp(ret, 0, 1);
        }

    }

    public static class Screen
    {
        public static string ScreenMainMenu = "Screen/ScreenMainMenu";
    }

    public static class Popup
    {
        public static string PopupMessage = "Popup/PopupMessage";
        public static string PopupSetting = "Popup/PopupSetting";
        public static string PopupWinMiniGame = "Popup/PopupWinMiniGame";
        public static string PopupShopTheme = "Popup/ShopTheme";
        public static string PopupBuyItem = "Popup/PopupBuyItem";
        public static string PopupUnlockTheme = "Popup/PopupUnlockTheme";
        public static string PopupRemoveAds = "Popup/PopupRemoveAds";
        public static string PopupTimeOver = "Popup/PopupTimeOver";
        public static string PopupListLevel = "Popup/PopupListLevel";
        public static string PopupQuestProgress = "Popup/PopupQuestProgress";
        public static string PopupInternet = "Popup/PopupInternet";
        public static string PopupChooseOption = "Popup/PopupChooseOption";
        public static string PopupNewMode = "Popup/PopupNewMode";
        public static string PopupLoseMiniGame = "Popup/PopupLoseMiniGame";
        public static string PopupShowCutScene = "Popup/PopupShowCutScene";
    }

    public static class Screw
    {
        public static float forceImpactMinForAudio = 2;
        public static int HoleLayerMask = 64;
        public static int ScrewLayerMask = LayerMask.GetMask("ScrewLayer1", "ScrewLayer2", "ScrewLayer3", "ScrewLayer4", "ScrewLayer5", "ScrewLayer6", "ScrewLayer7", "ScrewLayer8");

        public static int PlateLayer1 = 7;
        public static int PlateLayer2 = 8;
        public static int PlateLayer3 = 9;
        public static int PlateLayer4 = 10;
        public static int PlateLayer5 = 11;
        public static int PlateLayer6 = 12;
        public static int PlateLayer7 = 13;
        public static int PlateLayer8 = 14;
        public static int InputPlayer = 15;

        public const bool EnableAdsUnlockHoleBtn = false;

    }

    public struct KeyRemoteConfig
    {
        public const string configLevelRescue = "cf_level_rescue";
        public const string configLevel = "cf_level";
        public const string configListLevel = "cf_list_level";
        public const string gameConfigs = "game_configs";
    }

    public struct KeySkin_MainGirl
    {
        public const string dirty = "Dirty_girl";
        public const string normal = "Normal_girl";
        public const string outline = "Outline_dress";
        public const string vip = "Vip_girl";
    }
    public struct KeySkin_MeoMeo
    {
        public const string dirty = "Dirty";
        public const string normal = "Khan";
        public const string vip = "Vip";
    }
}
