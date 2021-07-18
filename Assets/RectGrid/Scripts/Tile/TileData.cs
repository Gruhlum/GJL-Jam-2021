using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    [System.Serializable]
    public struct TileData
    {
        public TilePosition Position;
        public List<EffectData> EffectDatas;

        public TileData(Tile tile)
        {
            Position = new TilePosition(tile.X, tile.Y);
            EffectDatas = new List<EffectData>();
            EffectDatas.AddRange(tile.GetEffectTypes());
        }
    }
}