using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	
	[System.Serializable]

	public abstract class TileEffect
	{
		protected Tile tile;

		public TileEffect()
        {
        }

		public virtual void Init(Tile tile)
        {
			this.tile = tile;
		}
		public abstract EffectData GetData();
	}
}