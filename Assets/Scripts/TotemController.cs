using System;
using System.Collections;
using Assets.Scripts.Common.Tweens;
using Assets.Scripts.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class TotemController : MonoBehaviour
    {
        public BaseInterface CongratWindow;

        private int _playerCount;
        private int _winner;
        

        public void Awake()
        {
            _playerCount = SpawnController.PlayersCount + SpawnController.CPUsCount;
            _winner = (int)Math.Pow(2, _playerCount) - 1;
        }

        private void OnTriggerExit2D(Collider2D objectCollider)
        {
            var player = objectCollider.GetComponent<PlayerView>();

            if (objectCollider.tag == "Player")
            {
                player.Health--;
                player.GetComponent<PlayerController>().OnTotem = false;
                Debug.Log($"On totem {player.GetComponent<PlayerController>().OnTotem} {player.GetComponent<PlayerController>().PlayerID}");

                if (player.Health == 0)
                {
                    player.Ripples.Play();
                    player.GetComponent<PlayerController>().DrippleSound.Play();
                    _winner = _winner - player.GetComponent<PlayerController>().PlayerID;
                    PlayerFall(player).OnComplete = () =>
                    {
                        if (SpawnController.CPUsCount > 0)
                        {
                            DeleteCPUTarget(player.GetComponent<PlayerController>());
                        }
                        Destroy(objectCollider.gameObject);
                    };
                    _playerCount--;

                    if (SpawnController.Tutorial)
                    {
                        PlayTutorial.TutorialEnd = true;
                    }
                    else if (_playerCount == 1)
                    {
                        GameEnd();
                    }
                }
                else
                {
                    PlayerFall(player).OnComplete = () => { StartCoroutine(StartRipplesAndSpawn(player)); };
                }
            }
        }

        void DeleteCPUTarget(PlayerController player)
        {
            if (SpawnController.CPUsCount > 0)
            {
                foreach (var cpu in SpawnController.CPUs)
                {
                    cpu.Players.Remove(player);
                }
            }
        }

        Tween PlayerFall(PlayerView player)
        {
            player.GetComponent<PlayerController>().MovementSpeed = 1;
            player.GetComponent<PlayerController>().FallingSound.Play();
            player.GetComponent<Collider2D>().isTrigger = true;

            var commonTween = Tween.Common(player, progress =>
            {
                player.PlayerImage.color = new Color(1, 1, 1, progress);
                player.transform.localScale = Vector3.one * progress;
            }, 11, 1, 0, 1f);

            player.DustTrail.Stop();
            commonTween.AnimationCurve = player.AnimationCurve;

            return commonTween;
        }

        public void SpawnPlayer (PlayerView player)
        {
            player.GetComponent<PlayerController>().MovementSpeed = player.GetComponent<PlayerController>().BuffedMovementSpeed;
            player.GetComponent<Collider2D>().isTrigger = false;
            player.transform.localPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50));
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.PlayerImage.color = Color.white;
            player.transform.localScale = Vector3.one;
            player.DustTrail.Play();
            player.GetComponent<PlayerController>().OnTotem = true;
        }

        void GameEnd()
        {
            switch (_winner)
            {
                case 8:
                    PlayerPrefs.SetInt("Winner", 4);
                    break;
                case 4:
                    PlayerPrefs.SetInt("Winner", 3);
                    break;
                case 2:
                    PlayerPrefs.SetInt("Winner", 2);
                    break;
                case 1:
                default:
                    PlayerPrefs.SetInt("Winner", 1);
                    break;
            }
            CongratWindow.Open();
        }


        IEnumerator StartRipplesAndSpawn(PlayerView player)
        {
            player.GetComponent<PlayerController>().DrippleSound.Play();
            player.Ripples.Play();
            yield return new WaitForSeconds(3);
            SpawnPlayer(player);
        }
    }
}
