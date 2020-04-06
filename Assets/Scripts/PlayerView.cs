using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerView : MonoBehaviour
    {
        public Image PlayerImage;
        public Image HealthImage;
        public List<Sprite> HealthSprites;
        public List<Sprite> PlayerSprites;

        private int _health;

        public void Initialize(int i, Image playerHealthImage)
        {
            HealthImage = playerHealthImage;
            Health = 3;
            PlayerImage.sprite = PlayerSprites[i];
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health == 1)
                {
                    HealthImage.enabled = false;
                }
                else if (_health > 1)
                {
                    HealthImage.sprite = HealthSprites[_health - 1];
                }
            }
        }

    }
}
