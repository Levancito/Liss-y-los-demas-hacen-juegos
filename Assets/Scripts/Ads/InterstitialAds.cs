using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _interstitialAdID = "Interstitial_Android";

    public void LoadInterstitialAd()
    {
        Advertisement.Load(_interstitialAdID, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(_interstitialAdID, this);
        LoadInterstitialAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial loaded");
    }

    public void OnUnityAdsFailedToLoad(string pacementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Interstitial failed to load");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Interstitial ad clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial ad completed");
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Interstitial ad failed to show");
    }
    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Starting interstitial ad");
    }
}
