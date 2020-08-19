using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{

    private string gameId = "3772945";
    public bool testMode = true;

    void Start()
    {
#if UNITY_IOS
            gameId = "3772944";
#elif UNITY_ANDROID
            gameId = "3772945";
#endif
       Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
}
