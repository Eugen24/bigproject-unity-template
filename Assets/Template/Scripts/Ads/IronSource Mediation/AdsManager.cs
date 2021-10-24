using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour
{
    public static AdsManager inst;

    [Header("Ads Data Keys Manager")]
    public string androidAppKey;
    public string iosAppKey;

    [Header("Giveaways")]
    private bool cash_reward;
    private bool reward_got;

    [Header("Menu References")]
    //public PlayMenuLocal menu_manager;
    //public ProgressBar loading_bar;

    public RuntimePlatform platform;

    private void Awake()
    {
        var num = FindObjectsOfType<AdsManager>();
        if (num.Length != 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
        if (!inst) inst = this;
        platform = Application.platform;

        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            //IRON SOURCE
            string YOUR_APP_KEY = "";

            if (platform == RuntimePlatform.Android)
            {
                YOUR_APP_KEY = androidAppKey;
            }
            if (platform == RuntimePlatform.IPhonePlayer)
            {
                YOUR_APP_KEY = iosAppKey;
            }

            IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER, IronSourceAdUnits.OFFERWALL);
            IronSource.Agent.validateIntegration();
            IronSource.Agent.setConsent(true);
            IronSourceConfig.Instance.setClientSideCallbacks(true);
        }
    }

    private void FixedUpdate()
    {
        if (!inst) inst = this;
    }

    public void OnNativeExceptionReceivedFromSDK(string message)
    {
        //handle exception
        Debug.Log("OnNativeExceptionReceivedFromSDK: " + message);
    }

    public IEnumerator Start()
    {
        //GAMEANALYTICS
        if (!ObscuredPrefs.HasKey("PlayerName"))
        {
            ObscuredPrefs.SetString("PlayerName", "User" + Random.Range(0, 100).ToString());
        }
        GameAnalytics.Initialize();
        GameAnalytics.SetCustomId(ObscuredPrefs.GetString("PlayerName"));

        yield return new WaitForSeconds(1f);

        if (platform != RuntimePlatform.OSXPlayer && platform != RuntimePlatform.WindowsPlayer && platform != RuntimePlatform.LinuxPlayer)
        {
            setupCallbacks();
            IronSource.Agent.loadInterstitial();
        }
    }

    public void NextLevelStory()
    {
        print("<color=yellow>NEXT LEVEL STORY AD</color>");

        if (ObscuredPrefs.GetInt("RemovedAds") == 0)
        {
            if (ObscuredPrefs.GetInt("vip_user") == 0)
            {
                ShowInterestial(ObscuredPrefs.GetString("END_RACE_PLACEMENT"));
                //RewardAd(true, ObscuredPrefs.GetString("END_RACE_PLACEMENT"));
            }
        }
        else
        {
            print("<color=yellow>Remove Ads " + ObscuredPrefs.GetInt("RemovedAds") + " VIP USER " + ObscuredPrefs.GetInt("vip_user") + "</color>");
        }
    }

    public void resetCar()
    {
        //INTERESTIAL
        if (ObscuredPrefs.GetInt("RemovedAds") == 0)
        {
            if (ObscuredPrefs.GetInt("vip_user") == 0)
            {
                print("<color=yellow>RESPAWN AD REQUESTED!</color>");
                ShowInterestial("RESPAWN");
                //RewardAd(true, "RESPAWN");
            }
        }
    }

    public void ShowInterestial(string placement)
    {
        if (platform != RuntimePlatform.OSXPlayer && platform != RuntimePlatform.WindowsPlayer)
        {
            print("INTERESTIAL AD PLACEMENT: " + placement);

            if (ObscuredPrefs.GetInt("RemovedAds") == 0)
            {
                if (ObscuredPrefs.GetInt("vip_user") == 0)
                {
                    if (IronSource.Agent.isInterstitialReady())
                    {
                        //increaseAddCount();
                        IronSource.Agent.showInterstitial(placement);
                    }
                }
            }
            else
            {
                print("<color=yellow>Remove Ads " + ObscuredPrefs.GetInt("RemovedAds") + " VIP USER " + ObscuredPrefs.GetInt("vip_user") + "</color>");
            }
        }
    }

    public void doubleMyRewardShow(string placement)
    {
        if (placement == "Main_Menu")
        {
            ShowAd(placement);
        }
    }

    public void RewardAd(bool forced_ad, string placement)
    {
        cash_reward = !forced_ad;
        print("forced_ad: " + forced_ad);
        print("REWARD AD PLACEMENT: " + placement);
        print("RemovedAds: " + ObscuredPrefs.GetInt("RemovedAds"));

        if (ObscuredPrefs.GetInt("RemovedAds") == 0)
        {
            if (ObscuredPrefs.GetInt("vip_user") == 0)
            {
                print("<color=yellow>Remove Ads " + ObscuredPrefs.GetInt("RemovedAds") + " VIP USER " + ObscuredPrefs.GetInt("vip_user") + "</color>");
                ShowAd(placement);
            }
            else
            {
                doubleMyRewardShow(placement);
            }
        }
        else
        {
            print("<color=yellow>Remove Ads " + ObscuredPrefs.GetInt("RemovedAds") + " VIP USER " + ObscuredPrefs.GetInt("vip_user") + "</color>");

            doubleMyRewardShow(placement);
        }
    }

    public void ShowAd(string placement)
    {
        if (platform != RuntimePlatform.OSXPlayer && platform != RuntimePlatform.WindowsPlayer)
        {
            reward_got = true;
            print("SHOWADDPLACEMENT +" + placement);

            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo(placement);

                GameAnalytics.StartTimer(placement);
            }
            else
            {
                print("SHOW AD PLACEMENT NOT AVAILABLE" + placement);
                GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "ironsource", placement);
            }
        }
    }

    //RESPONSE
    public void Response(string placementName)
    {
        if (reward_got)
        {
            Debug.Log("RESPONSE" + placementName);

            //if (placementName == "Main_Menu" || placementName == "IAP_Store")
            //{
            //    if (FindObjectOfType<PlayMenuLocal>())
            //    {
            //        ObscuredPrefs.SetInt("Cash", ObscuredPrefs.GetInt("Cash") + 1000);
            //        menu_manager.StartCoroutine(menu_manager.Reset_USD());
            //    }
            //}

            if (placementName == "DoubleEarnedCoins")
            {
                //if (FindObjectOfType<PlayMenuLocal>())
                //{
                ObscuredPrefs.SetInt("Cash", ObscuredPrefs.GetInt("Cash") + 1000);
                //menu_manager.StartCoroutine(menu_manager.Reset_USD());
                //}
            }

            reward_got = false;
        }
    }

    public void increaseAddCount()
    {
        if (ObscuredPrefs.GetInt("RemovedAds") == 0)
        {
            if (ObscuredPrefs.GetInt("vip_user") == 0)
            {
                if (ObscuredPrefs.GetInt("AdsShowed") < 3)
                {
                    ObscuredPrefs.SetInt("AdsShowed", ObscuredPrefs.GetInt("AdsShowed") + 1);
                }
                else
                {
                    //if (GameObject.FindObjectOfType<PlayMenuLocal>())
                    //{
                    //    ObscuredPrefs.SetInt("AdsShowed", 0);
                    //    //GameObject.FindObjectOfType<PlayMenuLocal>().Go_RemoveAds();
                    //}
                    //else
                    //{
                    //    ObscuredPrefs.SetInt("AdsShowed", 2);
                    //}
                }
            }
        }
        else
        {
            print("<color=yellow>Remove Ads " + ObscuredPrefs.GetInt("RemovedAds") + " VIP USER " + ObscuredPrefs.GetInt("vip_user") + "</color>");
        }
    }

    void setupCallbacks()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
    }

    //REWARD VIDEO IRON SOURCE

    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
        print("IronSource - RewardedVideoAdOpenedEvent");
    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
        print("IronSource - RewardedVideoAdClosedEvent");

        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.UnPause();
        //}
        AudioListener.volume = 1;

        string placement = "placement";

        if (placement != null)
        {
            long elapsedTime = GameAnalytics.StopTimer(placement);
            // send ad event for tracking elapsedTime
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "ironsource", placement, elapsedTime);
            placement = null;

            // OR if you do not wish to track time

            // send ad event without tracking elapsedTime
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "ironsource", placement);
        }
    }

    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available.
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.

    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        print("IronSource - RewardedVideoAvailabilityChangedEvent ->" + available);
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
    }

    //  Note: the events below are not available for all supported rewarded video
    //   ad networks. Check which events are available per ad network you choose
    //   to include in your build.
    //   We recommend only using events which register to ALL ad networks you
    //   include in your build.

    //Invoked when the video ad starts playing.
    void RewardedVideoAdStartedEvent()
    {
        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.Pause();
        //}
        AudioListener.volume = 0;

        print("IronSource - RewardedVideoAdStartedEvent");
    }

    //Invoked when the video ad finishes playing.
    void RewardedVideoAdEndedEvent()
    {
        print("IronSource - RewardedVideoAdEndedEvent");

        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.UnPause();
        //}
        AudioListener.volume = 1;
    }

    //Invoked when the user completed the video and should be rewarded.
    //If using server-to-server callbacks you may ignore this events and wait for the callback from the  ironSource server.
    //
    //@param - placement - placement object which contains the reward data
    //

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        print("ironSource Reward Video Finished");

        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.UnPause();
        //}
        AudioListener.volume = 1;
    }

    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        print("IronSource -RewardedVideoAdShowFailedEvent");
        print(error.getDescription());

        //GameAnalytics FailedAdShowEvent
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "ironsource", error.getDescription());
    }

    //INTERESTIAL
    //Invoked when the initialization process has failed.
    //@param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        print("IronSource -InterstitialAdLoadFailedEvent");
        print(error.getDescription());
    }

    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
        print("IronSource -InterstitialAdShowSucceededEvent");
    }

    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        print("IronSource -InterstitialAdShowFailedEvent");
        print(error.getDescription());

        //GameAnalytics FailedAdShowEvent
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "ironsource", error.getDescription());
    }

    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
        print("IronSource - InterstitialAdClickedEvent");

        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.UnPause();
        //}
        AudioListener.volume = 1;

        //GameAnalytics ClickedAdShowEvent
        GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Interstitial, "ironsource", "IronSource - InterstitialAdClickedEvent");

        //if (menu_manager.showing_pre_join_ad)
        //{
        //    menu_manager.OnJoinedRoom();
        //}
    }

    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        print("IronSource -InterstitialAdClosedEvent");
        IronSource.Agent.loadInterstitial(); //PREPARE NEW INTERESTIAL

        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.UnPause();
        //}
        AudioListener.volume = 1;
    }

    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
        print("IronSource - InterstitialAdReadyEvent");
    }

    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
        print("IronSource - InterstitialAdOpenedEvent");

        //AUDIO SCRIPT
        //if (GameObject.FindObjectOfType<random_song>())
        //{
        //    GameObject.FindObjectOfType<random_song>().audio.UnPause();
        //}
        AudioListener.volume = 0;

        //GameAnalytics LoadedAdShowEvent
        GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "ironsource", "IronSource - InterstitialAdOpenedEvent");
    }

    void OnApplicationPause(bool isPaused)
    {
        print("ON PAUSE EVENT IS->" + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
        string placement = "placement";

        if (isPaused)
        {
            if (placement != null)
            {
                GameAnalytics.PauseTimer(placement);
            }
        }
        else
        {
            if (placement != null)
            {
                GameAnalytics.ResumeTimer(placement);
            }
        }
    }

}
