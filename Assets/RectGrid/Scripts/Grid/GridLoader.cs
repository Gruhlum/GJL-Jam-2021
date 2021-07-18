using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class GridLoader : MonoBehaviour
    {
        [SerializeField] private SavedGrid loadOnStartup = default;
        [SerializeField] private RectGrid grid = default;

        [SerializeField] private bool enableTileEffects = default;

        public event Action<GridLoadData> GridLoaded;

        private Player player;

        private void Start()
        {
            if (loadOnStartup != null)
            {
                LoadGrid(loadOnStartup);
            }
        }
        public void LoadGrid(SavedGrid savedData)
        {
            if (savedData == null)
            {
                return;
            }

            grid.LoadGrid(savedData);

            GridLoadData data = new GridLoadData();
            data.SavedGrid = savedData;
            data.End = grid.FindTile(savedData.End);

            if (enableTileEffects)
            {
                for (int x = 0; x < grid.Tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.Tiles.GetLength(1); y++)
                    {
                        if (grid.Tiles[x, y] != null)
                        {
                            grid.Tiles[x, y].ActivateEffects = true;
                        }
                    }
                }
            }

            data.TileObjects = new List<TileObject>();
            foreach (var objData in savedData.ObjectDatas)
            {
                TileObject obj = Instantiate(objData.Prefab);
                obj.Setup(grid.FindTile(objData.Position));            
                data.TileObjects.Add(obj);
            }

            data.TileUnits = new List<TileUnit>();
            foreach (var unitData in savedData.UnitDatas)
            {
                if (unitData.Prefab is Player _player)
                {
                    if (player == null)
                    {
                        player = Instantiate(_player);
                    }
                    player.Setup(grid.FindTile(unitData.Position));
                    data.Player = player;
                }
                else
                {
                    TileUnit unit = Instantiate(unitData.Prefab);
                    unit.Setup(grid.FindTile(unitData.Position));
                    data.TileUnits.Add(unit);
                }
            }
            GridLoaded?.Invoke(data);
        }
    }
}