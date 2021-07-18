using HecTecGames.SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public abstract class Collectable : TileObject, IUnitEnter
    {    
        protected abstract void Collected(TileUnit unit);

        [SerializeField] private SoundClip collectSound = default;

        public virtual void UnitEnter(TileUnit unit)
        {
            collectSound.PlaySound();
            gameObject.SetActive(false);
            Collected(unit);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}