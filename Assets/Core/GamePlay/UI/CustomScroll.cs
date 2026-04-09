using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.GamePlay.UI
{
    public class CustomScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private RectTransform _background;

        [Header("Delay to check pick opportunity [ms]")] [SerializeField]
        private int _pickDelay;

        [Header("Shift to check pick opportunity [screen points]")] [SerializeField]
        private float _shiftThreshold;

        [SerializeField] private float _dragAcceleration;
        [SerializeField] private Image _pointerZone;

        private CancellationTokenSource _ctx;
        private PointerEventData _pointerDownData;
        private PointerEventData _dragData;
        private Vector2 _pointerDownPosition;
        private Vector2 _prevDragPosition;
        private Vector2 _scrollBorders;
        private Subject<PointerEventData> _onTryPickItem = new Subject<PointerEventData>();
        private bool _pointerDown;

        public IObservable<PointerEventData> OnTryPickItem => _onTryPickItem;

        public RectTransform Container => _container;
        public RectTransform Background => _background;

        public void ChangeScrollState(bool state) => _pointerZone.enabled = state;
        
        public void StopDrag() => DisableInteraction();

        public void SetWidth(float width)
        {
            var defaultXPos = _container.transform.localPosition.x;
            _scrollBorders = new Vector2(defaultXPos, -width);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDownData = eventData;
            _pointerDownPosition = eventData.position;
            _pointerDown = true;
            TryPickItem().Forget();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (!_pointerDown) return;
            _dragData = eventData;
            float dragShift;
            if (_prevDragPosition == Vector2.zero)
                dragShift = eventData.position.x - _pointerDownPosition.x;
            else
                dragShift = eventData.position.x - _prevDragPosition.x;
            _prevDragPosition = eventData.position;
            var newXPos = _container.transform.localPosition.x + dragShift * _dragAcceleration;
            newXPos = Mathf.Clamp(newXPos, -3335, _scrollBorders.x);
            _container.transform.localPosition = new Vector3(newXPos, _container.transform.localPosition.y, 0);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            DisableInteraction();
        }
        
        private void DisableInteraction()
        {
            DestroyToken();
            _pointerDown = false;
            _prevDragPosition = Vector2.zero;
            _pointerDownPosition = Vector2.zero;
            _pointerDownData = null;
            _dragData = null;
        }

        private async UniTaskVoid TryPickItem()
        {
            DestroyToken();
            _ctx = new CancellationTokenSource();
            var cancellationHandler = await UniTask
                .Delay(TimeSpan.FromMilliseconds(_pickDelay))
                .AttachExternalCancellation(_ctx.Token)
                .SuppressCancellationThrow();
            if (cancellationHandler) return;

            if (_dragData == null)
            {
                _onTryPickItem.OnNext(_pointerDownData);
                return;
            }

            var distance = Vector2.Distance(_pointerDownPosition, _dragData.position);
            
            if (distance <= _shiftThreshold)
                _onTryPickItem.OnNext(_dragData);
        }

        private void DestroyToken()
        {
            if (_ctx != null && !_ctx.IsCancellationRequested)
            {
                _ctx?.Cancel();
                _ctx?.Dispose();
                _ctx = null;
            }
        }
    }
}