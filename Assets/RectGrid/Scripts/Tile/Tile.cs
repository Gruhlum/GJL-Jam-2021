using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Exile
{
    public class Tile : MonoBehaviour
    {
        public int X;
        public int Y;

        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        [SerializeField] private float width = 1;
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        [SerializeField] private float height = 1;
        public float Spacing
        {
            get
            {
                return spacing;
            }
            set
            {
                spacing = value;
            }
        }
        [SerializeField] private float spacing = default;

        public TileUnit Unit
        {
            get
            {
                return unit;
            }
            set
            {
                TileUnit lastUnit = unit;
                unit = value;
                if (ActivateEffects)
                {
                    foreach (var effect in effects)
                    {
                        if (lastUnit != null)
                        {
                            if (effect is IUnitExit exitEffect)
                            {
                                exitEffect.UnitExit(lastUnit);
                            }
                        }
                        if (unit != null)
                        {
                            if (effect is IUnitEnter enterEffect)
                            {
                                enterEffect.UnitEnter(unit);
                            }
                        }
                    }
                    if (tileObject != null)
                    {
                        if (tileObject is IUnitExit exitEffect)
                        {
                            exitEffect.UnitExit(unit);
                        }
                        if (tileObject is IUnitEnter enterEffect)
                        {
                            enterEffect.UnitEnter(unit);
                        }
                    }
                }
            }
        }
        [SerializeField] private TileUnit unit = default;

        public TileObject Object
        {
            get
            {
                return tileObject;
            }
            set
            {
                tileObject = value;
            }
        }
        [SerializeField] private TileObject tileObject = default;

        [SerializeField] private List<TileEffect> effects = new List<TileEffect>();

        public RectGrid Grid
        {
            get
            {
                return grid;
            }
            private set
            {
                grid = value;
            }
        }
        [SerializeField] private RectGrid grid = default;

        [SerializeField] private SpriteRenderer sr = default;

        public bool IsBlocked
        {
            get
            {
                if (Unit != null)
                {
                    return true;
                }
                if (Object != null && Object.Block)
                {
                    return true;
                }
                return false;
            }
        }

        public bool ActivateEffects
        {
            get
            {
                return activateEffects;
            }
            set
            {
                activateEffects = value;
            }
        }
        [SerializeField] private bool activateEffects = true;

        private void Reset()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public void Setup(int x, int y, RectGrid grid)
        {
            X = x;
            Y = y;
            Grid = grid;
            CalculatePosition();
            effects = new List<TileEffect>();
        }
        public void Setup(TileData data, RectGrid grid)
        {
            foreach (var effectData in data.EffectDatas)
            {
                TileEffect effect = effectData.Create();
                if (effect != null)
                {
                    AddEffect(effect);
                }
            }
            Setup(data.Position.X, data.Position.Y, grid);
        }

        //public void RemoveEffect()
        //{

        //}
        public void SetSprite(Sprite sprite)
        {
            sr.sprite = sprite;
        }

        public void AddEffect(TileEffect effect)
        {
            effect.Init(this);
            effects.Add(effect);
        }
        public void AddEffects(List<TileEffect> effects)
        {
            foreach (var effect in effects)
            {
                AddEffect(effect);
            }
        }

        [ContextMenu("Recalculate Position")]
        public int GetSortingLayer()
        {
            return -Y * 100 + X - 100;
        }
        private void CalculatePosition()
        {
            transform.position = new Vector2((Width + Spacing) * (X + Y), (Height + Spacing * Height) * (Y - X));
            sr.sortingOrder = GetSortingLayer();
        }

        public void Destroy(bool force = false)
        {
            if (force == true)
            {
                StartCoroutine(AnimateOut(true, delay: (X + Y) * 0.05f));
            }
            else StartCoroutine(AnimateOut(false));
        }

        private IEnumerator AnimateOut(bool quick, int speed = 2, float delay = 0)
        {
            if (Unit != null)
            {
                Unit.Die(quick);
            }
            if (Object != null)
            {
                Object.Destroy();
            }
            if (quick == false)
            {
                grid.Tile_Destroyed(this);
            }
            grid.Tiles[X, Y] = null;

            yield return new WaitForSeconds(delay);
            if (speed > 0)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
                {
                    sr.color -= new Color(0, 0, 0, Time.deltaTime * speed);
                    transform.position -= new Vector3(0, Time.deltaTime * speed * 2, 0);
                    yield return new WaitForEndOfFrame();
                }
            }
            gameObject.SetActive(false);
            sr.color = Color.white;
        }

        public List<EffectData> GetEffectTypes()
        {
            List<EffectData> types = new List<EffectData>();
            foreach (var effect in effects)
            {
                EffectData data = effect.GetData();
                if (data != null)
                {
                    types.Add(data);
                }

            }
            return types;
        }
        public TilePosition WorldToTile(float x, float y)
        {
            float posY = (x + (y / Height)) / 2f;
            float posX = x - posY;
            return new TilePosition(Mathf.RoundToInt(posX), Mathf.RoundToInt(posY));
        }
        public TilePosition GetTilePosition()
        {
            return new TilePosition(X, Y);
        }

        public List<Tile> GetAllConnectedTiles(List<Tile> tiles)
        {
            List<TilePosition> positions = TilePosition.GetConnected(this);
            foreach (var pos in positions)
            {
                Tile t = grid.FindTile(pos);
                if (t != null && tiles.Contains(t) == false)
                {
                    tiles.Add(t);
                    t.GetAllConnectedTiles(tiles);
                    if (tiles.Count >= 100)
                    {
                        Debug.Log("Count: " + tiles.Count);
                        return null;
                    }
                }
            }
            return tiles;
        }

        public bool HasNeighbour()
        {
            List<TilePosition> positions = TilePosition.GetAdjacent(this);
            foreach (var pos in positions)
            {
                if (grid.FindTile(pos) != null)
                {
                    return true;
                }
            }
            return false;
        }
        [ContextMenu("Check Connection")]
        public void Check()
        {
            for (int x = X - 1; X - x > 0; x--)
            {
                if (grid.HasTile(x, Y) == false)
                {
                    break;
                }
                else if (x == 0)
                    Debug.Log("true1");
            }
            for (int x = X + 1; X + x < grid.Tiles.GetLength(0); x++)
            {
                if (grid.HasTile(x, Y) == false)
                {
                    break;
                }
                else if (x == grid.Tiles.GetLength(0) - 1)
                    Debug.Log("true2");
            }
            for (int y = Y - 1; Y - y > 0; y--)
            {
                if (grid.HasTile(X, y) == false)
                {
                    break;
                }
                else if (y == 0)
                    Debug.Log("true3");
            }
            for (int y = Y + 1; Y + y < grid.Tiles.GetLength(1); y++)
            {
                if (grid.HasTile(y, Y) == false)
                {
                    break;
                }
                else if (y == grid.Tiles.GetLength(1) - 1)
                    Debug.Log("true4");
            }
            Debug.Log("false");

        }

        private bool IsBorderTile()
        {
            if (X == 0 || Y == 0 || X == grid.Tiles.GetLength(0) - 1 || Y == grid.Tiles.GetLength(1) - 1)
            {
                return true;
            }
            return false;
        }
        public bool IsConnectedToWall()
        {
            if (IsBorderTile())
            {
                return true;
            }
            List<Tile> tiles = GetAllConnectedTiles(new List<Tile>());
            if (tiles.Any(tile => tile.IsBorderTile()))
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return "Tile " + X + ", " + Y;
        }
    }
}

// 0, 0 = 0, 0
// 0, 1 = 0.5,  0.25
// 1, 0 = 0.5, -0.25
// 2, 0 = 1,   -0.5
// 1, 1 = 1,    0
// posX = width * (x - y)
// posY = (height / 2) * (y - x)