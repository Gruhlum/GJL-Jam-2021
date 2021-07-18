using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{

    public class HPEffect : TileEffect, IUnitExit
    {
        public int Life
        {
            get
            {
                return life;
            }
            set
            {
                life = value;
                if (life <= 0)
                {
                    tile.Grid.RemoveTile(tile.X, tile.Y);
                }
                else
                {
                    tile.SetSprite(data.Sprites[life - 1]);
                }
            }
        }
        [SerializeField] private int life = default;

        [SerializeField] private HPEffectData data = default;

        public HPEffect(HPEffectData data) : base()
        {
            this.data = data;
        }

        public void UnitExit(TileUnit unit)
        {
            if (unit is Player)
            {
                Life--;
            }
        }

        public override void Init(Tile tile)
        {
            base.Init(tile);
            Life = data.Life;
            tile.SetSprite(data.Sprites[life - 1]);
        }

        public override EffectData GetData()
        {
            return data;
        }
    }
}