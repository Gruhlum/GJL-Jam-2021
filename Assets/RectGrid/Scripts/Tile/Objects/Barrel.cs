using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class Barrel : Pushable
    {
        [SerializeField] private Spawner<Explosion> explosionSpawner = default;

        public override void DoPush(TileUnit unit)
        {
            List<TilePosition> positions = TilePosition.GetLine(Tile.GetTilePosition(), unit.Tile.GetTilePosition(), 12);
            int lastPos;
            for (lastPos = 0; lastPos < positions.Count; lastPos++)
            {
                Tile tile = Tile.Grid.FindTile(positions[lastPos]);
                if (tile == null)
                {
                    break;
                }
                if (tile.Object != null && tile.Object.Block)
                {
                    break;
                }
                if (tile.Unit != null)
                {
                    break;
                }
            }

            if (lastPos - 1 <= 0)
            {
                return;
            }
            Tile target = Tile.Grid.FindTile(positions[lastPos - 1]);
            Tile = target;
            StartCoroutine(AnimateTo(transform.position, target.transform.position, 7f / (lastPos - 1f)));
            
        }
        private void Explode()
        {
            List<TilePosition> positions = TilePosition.GetConnected(Tile);
            foreach (var tile in Tile.Grid.FindTiles(positions))
            {
                if (tile.Object != null && tile.Object.Destructable == true)
                {
                    tile.Object.Destroy();
                }
                if (tile.Unit != null)
                {
                    tile.Unit.Health.Value -= 2;
                }
            }
            explosionSpawner.Spawn().Setup(Tile);
            Destroy();
        }
        protected IEnumerator AnimateTo(Vector3 start, Vector3 end, float speed = 5f)
        {
            GameController.blockingScripts.Add(this);

            for (float i = 0; i < 1; i += Time.deltaTime * speed)
            {
                transform.position = Vector3.Lerp(start, end, i);
                yield return new WaitForEndOfFrame();
            }
            transform.position = end;
            yield return new WaitForSeconds(0.2f);
            Explode();
            GameController.blockingScripts.Remove(this);
        }
    }
}