using Core.GamePlay.Input;
using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Factory;
using Core.GamePlay.UI;
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
        [Inject] private InventoryView _inventoryView;
        [Inject] private IBlockFactory _blockFactory;
        
        [SerializeField] private DragComponent.DragComponent _dragComponent;
        [SerializeField] private Pit.Pit _pit;
        [SerializeField] private Transform _field;
        
        private CompositeDisposable _disposable;

        public void Launch()
        {
            _disposable = new CompositeDisposable();
            _input.PointerDown.Subscribe(_=>
            {                                                    
                OnPointerDown();
            }).AddTo(_disposable);
            _input.PointerUp.Subscribe(_ =>
            {
                OnPointerUp();
            }).AddTo(_disposable);
            
            _inventoryView.BlockPickedEvent.Subscribe(PickBlockFromInventory).AddTo(_disposable);
        }

        private void OnPointerUp()
        {
            Debug.Log("OnPointerUp");
            if (_dragComponent.HasBlock)
            {
                Debug.Log("Throw check");
                var block = _dragComponent.RemoveBlock();
                block.transform.SetParent(_field);
                var raycastResult = _raycastProvider.ThrowRay(_input.GetPointerPosition(), out GameObject hitResult);
                if (raycastResult)
                {
                    if (hitResult == _pit.gameObject && _pit.AvailableToThrow)
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

        private void PickBlockFromInventory(BlockType blockType)
        {
            var block = _blockFactory.CreateBlock(blockType,_input.GetPointerPosition());
            block.ChangeRaycastInteraction(false);
            block.ChangeVisibility(true);
            _pit.ChangeRaycastInteraction(true);
            _dragComponent.SetBlock(block);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}