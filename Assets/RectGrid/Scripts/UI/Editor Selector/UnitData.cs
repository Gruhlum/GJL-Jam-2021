using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[System.Serializable]	
	public class UnitData
	{
		public TileUnit Prefab;
		public TilePosition Position;

		public UnitData(TilePosition pos, TileUnit unit)
		{
			Prefab = unit;
			Position = pos;
		}
	}
}