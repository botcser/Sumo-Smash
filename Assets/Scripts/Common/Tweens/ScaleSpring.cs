using UnityEngine;

namespace Assets.Scripts.Common.Tweens
{
    public class ScaleSpring : TweenBase
    {
        public Vector3 From;
        public Vector3 To;
        public float Dumping;

        private Vector3 _scale;
        private float _amplitude = 1;

        public static void Begin(Component target, Vector3 from, Vector3 to, float speed, float dumping)
        {
            var component = target.GetComponent<ScaleSpring>() ?? target.gameObject.AddComponent<ScaleSpring>();

            component.From = from;
            component.To = to;
            component.Speed = speed;
            component.Dumping = dumping;
            component.enabled = true;
        }

        protected override void OnUpdate()
        {
            _amplitude = Mathf.Max(0, _amplitude - Dumping * Time.deltaTime);

            var x = _scale.x * (From.x + (To.x - From.x) * Sin() * _amplitude);
            var y = _scale.y * (From.y + (To.y - From.y) * Sin() * _amplitude);
            var z = _scale.z * (From.z + (To.z - From.z) * Sin() * _amplitude);

            transform.localScale = new Vector3(x, y, z);
     
            if (_amplitude <= 0)
            {
                enabled = false;
            }
        }

        public override void OnEnable()
        {
            _scale = transform.localScale;
            base.OnEnable();
            Reset();
        }

        public void OnDisable()
        {
            transform.localScale = _scale;
        }

        public void Reset()
        {
            _amplitude = 1;
        }
    }
}