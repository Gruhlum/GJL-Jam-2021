using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class EndGameEffect : TileEffect, IUnitEnter
    {
        GameController gc;
        public EndGameEffect(GameController gc)
        {
            this.gc = gc;
        }
        public override EffectData GetData()
        {
            return null;
        }

        public void UnitEnter(TileUnit unit)
        {
            gc.GoalReached(unit);
        }
    }
}