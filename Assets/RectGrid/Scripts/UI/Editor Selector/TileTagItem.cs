using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    public class TileTagItem : SelectItem
    {
        [SerializeField] private GameObject marker = default;

        public override void LeftClick(RectGrid grid, TilePosition pos)
        {
            Tile t = grid.FindTile(pos);
            if (t == null)
            {
                return;
            }
            gridEditor.End = pos;

            marker.transform.position = t.transform.position;
        }
    }
}