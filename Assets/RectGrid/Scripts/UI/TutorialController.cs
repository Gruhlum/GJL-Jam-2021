using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exile
{
	public class TutorialController : MonoBehaviour
	{
        [SerializeField] private TextMeshProUGUI text = default;

        [SerializeField] private GridLoader gridLoader = default;
        [SerializeField] private RectGrid grid = default;

        [SerializeField] private List<TutorialData> datas = default;

        private void Awake()
        {
            gridLoader.GridLoaded += GridLoader_GridLoaded;
        }

        private void GridLoader_GridLoaded(GridLoadData obj)
        {
            if (obj.SavedGrid.name == "Tutorial")
            {
                foreach (var data in datas)
                {
                    TutorialEffect effect = new TutorialEffect();
                    effect.UnitEntered += Effect_UnitEntered;
                    grid.FindTile(data.pos).AddEffect(effect);
                }
            }
            else gameObject.SetActive(false);
        }

        private void Effect_UnitEntered(TileUnit obj)
        {
            text.text = datas.Find(x => x.pos == obj.Tile.GetTilePosition()).text;
        }

        private void OnDisable()
        {
            gridLoader.GridLoaded -= GridLoader_GridLoaded;
        }
    }
}