using System;
using System.Collections;
using UnityEngine;
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

        void Start()
        {
            TutorialEnd = false;
            FoodGotcha = true;
            scores = 0;
            CoroutineTimer =  StartCoroutine(Timer());
            CoroutineSpawnFood = StartCoroutine(SpawnFood(FoodIcon));
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

        public IEnumerator SpawnFood(Sprite icon)
        {
            while (SliderTime.value > 0)
            {
                if (FoodGotcha == true)
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

            LastScores.text = $"Last scores {PlayerPrefs.GetInt("scores")}";

            if (PlayerPrefs.GetInt("scores") < scores)
            {
                GoodText.SetActive(true);
                PlayerPrefs.SetInt("scores", scores);
            }
            else
            {
                BadText.SetActive(true);
            }
        }
    }
}
