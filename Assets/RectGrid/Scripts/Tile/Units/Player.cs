using HecTecGames.SoundSystem;
using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class Player : TileUnit
    {
        public PlayerData PlayerData
        {
            get
            {
                return playerData;
            }
            private set
            {
                playerData = value;
            }
        }
        [SerializeField] private PlayerData playerData = default;

        private RectGrid grid = default;

        public Sprite[] sprites;

        [SerializeField] private Spawner<Explosion> explosionSpawner = default;
        [SerializeField] private SoundClip moveSound = default;

        private void Awake()
        {
            PlayerData.ResetData();
            Health.ValueChanged += Health_ValueChanged;
        }

        private void SetSprite(Tile start, Tile target)
        {
            if (start == null || target == null)
            {
                return;
            }
            TilePosition? pos = TilePosition.GetDirection(start.GetTilePosition(), target.GetTilePosition());
            if (pos != null && pos.HasValue)
            {
                if (pos.Value.X == -1)
                {
                    sr.sprite = sprites[0];
                }
                else if (pos.Value.Y == 1)
                {
                    sr.sprite = sprites[1];
                }
                else sr.sprite = sprites[2];
            }
        }

        protected override void Health_ValueChanged(int hp)
        {
            PlayerData.Health.Value = hp;
            base.Health_ValueChanged(hp);
        }

        public override void MoveTo(Tile tile)
        {
            moveSound.PlaySound();
            SetSprite(Tile, tile);
            base.MoveTo(tile);
        }

        private void Update()
        {
            if (GameController.AllowInput == false || Tile == null)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveTo(grid.FindTile(Tile.X, Tile.Y + 1));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveTo(grid.FindTile(Tile.X, Tile.Y - 1));
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveTo(grid.FindTile(Tile.X - 1, Tile.Y));
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveTo(grid.FindTile(Tile.X + 1, Tile.Y));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                UseBomb();
            }
        }
        private void UseBomb()
        {
            if (PlayerData.Bombs.Value <= 0)
            {
                return;
            }
            explosionSpawner.Spawn().Setup(Tile);
            List<TilePosition> tilePos = TilePosition.GetConnected(Tile);
            PlayerData.Bombs.Value--;
            foreach (var pos in tilePos)
            {
                Tile tile = grid.FindTile(pos);
                if (tile != null)
                {
                    if (tile.Unit != null)
                    {
                        tile.Unit.Health.Value--;
                    }
                    if (tile.Object != null)
                    {
                        if (tile.Object.Destructable)
                        {
                            tile.Object.Destroy();
                        }
                    }
                }
            }
        }

        public override void Setup(Tile tile)
        {
            base.Setup(tile);
            grid = tile.Grid;
            if (PlayerData == null)
            {
                PlayerData = new PlayerData();
            }
            else Health.Value = 3;
        }

        public override void Die(bool force)
        {
            base.Die(force);
            Tile = null;
        }
    }
}