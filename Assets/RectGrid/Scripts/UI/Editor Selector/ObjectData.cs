using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[System.Serializable]
    public class ObjectData
	{
		public TileObject Prefab;
        public TilePosition Position;

		public ObjectData(TilePosition pos, TileObject obj)
        {
            Prefab = obj;
            Position = pos;
        }
    }
}