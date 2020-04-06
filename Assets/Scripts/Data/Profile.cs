using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class Profile
    {
        public int Progress;
        public Settings Setting;

        public static Profile Instance;

        private const string ProfKey = "profile";

        public void Save()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(ProfKey, json);
            PlayerPrefs.Save();
        }


        public static void Load()
        {
            if (PlayerPrefs.HasKey(ProfKey))
            {
                var json = PlayerPrefs.GetString(ProfKey);
                Instance = JsonUtility.FromJson<Profile>(json);
            }
            else
            {
                Instance = new Profile();
            }
        }

    }
}