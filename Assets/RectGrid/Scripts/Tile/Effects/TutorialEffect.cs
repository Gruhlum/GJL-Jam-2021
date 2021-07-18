using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class TutorialEffect : TileEffect, IUnitEnter
    {
        public event Action<TileUnit> UnitEntered;
        public override EffectData GetData()
        {
            return null;
        }

        public void UnitEnter(TileUnit unit)
        {
            UnitEntered?.Invoke(unit);
        }
    }
}