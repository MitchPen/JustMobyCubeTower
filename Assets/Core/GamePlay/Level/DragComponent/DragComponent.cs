using System;
using Core.GamePlay.Input;
using Core.GamePlay.Level.Block;
using Core.Services.ScreenBorderProvider;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.DragComponent
{
    public class DragComponent : MonoBehaviour
    {
        public event Action OutOfBounds;
        
        [Inject] private IInputService _input;
        [Inject] private IScreenBorderProvider _screenBorderProvider;
        
        private ScreenBorderProviderData _borderData;
        private BaseBlock _currentBlock;
        private bool _hasBlock = false;
        
        public bool HasBlock => _hasBlock;
        

        private void Start() => _borderData = _screenBorderProvider.GetScreenToWorldBorder();
        
        public void SetBlock(BaseBlock block)
        {
            if (_hasBlock) return;
            block.transform.SetParent(transform);
            _hasBlock =  true;
            _currentBlock = block;
        }

        public BaseBlock GetBlock() => _currentBlock;
    
        public BaseBlock RemoveBlock()
        {
            if (!_hasBlock) return null;
            _hasBlock = false;
            var blockToRemove = _currentBlock;
            _currentBlock = null;
            return blockToRemove;
        }

        private void LateUpdate()
        {
            if(!_hasBlock) return;
            var position = CheckForBounds(_input.GetPointerPosition());
            _currentBlock.transform.position = _input.GetPointerPosition();
        }

        private Vector2 CheckForBounds(Vector2 input)
        {
            if(input.x < _borderData.LeftBorder || input.x > _borderData.RightBorder)
                OutOfBounds?.Invoke(); 
            if(input.y < _borderData.BottomBorder || input.y > _borderData.TopBorder)
                OutOfBounds?.Invoke();

            var resultPosition = new Vector2(
                Mathf.Clamp(input.x, _borderData.LeftBorder, _borderData.RightBorder),
                Mathf.Clamp(input.y, _borderData.BottomBorder, _borderData.TopBorder));
            return resultPosition;
        }
    }
}
