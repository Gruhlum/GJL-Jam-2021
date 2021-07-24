using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exile
{
    [CreateAssetMenu(menuName = "Tween/Text/Transparancy")]
    public class TransparancyData : TextTweenData
    {
        public override Tween Construct(TextMeshProUGUI text)
        {
            return new TransparancyTween(this, text);
        }
    }
}