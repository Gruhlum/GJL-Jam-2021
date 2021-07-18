using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public class EffectItem : SelectItem
	{
        public EffectData EffectData
        {
            get
            {
                return effectData;
            }
            private set
            {
                effectData = value;
            }
        }
        [SerializeField] private EffectData effectData = default;

        public override void LeftClick(RectGrid grid, TilePosition pos)
        {
            Tile tile = grid.AddTile(pos);
            if (tile != null)
            {
                tile.AddEffect(EffectData.Create());
                tile.ActivateEffects = false;
            }
        }
    }
}