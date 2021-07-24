using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exile
{
	public class TransparancyTween : TextTween
	{
        private TransparancyData data;
        private float startAlpha;
        public TransparancyTween(TransparancyData data, TextMeshProUGUI textGUI) : base(data, textGUI)
        {
            this.data = data;
            startAlpha = textGUI.alpha;
        }

        protected override void DoAnimation(float time)
        {
            textGUI.alpha = startAlpha * data.animationCurve.Evaluate(time);
        }
    }
}