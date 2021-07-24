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
        [SerializeField] private TweenHandler tweenHandler = default;
        public void Setup(MyInt myInt)
        {
            image.sprite = myInt.Sprite;
            textGUI.text = myInt.Value.ToString();
            myInt.ValueChanged += ValueChanged;
        }

        private void ValueChanged(int value)
        {
            tweenHandler.StartAnimation();
            textGUI.text = value.ToString();
        }
    }
}