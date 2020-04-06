using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class SpawnController : MonoBehaviour
    {
        public PlayerController PlayerPrefab;
        public Transform PlayerContainer;
        public List<PressButton> PressButtons;
        public List<Vector2> SpawnPositions;
        public List<Image> PlayerHealthImages;
        

        public static int PlayerCount = 2;


        public void Awake()
        {
            Profile.Load();
        }

        public void Start()
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                var player = Instantiate(PlayerPrefab, PlayerContainer);
                player.transform.localPosition = SpawnPositions[i];
                player.PressButton = PressButtons[i];
                player.transform.Rotate(0, 0, -45 + 180*i);
                player.RotationSpeed *= Random.Range(0, 2) * 2 - 1;
                player.GetComponent<PlayerView>().Initialize(i, PlayerHealthImages[i]);
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
