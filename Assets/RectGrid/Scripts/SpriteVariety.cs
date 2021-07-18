using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteVariety : MonoBehaviour
	{
        [SerializeField] private List<Sprite> sprites = default;

        [SerializeField] private SpriteRenderer sr = default;

        private void Reset()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            sr.sprite = sprites.Random();
            float rng = Random.Range(0.55f, 1f);
            sr.color = new Color(rng, rng, rng);
        }
    }
}