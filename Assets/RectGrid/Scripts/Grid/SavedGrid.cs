using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class SavedGrid : ScriptableObject
    {
        public List<TileData> TileDatas = new List<TileData>();
        public List<ObjectData> ObjectDatas;
        public List<UnitData> UnitDatas;

        public TilePosition End;

        public virtual void SaveGridData(Tile[,] tiles, List<ObjectData> objDatas, List<UnitData> unitDatas, TilePosition end)
        {
            End = end;
            ObjectDatas = new List<ObjectData>();
            ObjectDatas.AddRange(objDatas);
            UnitDatas = new List<UnitData>();
            UnitDatas.AddRange(unitDatas);
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] != null)
                    {
                        TileDatas.Add(new TileData(tiles[x, y]));
                    }
                }
            }
        }
    }
}