using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEditor;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

namespace Assets.Scripts.Interface
{
    public class Main : BaseInterface
    {
        public static Main Instance;
        public BaseInterface MainMenu;
        public AudioSource MenuMusic;
        public static bool DEBUG;
        public ParticleSystem SmokePrefab;

        public void Awake()
        {
            Instance = this;
            Profile.Load();
            LocalizationManager.Read();
            LocalizationManager.Language = Profile.Instance.Settings.Language;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(SmokePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward, transform.rotation);
            }
        }
        
        public void Start()
        {
            MainMenu.Open();
            if (PlayerPrefs.GetFloat("MusicVolume") == 0f)
            {
                MenuMusic.volume = 0f;
            }
            //PlayerPrefs.SetInt("scores", 0);
            //PlayerPrefs.SetInt("NewPants", 0);
            //PlayerPrefs.SetInt("HelpDenied", 0);
        }
    }
}