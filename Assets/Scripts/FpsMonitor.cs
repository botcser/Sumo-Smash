using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FpsMonitor : MonoBehaviour
    {
        public float UpdateInterval = 0.5f;
        public static float Fps;
        public Text Text;

        public Color ColorNormal;
        public Color ColorWarning;
        public Color ColorCritical;

        private float _accum;
        private int _frames;
        private float _timeleft;

        public void Start()
        {
            _timeleft = UpdateInterval;
            gameObject.SetActive(true);
        }

        public void Update()
        {
            _timeleft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            ++_frames;

            if (_timeleft <= 0.0)
            {
                var fps = _accum / _frames;
                var format = $"{fps:F0} FPS ";

                Text.text = format;
                Text.color = fps >= 30 ? ColorNormal : fps >= 10 ? ColorWarning : ColorCritical;

                _timeleft = UpdateInterval;
                _accum = 0.0F;
                _frames = 0;

                Fps = fps;
            }
        }
    }
}