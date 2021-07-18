
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exile
{
	[RequireComponent(typeof(Button))]
	public abstract class SelectItem : MonoBehaviour
	{
		public abstract void LeftClick(RectGrid grid, TilePosition pos);

        [SerializeField] protected GridEditor gridEditor;
        [SerializeField] protected Button btn;
        [SerializeField] private Image image = default;

        [SerializeField] private Color defaultCol = default;
        [SerializeField] private Color highlightColor = default;

        private void Reset()
        {
            btn = GetComponent<Button>();
            image = GetComponent<Image>();
        }
        private void Awake()
        {
            btn.onClick.AddListener(() => Selected());
        }
        private void Selected()
        {
            gridEditor.ItemSelected(this);
        }
        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                image.color = highlightColor;
            }
            else image.color = defaultCol;
        }
    }
}