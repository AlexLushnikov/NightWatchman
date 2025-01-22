using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        private const float _fadeDuration = 0.5f;

        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _counter;
        [SerializeField] private TMP_Text _mistakes;
        [SerializeField] private Image _progressSelector;
        [SerializeField] private GameObject _notAnomalyText;
        [SerializeField] private Image _target;
        [SerializeField] private Image _fade;

        public void SetDayText()
        {
            _description.text = "Look carefully at the rooms.\nWhen you are ready, enter the door behind.";
            _counter.text = string.Empty;
            _mistakes.text = string.Empty;
            SetProgress(0);
        }
        
        public void SetNightText()
        {
            _description.text = "Some strange anomalies have appeared, \ntry to find them all.";
        }

        public void SetAnomalyCount(int current, int total)
        {
            _counter.text = $"{current}/{total}";
        }

        public void SetMistakeCount(int mistakes)
        {
            _mistakes.text = $"{mistakes}";
        }

        public void SetProgress(float progress)
        {
            _progressSelector.fillAmount = progress;
        }

        public async UniTask EnableFade()
        {
            await _fade.DOFade(1f, _fadeDuration).ToUniTask();
        }

        public async UniTask DisableFade()
        {
            await _fade.DOFade(0f, _fadeDuration).ToUniTask();
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