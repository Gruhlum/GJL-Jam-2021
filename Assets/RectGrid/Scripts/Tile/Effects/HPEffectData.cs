using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[CreateAssetMenu(menuName = "RectGrid/HPData")]
	public class HPEffectData : EffectData
	{
		public int Life;
		public List<Sprite> Sprites;

        public override TileEffect Create()
        {
            return new HPEffect(this);
        }
    }
}