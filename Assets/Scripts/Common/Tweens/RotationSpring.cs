using UnityEngine;

namespace Assets.Scripts.Common.Tweens
{
    public class RotationSpring : TweenBase
    {
        public Axis Axis;
        public float From;
        public float To;

        private float _rotation;

        public override void OnEnable()
        {
            base.OnEnable();

            var rotation = transform.localEulerAngles;

            switch (Axis)
            {
                case Axis.X: _rotation = rotation.x; break;
                case Axis.Y: _rotation = rotation.y; break;
                case Axis.Z: _rotation = rotation.z; break;
            }
        }

        public void OnDisable()
        {
            transform.localEulerAngles = GetRotation(_rotation);
        }

        protected override void OnUpdate()
        {
            transform.localEulerAngles = GetRotation(_rotation + From + (To - From) * Sin());
        }

        private Vector3 GetRotation(float angle)
        {
            var rotation = transform.localEulerAngles;

            switch (Axis)
            {
                case Axis.X: rotation.x = angle; break;
                case Axis.Y: rotation.y = angle; break;
                case Axis.Z: rotation.z = angle; break;
            }

            return rotation;
        }
    }
}