using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ReturnToMainMenu : MonoBehaviour
    {

        public void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}