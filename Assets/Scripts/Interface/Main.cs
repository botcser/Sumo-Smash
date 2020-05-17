using System;
using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Scripts.Interface
{
    public class Main : BaseInterface, IUnityAdsListener
    {
        public static Main Instance;
        public BaseInterface MainMenu;
        public AudioSource MenuMusic;

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

            if (!Advertisement.isInitialized)
            {
                #if UNITY_ANDROID || UNITY_EDITOR
                Advertisement.Initialize("3609028");
                #elif UNITY_IOS
                Advertisement.Initialize("3609029");
                #endif
                Advertisement.AddListener(this);

            }
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log("OnUnityAdsReady");
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.Log("OnUnityAdsDidError");
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

            Debug.Log("OnUnityAdsDidStart");
        }
    }
}