using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class Bomb : Collectable
    {
        protected override void Collected(TileUnit unit)
        {
            if (unit is Player player)
            {
                player.PlayerData.Bombs.Value++;
            }
        }
    }
}