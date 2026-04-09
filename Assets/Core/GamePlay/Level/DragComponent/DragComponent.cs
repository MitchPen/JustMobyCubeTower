using System;
using Core.GamePlay.Input;
using Core.GamePlay.Level.Block;
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

        private ScreenBorderProviderData _borderData;
        private BaseBlock _currentBlock;
        private bool _hasBlock = false;
        private Subject<Unit> _outOfBounds = new Subject<Unit>();

        public bool HasBlock => _hasBlock;

        public IObservable<Unit> OutOfBounds => _outOfBounds;

        private void Start() => _borderData = _screenBorderProvider.GetScreenToWorldBorder();

        public void SetBlock(BaseBlock block)
        {
            if (_hasBlock) return;
            block.transform.SetParent(transform);
            _hasBlock = true;
            _currentBlock = block;
            _currentBlock.ChangeSortingOrder(true);
        }

        public BaseBlock RemoveBlock()
        {
            if (!_hasBlock) return null;
            _hasBlock = false;
            var blockToRemove = _currentBlock;
            _currentBlock.ChangeSortingOrder(false);
            _currentBlock = null;
            return blockToRemove;
        }

        private void LateUpdate()
        {
            if (!_hasBlock) return;
            var position = CheckForBounds(_input.GetPointerPosition());
            _currentBlock.transform.position = position;
        }

        private Vector2 CheckForBounds(Vector2 input)
        {
            if (input.x < _borderData.LeftBorder || input.x > _borderData.RightBorder)
                _outOfBounds.OnNext(Unit.Default);
            if (input.y < _borderData.BottomBorder || input.y > _borderData.TopBorder)
                _outOfBounds.OnNext(Unit.Default);

            var resultPosition = new Vector2(
                Mathf.Clamp(input.x, _borderData.LeftBorder, _borderData.RightBorder),
                Mathf.Clamp(input.y, _borderData.BottomBorder, _borderData.TopBorder));
            return resultPosition;
        }
    }
}