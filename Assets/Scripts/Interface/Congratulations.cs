using System;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.Advertisements;
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
            if (Advertisement.IsReady() && (DateTime.UtcNow - new DateTime(Profile.Instance.AdTimeTicks)).TotalMinutes > 10)
            {
                Advertisement.Show();
            }

            CongratText.text = "Player " + PlayerPrefs.GetInt("Winner") + " win!!!";
        }
    }
}