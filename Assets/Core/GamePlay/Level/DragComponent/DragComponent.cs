using Core.GamePlay.Input;
using Core.Services.RaycastProvider;
using Core.Services.ScreenBorderProvider;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.DragComponent
{
    public class DragComponent : MonoBehaviour
    {
        [Inject] private IInputService _input;
        [Inject] private IScreenBorderProvider _screenBorderProvider;
        [Inject] private IRaycastProvider _raycastProvider;

        [SerializeField] private Transform _dragObject;

        private CompositeDisposable _disposables;

        private void Start()
        {
            _disposables = new CompositeDisposable();
            _input.PointerDown.Subscribe(_=>
            {                                                    
                OnPointerDown();
            }).AddTo(_disposables);
            _input.PointerUp.Subscribe(_ =>
            {
                OnPointerUp();
            }).AddTo(_disposables);
        }

        private void OnPointerUp()
        {
            Debug.Log("OnPointerUp CAPTURE");
            var castResult = _raycastProvider.ThrowRay(_input.GetPointerPosition(),out GameObject resultObject);
            Debug.Log($"cast result is {castResult}");
            if(resultObject != null)
                Debug.Log($"name of object = {resultObject}");
        }

        private void OnPointerDown()
        {
            Debug.Log("OnPointerDOWN CAPTURE");
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}
