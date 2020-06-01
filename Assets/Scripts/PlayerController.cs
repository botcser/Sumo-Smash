using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common.Tweens;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;
        public float RotationSpeed;
        public float MovementSpeed;
        public float CollisionForce;
        public float Velocity;
        public PressButton PressButton;
        public int PlayerID;
        public bool OnTotem = true;

        public AudioSource UffSound;
        public AudioSource FallingSound;
        public AudioSource EatingSound;
        public AudioSource DrippleSound;

        public float BuffedMovementSpeed;


        public void Start()
        {
            PressButton.OnPointerDownEvent.AddListener(() => { RotationSpeed *= -1; });
            BuffedMovementSpeed = MovementSpeed;
        }

        public void Update()
        {
            if (PressButton.Pressed)
            {
                Rigidbody.AddForce(transform.up * MovementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else if (Rigidbody.velocity.magnitude < 0.2)
            {
                transform.Rotate(0, 0, Time.deltaTime * RotationSpeed);
            }

            Velocity = Rigidbody.velocity.magnitude;                                                                    // DEBUG
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.transform.tag == "Player")
            {
                //Debug.Log($"Collision {PlayerID} and {coll.transform.GetComponent<PlayerController>().PlayerID}");
                var vector = (coll.transform.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, vector) < 90)
                {
                    this.GetComponent<PlayerController>().UffSound.Play();
                    coll.transform.GetComponent<Rigidbody2D>().AddForce(vector * CollisionForce, ForceMode2D.Impulse);
                }

                coll.transform.GetComponentInChildren<ScaleSpring>().enabled = true;
                //Debug.Log(Vector2.SignedAngle(transform.up, vector) + ":" + transform.GetComponent<PlayerController>().PlayerID);
            }
            else if (coll.transform.tag == "Food")
            {
                StartCoroutine(Eating(coll));
            }
        }
        
        IEnumerator Eating(Collision2D coll)
        {
            Destroy(coll.gameObject);
            EatingSound.Play();
            yield return new WaitForSeconds(1.5f);
            PlayTutorial.FoodGotcha = true;
        }
    }
}