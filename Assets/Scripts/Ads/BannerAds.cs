using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] string _bannerAdID = "Banner_Android";

    private void Awake()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };
        Advertisement.Banner.Load(_bannerAdID, options);
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions()
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };
        Advertisement.Banner.Show(_bannerAdID, options);
    }

    public void HideBannerAd() => Advertisement.Banner.Hide();

    void BannerShown() { }

    void BannerClicked() { }

    void BannerHidden() { }

    void BannerLoaded() { }

    void BannerLoadedError(string error) { }
}