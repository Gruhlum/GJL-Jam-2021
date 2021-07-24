using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public abstract class TweenData : ScriptableObject
    {
        public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 1) });
        public bool Loop
        {
            get
            {
                return loop;
            }
            set
            {
                loop = value;
            }
        }
        [SerializeField] private bool loop = default;
    }
}