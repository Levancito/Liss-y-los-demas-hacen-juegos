using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    [SerializeField] InitializeAds _initializeAds;
    [SerializeField] RewardedAds _rewardedAds;
    [SerializeField] BannerAds _bannerAds;
    [SerializeField] InterstitialAds _interstitialAds;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        _rewardedAds.LoadRewardedAd();

        //StartCoroutine(BannerAd());

        //_interstitialAds.LoadInterstitialAd();
        //StartCoroutine(InterstitialAd());
    }

    [ContextMenu("Test Rewarded")]  public void ShowRewardedAd() => _rewardedAds.ShowRewardedAd();

    IEnumerator BannerAd()
    {
        while (true)
        {
            _bannerAds.LoadBannerAd();
            yield return new WaitForSeconds(5f);
            _bannerAds.ShowBannerAd();
            yield return new WaitForSeconds(30f);
            _bannerAds.HideBannerAd();
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator InterstitialAd()
    {
        yield return new WaitForSeconds(15f);
        _interstitialAds.ShowInterstitialAd();
    }
}
