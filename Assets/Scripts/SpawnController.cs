using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Interface;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class SpawnController : MonoBehaviour
    {
        public PlayerController PlayerPrefab;
        public CPUController CPUPrefab;
        public Transform PlayerContainer;
        public List<PressButton> PressButtons;
        public List<Vector2> SpawnPositions;
        public List<GameObject> PlayerHealthImages;
        public BaseInterface StartReady;
        public List<PlayerController> Players;
        public AudioSource MenuMusic;
        public BaseInterface TutorialPanel;

        public static int PlayersCount = 1;
        public static int CPUsCount = 3;
        public static List<CPUController> CPUs;
        public static bool Tutorial = false;


        public void Awake()
        {
            CPUs = new List<CPUController>(CPUsCount);

            Debug.Log($"{PlayerPrefs.GetInt("PlayersVoice")}");
            int j;
            for (j = 0; j < PlayersCount; j++)
            {
                var player = Instantiate(PlayerPrefab, PlayerContainer);
                player.transform.localPosition = SpawnPositions[j];
                player.PressButton = PressButtons[j];
                PressButtons[j].gameObject.SetActive(true);
                player.transform.Rotate(0, 0, -45 + 180 * j);
                player.RotationSpeed *= Random.Range(0, 2) * 2 - 1;
                player.GetComponent<PlayerView>().Initialize(j, PlayerHealthImages[j], PressButtons[j].gameObject);
                player.PlayerID = (int)Mathf.Pow(2, j);
                Players.Add(player);

                if (PlayerPrefs.GetInt("PlayersVoice") == 1)       // выключено
                {
                    player.UffSound.volume = 0f;
                    player.EatingSound.volume = 0f;
                    player.FallingSound.volume = 0f;
                    player.DrippleSound.volume = 0f;
                }
                else
                {
                    player.UffSound.volume = 0.7f;
                    player.EatingSound.volume = 1.0f;
                    player.FallingSound.volume = 0.4f;
                    player.DrippleSound.volume = 1.0f;
                }
            }
            for (int i = 0; i < CPUsCount; i++)
            {
                var cpu = Instantiate(CPUPrefab, PlayerContainer);
                cpu.transform.localPosition = SpawnPositions[i + j];
                cpu.PressButton = PressButtons[i + j];
                cpu.transform.Rotate(0, 0, -45 + 180 * i);
                cpu.RotationSpeed *= Random.Range(0, 2) * 2 - 1;
                cpu.GetComponent<PlayerView>().Initialize(i + j, PlayerHealthImages[i + j], PressButtons[i + j].gameObject);
                cpu.PlayerID = (int)Mathf.Pow(2, i + j);
                cpu.Players.AddRange(Players);
                CPUs.Add(cpu);

                if (PlayerPrefs.GetInt("PlayersVoice") == 1f)       // выключено
                {
                    cpu.UffSound.volume = 0f;
                    cpu.FallingSound.volume = 0f;
                    cpu.DrippleSound.volume = 1.0f;
                }
                else
                {
                    cpu.UffSound.volume = 0.7f;
                    cpu.FallingSound.volume = 0.4f;
                    cpu.DrippleSound.volume = 1.0f;
                }
            }

            if (CPUsCount > 0)
            {
                foreach (var cpu in CPUs)
                {
                    foreach (var otherCpu in CPUs)
                    {
                        if (cpu != otherCpu)
                        {
                            cpu.Players.Add(otherCpu);
                        }
                    }
                }
            }
        }

        public void Start()
        {
            if (PlayerPrefs.GetFloat("MusicVolume") == 0f)
            {
                MenuMusic.volume = 0f;
            }

            if (Tutorial)
            {
                TutorialPanel.Open();
            }
            else
            {
                StartReady.Open();
                Analytics.CustomEvent("GameStarted",
                    new Dictionary<string, object> {{"PlayersCount", PlayersCount}, {"CPUsCount", CPUsCount}});
            }
        }

        public void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Profile.Instance.Save();
            }
        }
    }
}
