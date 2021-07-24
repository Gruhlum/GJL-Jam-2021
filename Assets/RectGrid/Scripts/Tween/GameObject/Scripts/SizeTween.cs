using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class SizeTween : GOTween
    {
        private SizeData data;
        private Vector3 startSize;
        public SizeTween(SizeData data, GameObject go) : base(data, go)
        {
            this.data = data;
            startSize = go.transform.localScale;
        }
        protected override void DoAnimation(float time)
        {
            go.transform.localScale = startSize * data.animationCurve.Evaluate(time);
        }
    }
}