using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlayPlayerVSPlayer : MonoBehaviour
    {
        public int PlayersCount;
        public int CPUsCount;
        public bool Tutorial;

        public void OnClick()
        {
            SpawnController.PlayersCount = PlayersCount;
            SpawnController.CPUsCount = CPUsCount;
            SpawnController.Tutorial = Tutorial;
            if (PlayTutorial.CoroutineTimer != null)
            {
                StopCoroutine(PlayTutorial.CoroutineTimer);
            }
            if (PlayTutorial.CoroutineSpawnFood != null)
            {
                StopCoroutine(PlayTutorial.CoroutineSpawnFood);
            }
            SceneManager.LoadScene(1);
        }
    }
}
