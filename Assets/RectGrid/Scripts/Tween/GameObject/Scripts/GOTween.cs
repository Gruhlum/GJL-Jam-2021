using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public abstract class GOTween : Tween
	{
		protected GameObject go;
		public GOTween(GOTweenData data, GameObject go) : base(data)
		{
			this.go = go;
		}
	}
}