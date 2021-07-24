using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Exile
{
    public class TweenHandler : MonoBehaviour
    {
        public bool StartOnEnabled
        {
            get
            {
                return startOnEnabled;
            }
            set
            {
                startOnEnabled = value;
            }
        }
        [SerializeField] private bool startOnEnabled = true;

        public TextMeshProUGUI TextGUI
        {
            get
            {
                return textGUI;
            }
            private set
            {
                textGUI = value;
            }
        }
        [SerializeField] private TextMeshProUGUI textGUI;
        [SerializeField] private List<TweenData> tweenDatas = default;
        private List<Tween> tweens = new List<Tween>();

        private float timeElapsed = 0;
        private float maxTime;

        private bool isEnabled = false;

        private void Reset()
        {
            textGUI = gameObject.GetComponent<TextMeshProUGUI>();
        }

        private void Awake()
        {
            foreach (var data in tweenDatas)
            {
                if (data is TextTweenData textData)
                {
                    tweens.Add(textData.Construct(textGUI));
                }
                if (data is GOTweenData goData)
                {
                    tweens.Add(goData.Construct(gameObject));
                }
            }
        }

        private void OnEnable()
        {
            if (StartOnEnabled)
            {
                StartAnimation();
            }
        }
        private void Update()
        {
            if (!isEnabled)
            {
                return;
            }
            timeElapsed += Time.deltaTime;

            if (maxTime > 0 && timeElapsed > maxTime)
            {
                isEnabled = false;
                return;
            }
            foreach (var tween in tweens)
            {
                tween.Evaluate(timeElapsed);
            }
        }
        public void StartAnimation()
        {
            if (tweens.Any(x => x.Loop == true))
            {
                maxTime = -1;
            }
            else maxTime = tweens.Max(x => x.Duration);
            timeElapsed = 0;
            isEnabled = true;
        }
    }
}