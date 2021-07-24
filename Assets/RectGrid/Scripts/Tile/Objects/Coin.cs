using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class Coin : Collectable
    {
        protected override void Collected(TileUnit unit)
        {
            if (unit is Player player)
            {
                player.PlayerData.Coins.Value += 1;
            }
        }
    }
}