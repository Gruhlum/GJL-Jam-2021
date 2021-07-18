using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exile
{
    public class RectGrid : MonoBehaviour
    {
        public Tile[,] Tiles = new Tile[6, 10];

        [SerializeField] private Spawner<Tile> tileSpawner = default;

        public event Action<Tile> TileDestroyed;
        public bool HasTile(int x, int y)
        {
            if (FindTile(x, y) == null)
            {
                return false;
            }
            return true;
        }
        public Tile FindTile(int x, int y)
        {
            if (x >= Tiles.GetLength(0) || y >= Tiles.GetLength(1) || x < 0 || y < 0)
            {
                return null;
            }
            else return Tiles[x, y];
        }
        public Tile FindTile(TilePosition pos)
        {
            return FindTile(pos.X, pos.Y);
        }
        public List<Tile> FindTiles(List<TilePosition> positions)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (var pos in positions)
            {
                Tile t = FindTile(pos);
                if (t != null)
                {
                    tiles.Add(t);
                }
            }
            return tiles;
        }

        public List<Tile> GetAllTiles()
        {
            List<Tile> tiles = new List<Tile>();
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {

                    if (Tiles[x, y] != null)
                    {
                        tiles.Add(Tiles[x, y]);                        
                    }
                }
            }
            return tiles;
        }

        public TilePosition WorldToTile(float x, float y)
        {
            Tile tile = tileSpawner.Prefab;
            return tile.WorldToTile(x, y);
        }
        public void RemoveAllTiles()
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (Tiles[x, y] != null)
                    {
                        Tiles[x, y].Destroy(true);
                    }                   
                }
            }
        }
        public void RemoveTile(TilePosition pos)
        {
            RemoveTile(pos.X, pos.Y);
        }
        public void RemoveTile(int x, int y)
        {
            if (x >= Tiles.GetLength(0) || y >= Tiles.GetLength(1) || x < 0 || y < 0)
            {
                return;
            }
            else if (Tiles[x, y] != null)
            {
                Tiles[x, y].Destroy();
                Tiles[x, y] = null;
            }
        }
        public Tile AddTile(TileData data)
        {
            Tile tile = AddTile(data.Position);
            foreach (var effectData in data.EffectDatas)
            {
                if (tile == null)
                {
                    Debug.Log("tile null");
                }
                if (effectData == null)
                {
                    Debug.Log("data null");
                }
                tile.AddEffect(effectData.Create());
            }
            return tile;
        }
        public Tile AddTile(TilePosition pos)
        {
            return AddTile(pos.X, pos.Y);
        }
        public Tile AddTile(int x, int y)
        {
            if (x >= Tiles.GetLength(0) || y >= Tiles.GetLength(1) || x < 0 || y < 0)
            {
                Debug.LogWarning("Out of Bounds");
                return null;
            }
            if (FindTile(x, y) != null)
            {
                Debug.LogWarning("Tile already exists at " + x + "/" + y);
                return null;
            }
            else
            {
                Tile tile = tileSpawner.Spawn();
                tile.Setup(x, y, this);
                Tiles[x, y] = tile;
                return tile;
            }
        }

        public void Tile_Destroyed(Tile tile)
        {
            if (tile == null)
            {
                return;
            }
            Tiles[tile.X, tile.Y] = null;
            TileDestroyed?.Invoke(tile);
            CheckNeighbours(tile);
        }
        private void CheckNeighbours(Tile tile)
        {
            List<TilePosition> positions = TilePosition.GetConnected(tile);
            foreach (var pos in positions)
            {
                Tile neighbour = FindTile(pos);
                if (neighbour != null && neighbour.IsConnectedToWall() == false)
                {
                    neighbour.Destroy();
                }
            }
        }

        public void LoadGrid(SavedGrid savedGrid)
        {
            if (savedGrid == null)
            {
                return;
            }
            //int width = savedGrid.TilesPositions.Max(t => t.X);
            //int height = savedGrid.TilesPositions.Max(t => t.Y);
            //Tiles = new Tile[width, height];

            foreach (var data in savedGrid.TileDatas)
            {
                AddTile(data);
            }
        }
    }
}