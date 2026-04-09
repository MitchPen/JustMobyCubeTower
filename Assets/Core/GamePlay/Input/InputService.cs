using System;
using Core.Services.CameraProvider;
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
        private CompositeDisposable _disposable;
        private bool _firstTouch;

        public IObservable<Unit> PointerDown => _pointerDownEvent;

        public IObservable<Unit> PointerUp => _pointerUpEvent;

        public Vector2 GetPointerPosition() => UpdatePointerPosition();

        [Inject]
        public InputService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            Setup();
        }

        private void Setup()
        {
            UnityEngine.Input.multiTouchEnabled = false;
            _camera =  _cameraProvider.GetCamera();
            _pointerPos = Vector3.zero;
            _disposable =  new CompositeDisposable();
            
            Observable.EveryUpdate().Subscribe(_ =>
            {
                Tick();
            }).AddTo(_disposable);
        }

        private void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnPointerDown();
            if(UnityEngine.Input.GetMouseButtonUp(0))
                OnPointerUp();
        }

        private void OnPointerDown()
        {
            _firstTouch =  true;
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

        private Vector2 UpdatePointerPosition()
        {
            if(!_firstTouch) return  Vector2.zero;
            _pointerPos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            _pointerPos.z = 0;
            return  _pointerPos;
        }

        public void Dispose()
        {
            _pointerDownEvent?.Dispose();
            _pointerUpEvent?.Dispose();
            _disposable?.Dispose();
        }
    }
}