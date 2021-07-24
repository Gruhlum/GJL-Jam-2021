using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exile
{
    public abstract class TextTweenData : TweenData
    {
        public abstract Tween Construct(TextMeshProUGUI text);
    }
}