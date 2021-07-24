using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exile
{
	public class TextColorTween : TextTween
	{
		private TextColorData data;
		private Color startColor;
		public TextColorTween(TextColorData data, TextMeshProUGUI textGUI) : base(data, textGUI)
        {
			this.data = data;
			startColor = textGUI.color;
        }

        protected override void DoAnimation(float time)
		{
			textGUI.color = Color.Lerp(startColor, data.color, data.animationCurve.Evaluate(time) - 1);
		}
	}
}