using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common.Tweens;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class CPUController : PlayerController
    {
        public List<PlayerController> Players;
        public float Dist1;
        public float Angle1;
        public float Dist2;
        public float Angle2;

        private float _FullRotateTime = 3.2f;
        private float _MaxDistanceTime = 3.2f;
        private int _OnHourRotateFlag;

        private float AngleEffitience = 0.01f;                              // коефициент эффективности цели по углу
        private float _DesigionDelay = 0.5f;                                // скорость сооброжалки
        private float _DeltaEfficiency = 0.1f;                            // точность определения эфферктивности
        private float _DeltaTargetAngle = 5;                           // точность прицеливания

        private PlayerController _Target;


        public IEnumerator Start()
        {
            BuffedMovementSpeed = MovementSpeed;
            yield return new WaitForSeconds(4);
            yield return FindTargetAndAttack(Players);                           // в Z передается команда сменить вращение, поэтому Vector3
        }

        public new void Update()
        {
            if (PressButton.Pressed)
            {
                RotationSpeed *= -1;
                _OnHourRotateFlag = -Math.Sign(RotationSpeed);
            }

            if (PressButton.Pressed)
            {
                Rigidbody.AddForce(transform.up * MovementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else if (Rigidbody.velocity.magnitude < 0.2)
            {
                transform.Rotate(0, 0, Time.deltaTime * RotationSpeed);
            }
        }

        IEnumerator FindTargetAndAttack(List<PlayerController> players)
        {
            while (true)
            {
                var myRotateDirection = -Math.Sign(RotationSpeed);
                float direction;

                yield return(null);
                if (_Target == null || _Target.OnTotem == false)
                {
                    PressButton.Pressed = false;
                    _Target = Players.OrderBy(i =>
                    {
                        var distance = Vector2.Distance(i.transform.position, transform.position);
                        var angle = Vector2.Angle(transform.up, i.transform.position - transform.position);
                        return distance + angle * AngleEffitience;
                    }).FirstOrDefault();

                    if (_Target == null)
                    {
                        yield break;
                    }

                    if (_Target.OnTotem == false)
                    {
                        _Target = null;
                        continue;
                    }
                }
                else if (_Target.OnTotem == true)
                {
                    var secondTarget = Players.OrderBy(i =>
                    {
                        var distance = Vector2.Distance(i.transform.position, transform.position);
                        var angle = Vector2.Angle(transform.up, i.transform.position - transform.position);
                        return distance + angle * AngleEffitience;
                    }).FirstOrDefault();

                    if (secondTarget != _Target)
                    {
                        var x = Vector2.Distance(secondTarget.transform.position, transform.position);
                        if (Vector2.Distance(secondTarget.transform.position, transform.position) < 1.1f)
                        {
                            if (Mathf.Abs(secondTarget.transform.position.x) > 1.3f ||
                                Mathf.Abs(secondTarget.transform.position.y) > 1.3f ||
                                Mathf.Abs(_Target.transform.position.x) < 1.2 ||
                                Mathf.Abs(_Target.transform.position.y) < 1.2)
                            {
                                direction = Mathf.Sign(Vector2.SignedAngle(transform.up, secondTarget.transform.position - transform.position));
                                yield return RotateAndAttack(secondTarget, Math.Sign(myRotateDirection * direction));
                            }
                            else
                            {
                                direction = Mathf.Sign(Vector2.SignedAngle(transform.up, _Target.transform.position - transform.position));
                                yield return RotateAndAttack(_Target, Math.Sign(myRotateDirection * direction));
                            }
                        }

                        direction = Mathf.Sign(Vector2.SignedAngle(transform.up, _Target.transform.position - transform.position));
                        yield return RotateAndAttack(_Target, Math.Sign(myRotateDirection * direction));
                    }
                    else
                    {
                        direction = Mathf.Sign(Vector2.SignedAngle(transform.up, _Target.transform.position - transform.position));
                        yield return RotateAndAttack(_Target, Math.Sign(myRotateDirection * direction));
                    }
                }
            }
        }

        IEnumerator RotateAndAttack(PlayerController target, int makeStep)
        {
            if (target == null)
            {
                yield break;
            }

            if (makeStep == -1)
            {
                PressButton.Pressed = true;
                yield return new WaitForSeconds(0.1f);
                PressButton.Pressed = false;
            }

            float angle;
            do
            {
                PressButton.Pressed = false;
                var targetVector = target.transform.position - transform.position;
                angle = Vector2.Angle(transform.up, targetVector);

                yield return null;
            } while (angle > 5f && target != null);

            if (target == null || target.OnTotem == false)
            {
                PressButton.Pressed = false;
                yield break;
            }

            PressButton.Pressed = true;

            var startTime = Time.time;
            while (Time.time - startTime < _DesigionDelay)
            {
                yield return null;

                if (target == null || target.OnTotem == false)

        {
                    PressButton.Pressed = false;
                    yield break;
                }
            }
            yield break;
        }


       public new void OnCollisionEnter2D(Collision2D coll)
       {
//           Debug.Log($"Collision {PlayerID} and {coll.transform.GetComponent<PlayerController>().PlayerID}");
           var vector = (coll.transform.position - transform.position).normalized;

           if (Vector2.Angle(transform.up, vector) < 90)
           {
               this.GetComponent<PlayerController>().UffSound.Play();
               coll.transform.GetComponent<Rigidbody2D>().AddForce(vector * CollisionForce, ForceMode2D.Impulse);
               PressButton.Pressed = false;
//               Debug.Log("stoped by collision");
           }

           coll.transform.GetComponentInChildren<ScaleSpring>().enabled = true;

       }
    }
}