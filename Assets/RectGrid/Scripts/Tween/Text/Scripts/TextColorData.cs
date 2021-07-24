using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Exile
{
    [CreateAssetMenu(menuName = "Tween/Text/Color")]
    public class TextColorData : TextTweenData
    {        
        public Color color = Color.white;

        public override Tween Construct(TextMeshProUGUI text)
        {
            return new TextColorTween(this, text);
        }
    }
}