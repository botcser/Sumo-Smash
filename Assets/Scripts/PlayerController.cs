using UnityEngine;

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

        public int HP = 3;


        public void Update()
        {
            if (PressButton.Pressed)
            {
                RotationSpeed *= -1;
            }

            if (PressButton.Pressed)
            {
                Rigidbody.AddForce(transform.up * MovementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else if (Rigidbody.velocity.magnitude < 10)
            {
                transform.Rotate(0, 0, Time.deltaTime * RotationSpeed);
            }

            Velocity = Rigidbody.velocity.magnitude;
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {
            var vector = (coll.transform.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, vector) < 90)
            {
                coll.transform.GetComponent<Rigidbody2D>().AddForce(vector * CollisionForce, ForceMode2D.Impulse);
            }
            Debug.Log(Vector2.Angle(transform.up, vector));

        }
    }
}