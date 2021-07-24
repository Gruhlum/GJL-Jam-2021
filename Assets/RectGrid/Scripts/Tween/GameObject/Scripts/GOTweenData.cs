using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public abstract class GOTweenData : TweenData
	{
		public abstract GOTween Construct(GameObject go);
	}
}