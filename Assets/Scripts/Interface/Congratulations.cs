using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
    public class Congratulations : BaseInterface
    {
        public static Congratulations Instance;
        public Text CongratText;

        public void Awake()
        {
            Instance = this;
        }

        protected override void OnOpen()
        {
            CongratText.text = "Player " + PlayerPrefs.GetInt("Winner") + " win!!!";
        }
    }
}