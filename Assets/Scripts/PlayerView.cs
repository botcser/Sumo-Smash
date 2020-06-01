using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerView : MonoBehaviour
    {
        public Image PlayerImage;
        public GameObject HealthImage;
        public List<Sprite> PlayerSprites;
        public ParticleSystem DustTrail;
        public ParticleSystem Ripples;
        public AnimationCurve AnimationCurve;
        public Sprite BlackPlayerButtonImage;

        private int _health;

        public void Initialize(int i, GameObject playerHealthImages, GameObject playerButton)
        {
            HealthImage = playerHealthImages;
            Health = 3;
            if (PlayerPrefs.GetInt("NewPants") == 1 && i == 0)
            {
                PlayerImage.sprite = PlayerSprites.Last();
                playerButton.GetComponent<Image>().sprite = BlackPlayerButtonImage;
            }
            else
            {
                PlayerImage.sprite = PlayerSprites[i];
            }
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health > 3)
                {
                    _health = 3;
                }
                HealthImage.GetComponentsInChildren<Image>().Where(name => name.name == $"HPImage{_health}").FirstOrDefault().enabled = true;
                HealthImage.GetComponentsInChildren<Image>().Where(name => name.name == $"HPImage{_health + 1}").FirstOrDefault().enabled = false;


            }
        }
    }
}
