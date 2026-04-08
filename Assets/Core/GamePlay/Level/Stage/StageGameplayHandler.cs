using Core.GamePlay.Input;
using Core.GamePlay.Level.Block;
using Core.Services.GameObjectPool;
using Core.Services.RaycastProvider;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Stage
{
    public class StageGameplayHandler : MonoBehaviour
    {
        
        [Inject] private IInputService _input;
        [Inject] private IRaycastProvider _raycastProvider;
        [Inject] private IGameObjectPool _gameObjectPool;
        
        [SerializeField] private DragComponent.DragComponent _dragComponent;
        [SerializeField] private Pit.Pit _pit;
       
        
        private CompositeDisposable _disposable;

        public void Launch()
        {
            Debug.Log("Launching Stage Gameplay Controller");
            _disposable = new CompositeDisposable();
            _input.PointerDown.Subscribe(_=>
            {                                                    
                OnPointerDown();
            }).AddTo(_disposable);
            _input.PointerUp.Subscribe(_ =>
            {
                OnPointerUp();
            }).AddTo(_disposable);
        }

        private void OnPointerUp()
        {
            if (_dragComponent.HasBlock)
            {
                var block = _dragComponent.RemoveBlock();
                var raycastResult = _raycastProvider.ThrowRay(_input.GetPointerPosition(), out GameObject hitResult);
                if (raycastResult)
                {
                    if (hitResult == _pit.gameObject || _pit.AvailableToThrow)
                        _pit.DestroyBlock(block);
                }
                else
                {
                    block.ChangeRaycastInteraction(true);
                }
                
                _pit.ChangeRaycastInteraction(false);
            }
        }

        private void OnPointerDown()
        {
            if (_dragComponent.HasBlock == false)
            {
                var raycastResult = _raycastProvider.ThrowRay(_input.GetPointerPosition(), out GameObject hitResult);
                if (raycastResult)
                {
                    if (hitResult.TryGetComponent(out BaseBlock block))
                    {
                        _dragComponent.SetBlock(block);
                        block.ChangeRaycastInteraction(false);
                        _pit.ChangeRaycastInteraction(true);
                    }
                    
                }
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}