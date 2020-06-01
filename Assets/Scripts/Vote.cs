using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Assets.Scripts
{
    public class Vote : MonoBehaviour
    {
        public void OnClick()
        {
            if (PlayerPrefs.GetInt("Vote") == 0)
            {
#if UNITY_ANDROID || UNITY_IOS
                Events.Event("LIKE", "HW", SystemInfo.deviceUniqueIdentifier);
#else
                Events.Event("LIKE", "HW", "UNKNOWN?" + SystemInfo.deviceUniqueIdentifier);
#endif
                transform.gameObject.SetActive(false);
                PlayerPrefs.SetInt("Vote", 1);
            }

            Application.OpenURL("https://play.google.com/store/apps/details?id=com.sumosmash");
        }
    }
}
