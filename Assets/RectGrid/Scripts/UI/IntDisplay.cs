using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Exile
{
	public class IntDisplay : MonoBehaviour
	{
        [SerializeField] private Image image = default;
        [SerializeField] private TextMeshProUGUI textGUI = default;
        [SerializeField] private TweenHandler greenFlare = default;
        [SerializeField] private TweenHandler redFlare = default;

        [SerializeField] private string suffix = default;

        private int oldValue;
        public void Setup(MyInt myInt)
        {
            oldValue = myInt.Value;
            image.sprite = myInt.Sprite;
            textGUI.text = myInt.Value.ToString() + suffix;
            myInt.ValueChanged += ValueChanged;
        }

        private void ValueChanged(int value)
        {
            if (value < oldValue)
            {
                redFlare.StartAnimation();
            }
            else if (value > oldValue)
            {
                greenFlare.StartAnimation();
            }
            oldValue = value;
            textGUI.text = value.ToString() + suffix;
        }
    }
}