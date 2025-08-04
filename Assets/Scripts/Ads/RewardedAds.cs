using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _rewardedAdID = "Rewarded_Android";

    private Action _onRewardComplete;

    public void LoadRewardedAd()
    {
        Advertisement.Load(_rewardedAdID, this);
    }

    public void ShowRewardedAd(Action onComplete = null)
    {
        _onRewardComplete = onComplete;
        try
        {
            Advertisement.Show(_rewardedAdID, this);
        }
        catch (Exception ex)
        {
            Debug.LogWarning("No se pudo mostrar el ad: " + ex.Message);
        }

        LoadRewardedAd(); 
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Rewarded Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Rewarded failed to load");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded Ad Clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == _rewardedAdID)
        {
            Debug.Log("Time for reward");

            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {

                _onRewardComplete?.Invoke();
                _onRewardComplete = null;    

                Debug.Log("Full rewards");

                ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
                resourceManager.AddCurrency(10);
            }
            else
            {
                Debug.Log("Rewarded Ad no completado");
            }
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Reward ad failed");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Starting reward ad");
    }
}