using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public abstract class Tween
	{
        public bool Loop
        {
            get
            {
                return data.Loop;
            }           
        }

        public float Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        private float duration;

        TweenData data;

        public Tween(TweenData data)
        {
            this.data = data;
            if (data.animationCurve.length == 0)
            {
                Duration = 0;
            }
            Duration = data.animationCurve.keys[data.animationCurve.length - 1].time;
        }

        public void Evaluate(float time)
        {
            if (Loop == false && time > Duration)
            {
                return;
            }
            else DoAnimation(time % Duration);
        }
        protected abstract void DoAnimation(float time);
    }
}