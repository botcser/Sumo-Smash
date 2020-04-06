using UnityEngine;

namespace Assets.Scripts
{
    public class TotemController : MonoBehaviour
    {

        private void OnTriggerExit2D(Collider2D objectCollider)
        {
            var player = objectCollider.GetComponent<PlayerView>();

            if (objectCollider.tag == "Player")
            {
                player.Health--;

                if (player.Health == 0)
                {
                    Destroy(objectCollider.gameObject);
                }
                else
                {
                    SpawnPlayer(player.transform);
                }
            }
        }

        void SpawnPlayer(Transform player)
        {
            player.localPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50));
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
