using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exile
{
    public abstract class TextTween : Tween
    {
        protected TextMeshProUGUI textGUI;
        public TextTween(TextTweenData data, TextMeshProUGUI textGUI) : base(data)
        {
            this.textGUI = textGUI;
        }
    }
}