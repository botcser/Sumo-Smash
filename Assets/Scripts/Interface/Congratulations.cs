using System;
using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
    public class Congratulations : BaseInterface
    {
        public Text CongratText;

        public static Congratulations Instance;

        public void Awake()
        {
            Instance = this;
        }

        protected override void OnOpen()
        {
#if UNITY_ADS
            if (Advertisement.IsReady() && (DateTime.UtcNow - new DateTime(Profile.Instance.AdTimeTicks)).TotalMinutes > 5)
            {
                Advertisement.Show();
                Events.Event("Advertisement.Show()");
            }
#endif
            CongratText.text = LocalizationManager.Localize("Congrat.Winner", PlayerPrefs.GetInt("Winner"));
        }
    }
}