using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts
{
    public class Help : BaseInterface
    {
        public static Help Instance;
        public BaseInterface PrepareGamePanel;

        public void Awake()
        {
            Instance = this;
        }

        protected override void OnOpen()
        {
            if (PlayerPrefs.GetInt("HelpDenied") == 1)
            {
                PrepareGamePanel.Open();
            }
        }
    }
}
