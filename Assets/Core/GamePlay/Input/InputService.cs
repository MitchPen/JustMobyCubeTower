using System;
using Core.Services.CameraProvider;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Input
{
    public class InputService : IInputService, IDisposable
    {
        private ICameraProvider _cameraProvider;
        private Camera _camera;
        private Subject<Unit> _pointerDownEvent = new Subject<Unit>();
        private Subject<Unit> _pointerUpEvent = new Subject<Unit>();
        private bool _isPointerDown;
        private Vector3 _pointerPos;
        private IDisposable _disposable;

        public IObservable<Unit> PointerDown => _pointerDownEvent;

        public IObservable<Unit> PointerUp => _pointerUpEvent;

        public Vector2 GetPointerPosition() => _pointerPos;

        [Inject]
        public InputService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            Setup();
        }

        private void Setup()
        {
            _camera =  _cameraProvider.GetCamera();
            _pointerPos = Vector3.zero;
            _disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                Tick();
            });
        }

        private void Tick()
        {
            CalcWorldPosition(UnityEngine.Input.mousePosition);
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnPointerDown();
                return;
            }
            if(UnityEngine.Input.GetMouseButtonUp(0))
                OnPointerUp();
        }

        private void OnPointerDown()
        {
            if (_isPointerDown) return;
           
            _isPointerDown = true;
            _pointerDownEvent?.OnNext(Unit.Default);
        }

        private void OnPointerUp()
        {
            if (!_isPointerDown) return;
           
            _isPointerDown = false;
            _pointerUpEvent?.OnNext(Unit.Default);
        }

        private void CalcWorldPosition(Vector2 position)
        {
            if(!_isPointerDown) return;
            _pointerPos = _camera.ScreenToWorldPoint(position);
            _pointerPos.z = 0;
        }

        public void Dispose()
        {
            _pointerDownEvent?.Dispose();
            _pointerUpEvent?.Dispose();
            _disposable?.Dispose();
        }
    }
}