using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public abstract class EffectData : ScriptableObject
	{
		public abstract TileEffect Create();
	}
}