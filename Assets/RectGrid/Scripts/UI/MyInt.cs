using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[System.Serializable]
	public class MyInt
	{
        public virtual int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value == value)
                {
                    return;
                }
                this.value = value;
                ValueChanged?.Invoke(value);
            }
        }
        [SerializeField] private int value = 0;

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
        }
        [SerializeField] private Sprite sprite = default;

        public event Action<int> ValueChanged;
    }
}