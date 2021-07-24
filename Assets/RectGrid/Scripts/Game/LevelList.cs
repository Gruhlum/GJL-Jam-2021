using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[CreateAssetMenu(menuName = "LevelList")]
	public class LevelList : ScriptableObject
	{
		public List<SavedGrid> items;

		public SavedGrid this [int index]
        {
			get
            {
				return items[index];
            }
        }
        public int Count
        {
            get
            {
                return items.Count;
            }
        }

    }
}