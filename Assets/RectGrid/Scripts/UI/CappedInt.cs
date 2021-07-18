using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	[System.Serializable]
    public class CappedInt : MyInt
    {
        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {               
                if (maxValue == value)
                {
                    return;
                }
                maxValue = value;
                MaxValueChanged?.Invoke(value);
            }
        }
        [SerializeField] private int maxValue = 3;

        public override int Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (value > MaxValue)
                {
                    value = MaxValue;
                }
                base.Value = value;
            }
        }
        public event Action<int> MaxValueChanged;

        public override string ToString()
        {
            return Value + " of " + MaxValue;
        }
    }
}