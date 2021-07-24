using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public class TileObject : MonoBehaviour
	{       
        public virtual Tile Tile
        {
            get
            {
                return tile;
            }
            protected set
            {
                if (tile != null)
                {
                    tile.Object = null;
                }
                tile = value;
                if (tile != null)
                {
                    tile.Object = this;                    
                    sr.sortingOrder = Tile.GetSortingOrder();
                }
            }
        }
        private Tile tile;
        public bool Block = default;
        public bool Destructable = default;

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                return sr;
            }
            private set
            {
                sr = value;
            }
        }
        [SerializeField] protected SpriteRenderer sr = default;

        private void Reset()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public virtual void MoveTo(Tile tile, int speed = 5)
        {
            transform.position = Tile.transform.position;
            Tile = tile;
        }

        public virtual void Setup(Tile tile)
        {
            Tile = tile;
            transform.position = Tile.transform.position;
        }        
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        protected virtual void OnDisable()
        {
            Tile = null;
        }
    }
}