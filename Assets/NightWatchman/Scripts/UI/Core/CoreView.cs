using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NightWatchman
{
    public class CoreView : BaseView
    {
        private Color DefaultTargetColor = Color.white;
        private Color SelectTargetColor = new (1f, 0.42f, 0f);

        [SerializeField] private TMP_Text _counter;
        [SerializeField] private Image _progressSelector;
        [SerializeField] private GameObject _notAnomalyText;
        [SerializeField] private Image _target;

        public void SetData(int current, int total, float progress)
        {
            SetCount(current, total);
            SetProgress(progress);
        }

        public void SetCount(int current, int total)
        {
            _counter.text = $"{current}/{total}";
        }

        public void SetProgress(float progress)
        {
            _progressSelector.fillAmount = progress;
        }

        public void ActivateNotAnomalyText()
        {
            _notAnomalyText.SetActive(true);
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                _notAnomalyText.SetActive(false);
            });
        }

        public void ChangeTarget(bool selected)
        {
            _target.color = selected ? SelectTargetColor : DefaultTargetColor;
        }
    }
}