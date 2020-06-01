using System;
using System.Collections;
using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class PlayTutorial : MonoBehaviour
    {
        public Slider SliderTime;
        public GameObject BadText;
        public GameObject GoodText;
        public GameObject EndPanel;
        public Transform Container;
        public FoodController FoodPrefab;
        public Sprite FoodIcon;
        public Text Scores;
        public Text LastScores;

        public static bool FoodGotcha;
        public static int scores = 0;
        public static Coroutine CoroutineTimer;
        public static Coroutine CoroutineSpawnFood;
        public static bool TutorialEnd;

        private static int _masterScores = 150;

        void Start()
        {
            TutorialEnd = false;
            FoodGotcha = true;
            scores = 0;
            CoroutineTimer =  StartCoroutine(Timer());
            CoroutineSpawnFood = StartCoroutine(SpawnFoodLoop(FoodIcon));
        }

        public IEnumerator Timer()
        {
            while (SliderTime.value > 0)
            {
                yield return new WaitForSeconds(1f);
                SliderTime.value--;
                if (TutorialEnd == true)
                {
                    SliderTime.value = 0;
                }
            }

            SliderTime.enabled = false;
            EndPanel.SetActive(true);
        }

        public IEnumerator SpawnFoodLoop(Sprite icon)
        {
            while (SliderTime.value > 0)
            {
                if (FoodGotcha == true)                                         // чтобы запустить спавн мы должны сьесть...
                {
                    Scores.text = $"{scores}";
                    var food = Instantiate(FoodPrefab, Container);
                    food.GetComponent<Image>().sprite = icon;
                    food.transform.localPosition = new Vector3(Random.Range(-160, 160), Random.Range(-160, 160));
                    food.transform.Rotate(0, 0, Random.Range(-180, 180)); 

                    FoodGotcha = false;
                    scores += 5;
                    SliderTime.value += 2;
                }
                yield return new WaitForSeconds(1f);
            }

#if UNITY_ADS
            if (Advertisement.IsReady() && (DateTime.UtcNow - new DateTime(Profile.Instance.AdTimeTicks)).TotalMinutes > 5)
            {
                Advertisement.Show();
                Events.Event("Advertisement.Show()");
            }
#endif
            scores -= 5;                                                      // ... поэтому минусуем стартовое.
            LastScores.text = LocalizationManager.Localize("Tutorial.LastScores", PlayerPrefs.GetInt("scores"));

            if (PlayerPrefs.GetInt("scores") < scores)
            {
                if (scores > _masterScores)
                {
                    if (PlayerPrefs.GetInt("NewPants") == 0)
                    {
                        GoodText.SetActive(true);
                        PlayerPrefs.SetInt("NewPants", 1);
                    }
                    PlayerPrefs.SetInt("scores", scores);
                }
                else
                {
                    BadText.SetActive(true);
                    PlayerPrefs.SetInt("scores", scores);
                }
            }
        }
    }
}
