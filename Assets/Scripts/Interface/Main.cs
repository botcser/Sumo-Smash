using System;
using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

namespace Assets.Scripts.Interface
{
#if UNITY_ADS
    public class Main : BaseInterface, IUnityAdsListener
#else
    public class Main : BaseInterface
#endif
    {
        public static Main Instance;
        public BaseInterface MainMenu;
        public AudioSource MenuMusic;
        public static bool DEBUG;

        public void Awake()
        {
            Instance = this;
            Profile.Load();
            LocalizationManager.Read();
            LocalizationManager.Language = Profile.Instance.Settings.Language;
        }

        public void Start()
        {
            MainMenu.Open();
            if (PlayerPrefs.GetFloat("MusicVolume") == 0f)
            {
                MenuMusic.volume = 0f;
            }
#if UNITY_ADS
            if (!Advertisement.isInitialized)
            {

#if UNITY_ANDROID || UNITY_EDITOR
                Advertisement.Initialize("3609028");
#elif UNITY_IOS
                Advertisement.Initialize("3609029");
#endif
                Advertisement.AddListener(this);
            }
#endif
        }

#if UNITY_ADS
        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log("OnUnityAdsReady");
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.Log("OnUnityAdsDidError");
            Events.Event("OnUnityAdsDidError", "message", message);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Debug.Log("OnUnityAdsDidStart");
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult != ShowResult.Failed)
            {
                Profile.Instance.AdTimeTicks = DateTime.UtcNow.Ticks;
            }

            Events.Event("OnUnityAdsDidFinish", "placementId", placementId, "showResult", showResult);
            Debug.Log("OnUnityAdsDidStart");
        }
#endif
    }
}