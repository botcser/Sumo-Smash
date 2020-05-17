using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlayAgain : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}
