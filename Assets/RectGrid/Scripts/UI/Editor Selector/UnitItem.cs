using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class UnitItem : SelectItem
    {
        [SerializeField] private Spawner<TileUnit> objectSpawner = default;
        private void Reset()
        {
            gridEditor = FindObjectOfType<GridEditor>();
        }
        public override void LeftClick(RectGrid grid, TilePosition pos)
        {
            Tile tile = grid.FindTile(pos);
            if (tile == null || tile.Unit != null)
            {
                return;
            }
            gridEditor.UnitDatas.Add(new UnitData(tile.GetTilePosition(), objectSpawner.Prefab));
            objectSpawner.Spawn().Setup(tile);
        }
    }
}