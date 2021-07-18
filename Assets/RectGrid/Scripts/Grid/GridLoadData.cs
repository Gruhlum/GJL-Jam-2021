using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public struct GridLoadData
	{
		public SavedGrid SavedGrid;
        public List<TileUnit> TileUnits;
		public List<TileObject> TileObjects;
		public Tile End;
		public Player Player;

        public GridLoadData(SavedGrid savedGrid, List<TileUnit> tileUnits, List<TileObject> tileObjects, Tile end, Player player)
        {
            SavedGrid = savedGrid;
            TileUnits = tileUnits;
            TileObjects = tileObjects;
            End = end;
            Player = player;
        }
    }
}