using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Exile
{
    [CreateAssetMenu(menuName = "Tween/GO/Size")]
    public class SizeData : GOTweenData
    {
        public override GOTween Construct(GameObject go)
        {
            return new SizeTween(this, go);
        }
    }
}