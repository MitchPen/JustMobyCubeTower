using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Level.Block
{
    public class BaseBlock : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider _collider;
        private BlockType _blockType;
        private Tween _animationTween;
        private Vector2 _localScale;
        
        public BlockType BlockType => _blockType;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public void ChangeRaycastInteraction(bool value)
        {
            _collider.enabled = value;
        }

        private void Awake()
        {
            _localScale =  transform.localScale;
        }
        
        public void Setup(BlockType blockType, Sprite sprite)
        {
            _blockType = blockType;
            _spriteRenderer.sprite = sprite;
        }

        public async UniTask ShowAnimation()
        {
            _animationTween?.Kill();
            _animationTween = transform.DOScale(_localScale, 1).SetEase(Ease.OutSine);
        }
        
        public async UniTask HideAnimation()
        {
            _animationTween?.Kill();
            _animationTween = transform.DOScale(Vector2.zero, 1).SetEase(Ease.OutSine);
        }
    }
}