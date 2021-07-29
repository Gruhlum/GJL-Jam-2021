using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exile
{
	public class GridEditor : MonoBehaviour
	{
        public SelectItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem != null)
                {
                    selectedItem.Highlight(false);
                }
                selectedItem = value;
                if (selectedItem != null)
                {
                    selectedItem.Highlight(true);
                }
            }
        }
        [SerializeField] private SelectItem selectedItem = default;

        [SerializeField] private RectGrid grid = default;
        public List<ObjectData> ObjectDatas = new List<ObjectData>();
        public List<UnitData> UnitDatas = new List<UnitData>();

        public TilePosition End;

        [SerializeField] private bool editTiles = default;

        public bool ValidLevel()
        {
            if (!UnitDatas.Any(x => x.Prefab is Player))
            {
                Debug.Log("Player is missing!");
                return false;
            }
            return true;
        }

        private void Update()
        {
            if (SelectedItem != null && Input.GetMouseButton(0))
            {
                SelectedItem.LeftClick(grid, GetTilePosition());
            }
            else if (Input.GetMouseButtonDown(1))
            {               
                Tile tile = grid.FindTile(GetTilePosition());
                if (tile == null)
                {
                    return;
                }
                if (tile.Object != null)
                {
                    ObjectDatas.Remove(ObjectDatas.Find(x => x.Position == tile.GetTilePosition()));
                    tile.Object.gameObject.SetActive(false);
                }
                else if (tile.Unit != null)
                {
                    UnitDatas.Remove(UnitDatas.Find(x => x.Position == tile.GetTilePosition()));
                    tile.Unit.gameObject.SetActive(false);
                }
                else if (editTiles)
                {
                    grid.RemoveTile(tile.GetTilePosition());
                }
            }
        }

        private TilePosition GetTilePosition()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return grid.WorldToTile(mousePos.x, mousePos.y);
        }
        public void ItemSelected(SelectItem item)
        {
            SelectedItem = item;
        }
	}
}