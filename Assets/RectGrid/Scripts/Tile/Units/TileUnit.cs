using HecTecGames.SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class TileUnit : MonoBehaviour
    {
        public Tile Tile
        {
            get
            {
                return tile;
            }
            protected set
            {
                if (tile != null)
                {
                    tile.Unit = null;
                }
                tile = value;
                if (tile != null)
                {
                    tile.Unit = this;
                    sr.sortingOrder = Tile.GetSortingLayer();
                }
            }
        }
        [SerializeField] private Tile tile;

        public virtual CappedInt Health
        {
            get
            {
                return health;
            }
            protected set
            {
                health = value;
            }
        }
        [SerializeField] private CappedInt health;

        public int ContactDamage
        {
            get
            {
                return contactDamage;
            }
            set
            {
                contactDamage = value;
            }
        }
        [SerializeField] private int contactDamage = 1;

        [SerializeField] protected SpriteRenderer sr = default;

        [SerializeField] private SoundClip deathSound = default;

        public event Action<TileUnit> UnitDied;


        private void Awake()
        {
            Health.ValueChanged += Health_ValueChanged;
        }

        protected virtual void Health_ValueChanged(int health)
        {
            if (health <= 0)
            {
                Die();
            }
        }

        public virtual void Setup(Tile tile)
        {
            gameObject.SetActive(true);
            Tile = tile;
            transform.position = tile.transform.position;
        }

        public virtual void MoveTo(Tile tile)
        {
            if (tile == null)
            {
                return;
            }
            if (tile.Object != null )
            {
                if (tile.Object.Block)
                {
                    return;
                }
                if (tile.Object is Pushable pushable)
                {
                    pushable.DoPush(this);
                    return;
                }
            }
            if (tile.Unit != null)
            {
                Battle(tile.Unit);
            }
            else
            {
                Tile = tile;
                StartCoroutine(AnimateTo(transform.position, Tile.transform.position));
                //transform.position = tile.transform.position;
            }
        }

        public void Battle(TileUnit unit)
        {
            Health.Value -= unit.ContactDamage;
            unit.Health.Value -= ContactDamage;
        }

        public virtual void Die(bool force = false)
        {
            if (!force)
            {
                UnitDied?.Invoke(this);
                if (deathSound != null) deathSound.PlaySound();
            }
            StartCoroutine(FadeOut());
        }

        protected virtual void OnDisable()
        {
            Tile = null;
        }

        private IEnumerator AnimateTo(Vector3 start, Vector3 end)
        {
            GameController.blockingScripts.Add(this);

            for (float i = 0; i < 1; i += Time.deltaTime * 8)
            {
                transform.position = Vector3.Lerp(start, end, i);
                yield return new WaitForEndOfFrame();
            }
            transform.position = end;
            GameController.blockingScripts.Remove(this);
        }
        private IEnumerator FadeOut(int speed = 4)
        {           
            if (speed == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GameController.blockingScripts.Add(this);
                for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
                {
                    sr.color -= new Color(0, 0, 0, Time.deltaTime * speed);
                    yield return new WaitForEndOfFrame();
                }

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                GameController.blockingScripts.Remove(this);
                gameObject.SetActive(false);
            }            
        }
    }
}