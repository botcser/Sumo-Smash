using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class Pause : BaseInterface
    {
        public static Pause Instance;

        public void Awake()
        {
            Instance = this;
        }

        protected override void OnOpen()
        {
            Time.timeScale = 0f;
        }

        protected override void OnClose()
        {
            Time.timeScale = 1f;
        }
    }
}

