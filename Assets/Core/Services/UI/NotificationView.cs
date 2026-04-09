using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.Services.UI
{
    public class NotificationView : MonoBehaviour
    {
        [SerializeField] private float _hideTimer;
        [SerializeField] private float _animationDuration;
        [SerializeField] private TMPro.TextMeshProUGUI _text;
        [SerializeField] private float _bounceFactor;
        private bool _shown;
        private Sequence _animationSequence;
        private CancellationTokenSource _cts;

        public void ShowNotification(string message)
        {
            if (_shown)
            {
                if(string.Equals(message, _text.text)) return;
                Bounce(message).Forget();
            }
            else
            {
                _text.SetText(message);
                Show().Forget();
            }

            LaunchTimer().Forget();
            _shown = true;
        }

        private async UniTaskVoid Show()
        {
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(transform.DOScale(Vector3.one, _animationDuration).SetEase(Ease.InOutSine));
            await _animationSequence.AsyncWaitForCompletion();
            _animationSequence = null;
        }

        private void Hide()
        {
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(transform.DOScale(Vector3.zero, _animationDuration).SetEase(Ease.InOutSine));
            _shown = false;
        }

        private async UniTaskVoid Bounce(string newMessage)
        {
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(transform
                .DOScale(Vector2.one * _bounceFactor, _animationDuration / 2)
                .SetEase(Ease.InSine)
                .OnComplete(() => { _text.SetText(newMessage); }));
            _animationSequence.Append(transform.DOScale(Vector2.one, _animationDuration / 2).SetEase(Ease.OutSine));
            await _animationSequence.AsyncWaitForCompletion();
            _animationSequence = null;
        }

        private void OnDestroy()
        {
            _animationSequence?.Kill();
        }

        private async UniTaskVoid LaunchTimer()
        {
            DestroyToken();
            _cts = new CancellationTokenSource();
            var cansellationHandler = await UniTask
                .Delay(TimeSpan.FromSeconds(_hideTimer))
                .AttachExternalCancellation(_cts.Token)
                .SuppressCancellationThrow();
            if(cansellationHandler) return;
            Hide();
        }

        private void DestroyToken()
        {
            if (_cts == null || _cts.IsCancellationRequested) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts =  null;
        }
    }
}

