using UnityEngine;

namespace Assets.Scripts
{
    public class WinnerScript : MonoBehaviour
    {
        private int _winner;
        void Start()
        {
            _winner = PlayerPrefs.GetInt("Winner");

        }
    }
}
