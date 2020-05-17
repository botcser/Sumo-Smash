using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class Main : BaseInterface
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
        }
    }
}