

public static class AdIds 
{
    #if UNITY_ANDROID
    private static string appOpenTestId                 = "ca-app-pub-3940256099942544/9257395921";
    private static string adpativeBannerTestId          = "ca-app-pub-3940256099942544/9214589741";
    private static string bannerTestId                  = "ca-app-pub-3940256099942544/6300978111";
    private static string interstitialTestId            = "ca-app-pub-3940256099942544/1033173712";
    private static string rewardedTestId                = "ca-app-pub-3940256099942544/5224354917";
    private static string rewardedInterstitialTestId    = "ca-app-pub-3940256099942544/5354046379";
    private static string nativeOverlayTestId           = "ca-app-pub-3940256099942544/2247696110";
    private static string nativeVideoTestId             = "ca-app-pub-3940256099942544/1044960115";



#elif UNITY_IPHONE
    private static string appOpenTestId                 = " ca-app-pub-3940256099942544/5575463023";
    private static string bannerTestID                  = "ca-app-pub-3940256099942544/2934735716";
    private static string interstitialTestID            = "ca-app-pub-3940256099942544/4411468910";
    private static string rewardedTestId                = "ca-app-pub-3940256099942544/1712485313";
    private static string nativeOverlayTestId           = "ca-app-pub-3940256099942544/3986624511";
    private static string rewardedInterstitialTestId    = "ca-app-pub-3940256099942544/6978759866";


#else
    private static string appOpenTestId                = "unused";

    private static string bannerTestId                 = "unused";
    private static string interstitialTestId           = "unused";
    private static string rewardedTestId               = "unused";

    private static string nativeOverlayTestId          = "unused";

    private static string rewardedInterstitialTestId   = "unused";

#endif

    private static string  appOpenId                    = "?";

    private static string  bannerId                     = "?";
    private static string  interstitialId               = "?";
    private static string  rewardedId                   = "?";
    private static string  nativeOverlayId              = "?";

    private static string  rewardedInterstitialId       = "?";

    
}
