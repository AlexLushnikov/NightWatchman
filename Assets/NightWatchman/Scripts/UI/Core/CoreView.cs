using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NightWatchman
{
    public class CoreView : BaseView
    {
        [SerializeField] private TMP_Text _counter;
        [SerializeField] private Image _progressSelector;
        [SerializeField] private GameObject _notAnomalyText;

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
    }
}