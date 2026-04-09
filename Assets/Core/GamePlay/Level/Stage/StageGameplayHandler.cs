using Core.GamePlay.Input;
using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Factory;
using Core.GamePlay.Level.SetupProvider;
using Core.GamePlay.Level.Tower;
using Core.GamePlay.UI;
using Core.Services.GameObjectPool;
using Core.Services.LocalizationService.Data;
using Core.Services.RaycastProvider;
using Core.Services.ScreenBorderProvider;
using Core.Services.UI;
using Cysharp.Threading.Tasks;
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
        [Inject] private ILevelSetupProvider _levelSetupProvider;
        [Inject] private IScreenBorderProvider _screenBorderProvider;
        [Inject] private INotificationProvider _notificationProvider;

        [SerializeField] private DragComponent.DragComponent _dragComponent;
        [SerializeField] private Pit.Pit _pit;
        [SerializeField] private Transform _field;
        [SerializeField] private BlockTower _tower;

        private StageConditionChecker _stageConditionChecker;
        private CompositeDisposable _disposable;

        public void Launch()
        {
            _disposable = new CompositeDisposable();
            _stageConditionChecker = new StageConditionChecker();
            _stageConditionChecker.InitializeLevelConditions(_levelSetupProvider.GetLevelSetup().LevelConditions);
            _input.PointerDown.Subscribe(_ => { OnPointerDown(); }).AddTo(_disposable);
            _input.PointerUp.Subscribe(_ => { OnPointerUp(); }).AddTo(_disposable);

            _inventoryView.BlockPickedEvent.Subscribe(PickBlockFromInventory).AddTo(_disposable);
            _dragComponent.OutOfBounds.Subscribe(_ =>
            {
                _notificationProvider.ShowNotification(LocalizationPhraseKey.OUT_OF_BOUNDS);
            }).AddTo(_disposable);
        }

        private void OnPointerUp()
        {
            if (!_dragComponent.HasBlock) return;

            var releasePosition = _input.GetPointerPosition();
            var block = _dragComponent.RemoveBlock();
            block.transform.SetParent(_field);
            if (releasePosition.x > 1f)
                ThrowBlockOnRightSide(block, releasePosition);
            else if (releasePosition.x < -1f)
                ThrowBlockOnLeftSide(block, releasePosition);
            else
                RemoveBlockToPool(block);
            
            _pit.ChangeRaycastInteraction(false);
        }

        private void ThrowBlockOnRightSide(BaseBlock block, Vector3 releasePosition)
        {
            if (_tower.BlockTowerData.BlockCount == 0)
            {
                if (releasePosition.y >= _tower.transform.position.y - block.transform.localScale.y)
                {
                    _tower.AddBlock(block);
                    _notificationProvider.ShowNotification(LocalizationPhraseKey.CUBE_INSTALLED);
                }
                else
                    RemoveBlockToPool(block);
            }
            else
            {
                var lastBlock = _tower.BlockTowerData.GetLastBlock();
                if (CheckVerticalCondition(releasePosition, lastBlock.transform)
                    && CheckHorizontalCondition(releasePosition, lastBlock.transform)
                    && CheckCondition(block))
                {
                    _notificationProvider.ShowNotification(LocalizationPhraseKey.CUBE_INSTALLED);
                    _tower.AddBlock(block);
                }
                else
                    RemoveBlockToPool(block);
            }
        }

        private void RemoveBlockToPool(BaseBlock block)
        {
            block.HideAnimation(() => _gameObjectPool.ReturnToPool(block)).Forget();
            _notificationProvider.ShowNotification(LocalizationPhraseKey.CUBE_REMOVED);
        }

        private bool CheckVerticalCondition(Vector2 releasePosition, Transform lastBlockTransform)
        {
            var nextBlockYPos = releasePosition.y + lastBlockTransform.lossyScale.y / 2;
            var addRestrict =  releasePosition.y < lastBlockTransform.position.y ||
                               nextBlockYPos > _screenBorderProvider.GetScreenToWorldBorder().TopBorder;
            return !addRestrict;
        }

        private bool CheckHorizontalCondition(Vector2 releasePosition, Transform lastBlock)
        {
            var lastBlockHorizontalPosition = new Vector2(lastBlock.position.x, 0);
            var pointerHorizontalPosition = new Vector2(releasePosition.x, 0);
            var shiftThreshold = lastBlock.localScale.x / 1.5f;
            var horizontalDistance = Vector2.Distance(lastBlockHorizontalPosition, pointerHorizontalPosition);
            return horizontalDistance <= shiftThreshold;
        }

        private bool CheckCondition(BaseBlock block)
        {
            return _stageConditionChecker.CheckForCondition(block, _tower.BlockTowerData);
        }

        private void ThrowBlockOnLeftSide(BaseBlock block, Vector2 releasePosition)
        {
            var raycastResult = _raycastProvider.ThrowRay(releasePosition, out GameObject hitResult);
            if (raycastResult && hitResult == _pit.gameObject && _pit.AvailableToThrow)
            {
                _pit.DestroyBlock(block);
                _notificationProvider.ShowNotification(LocalizationPhraseKey.THROW_CUBE_TO_PIT);
            }
            else
                RemoveBlockToPool(block);
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
                        _tower.RemoveBlock(block);
                        _pit.ChangeRaycastInteraction(true);
                    }
                }
            }
        }

        private void PickBlockFromInventory(BlockType blockType)
        {
            var block = _blockFactory.CreateBlock(blockType, _input.GetPointerPosition());
            block.ChangeRaycastInteraction(false);
            block.ShowAnimation().Forget();
            block.ChangeVisibility(true);
            _pit.ChangeRaycastInteraction(true);
            _dragComponent.SetBlock(block);
        }

        private void OnDestroy() => _disposable?.Dispose();
    }
}