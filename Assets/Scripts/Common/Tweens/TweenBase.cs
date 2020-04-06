﻿using UnityEngine;

namespace Assets.Scripts.Common.Tweens
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    public abstract class TweenBase : MonoBehaviour
    {
        public float Speed;
        public float Period;
        public bool RandomPeriod;
	    public bool SaveState;

        protected float _time;

	    public void Start()
	    {
		    if (RandomPeriod)
		    {
			    Period = Random.Range(0, 360);
		    }
		}

        public virtual void OnEnable()
        {
	        if (!SaveState)
	        {
		        _time = 0;
			}
        }

        public void Update()
        {
            OnUpdate();
            _time += Time.deltaTime;
        }

        protected virtual void OnUpdate()
        {
        }

        protected float Sin()
        {
            return (Mathf.Sin(Speed * _time + Period * Mathf.Deg2Rad) + 1) / 2;
        }
    }
}