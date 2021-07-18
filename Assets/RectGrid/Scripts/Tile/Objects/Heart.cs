using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class Heart : Collectable
    {
        protected override void Collected(TileUnit unit)
        {
            unit.Health.Value++;
        }
    }
}