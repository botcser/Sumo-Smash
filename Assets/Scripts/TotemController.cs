using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts
{
    public class TotemController : MonoBehaviour
    {

        private void OnTriggerExit2D(Collider2D objectCollider)
        {
            var playerController = objectCollider.GetComponent<PlayerController>();
            if (objectCollider.tag == "Player")
            {
                playerController.HP--;
                if (playerController.HP == 0)
                    Destroy(objectCollider.gameObject);
                else
                {
                    SpawnPlayer(playerController);
                }
            }
        }

        void SpawnPlayer(PlayerController playerController)
        { 
            playerController.transform.localPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50));
            playerController.Rigidbody.velocity = Vector2.zero;

            Profile.Instance.Setting.MusicVolume = 0;
        }
    }
}
