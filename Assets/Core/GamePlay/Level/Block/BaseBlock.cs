using UnityEngine;

namespace Core.GamePlay.Level.Block
{
    public class BaseBlock : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private BlockType _blockType;
        
        public BlockType BlockType => _blockType;
        
        public void Setup(BlockType blockType, Sprite sprite)
        {
            _blockType = blockType;
            _spriteRenderer.sprite = sprite;
        }
    }
}