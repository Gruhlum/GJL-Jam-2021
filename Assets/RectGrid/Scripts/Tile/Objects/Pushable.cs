using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public abstract class Pushable : TileObject
	{
        public abstract void DoPush(TileUnit unit);
	}
}