using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;
        public float RotationSpeed;
        public float MovementSpeed;

        private int _rotateDirection = 1;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _rotateDirection *= -1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Rigidbody.AddForce(transform.up * MovementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                transform.Rotate(0, 0, Time.deltaTime * RotationSpeed * _rotateDirection);
            }
        }
    }
}