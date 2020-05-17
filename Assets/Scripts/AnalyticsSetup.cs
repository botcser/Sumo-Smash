using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class AnalyticsSetup : MonoBehaviour
    {
        public GameObject AppMetricaGooglePlay;
        public GameObject AppMetricaAppStore;

        public void OnValidate()
        {
            foreach (var app in new[] { AppMetricaGooglePlay, AppMetricaAppStore })
            {
                app.SetActive(false);
            }

            #if UNITY_ANDROID

            AppMetricaGooglePlay.SetActive(true);

            #elif UNITY_IOS

            AppMetricaAppStore.SetActive(true);

            #endif
        }
    }
}