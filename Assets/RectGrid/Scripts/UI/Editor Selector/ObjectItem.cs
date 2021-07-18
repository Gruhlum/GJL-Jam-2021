using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class ObjectItem : SelectItem
    {
        [SerializeField] private Spawner<TileObject> objectSpawner = default;
        public override void LeftClick(RectGrid grid, TilePosition pos)
        {
            Tile tile = grid.FindTile(pos);
            if (tile == null || tile.Object != null)
            {
                return;
            }
            gridEditor.ObjectDatas.Add(new ObjectData(tile.GetTilePosition(), objectSpawner.Prefab));
            objectSpawner.Spawn().Setup(tile);
        }
    }
}