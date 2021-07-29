using HecTecGames.SoundSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exile
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites = default;
        [SerializeField] private List<SpriteRenderer> renderers = default;

        public float Duration = 2;
        private float remaining;

        private int count;

        [SerializeField] private SoundClip explosionSound = default;

        private void Reset()
        {
            renderers.AddRange(GetComponentsInChildren<SpriteRenderer>().ToList());
        }
        public int GetSortingLayer(Tile tile, int x, int y)
        {
            return -(y + tile.Y) * 100 + (x + tile.X) - 100;
        }

        public void Setup(Tile tile)
        {
            transform.position = tile.transform.position;
            remaining = Duration;
            explosionSound.PlaySound();
            renderers[0].sortingOrder = GetSortingLayer(tile, 0, 1);
            renderers[1].sortingOrder = GetSortingLayer(tile, 0, -1);
            renderers[2].sortingOrder = GetSortingLayer(tile, 1, 0);
            renderers[3].sortingOrder = GetSortingLayer(tile, -1, 0);
            if (renderers.Count >= 5)
            {
                renderers[4].sortingOrder = GetSortingLayer(tile, 0, 0);
            }

            foreach (var renderer in renderers)
            {
                renderer.color = Color.white;
            }
        }

        private void FixedUpdate()
        {
            remaining -= Time.deltaTime;
            if (remaining <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            if (remaining <= Duration / 2f)
            {
                foreach (var renderer in renderers)
                {
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, remaining / (Duration / 2f));
                }
            }


            count++;
            if (count >= 10)
            {
                count = 0;
                foreach (var renderer in renderers)
                {
                    renderer.sprite = sprites.Random();
                }
            }
        }
    }
}