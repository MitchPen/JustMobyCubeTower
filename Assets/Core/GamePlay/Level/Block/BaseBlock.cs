using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Level.Block
{
    public class BaseBlock : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        private BlockType _blockType;
        private Tween _animationTween;
        private Vector2 _localScale;

        public BlockType BlockType => _blockType;
        
        public void ChangeRaycastInteraction(bool value) => _collider.enabled = value;

        public void ChangeMaskInteraction(bool value) =>
            _spriteRenderer.maskInteraction =
                value ? SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.None;

        public void ChangeVisibility(bool value) =>  gameObject.SetActive(value);

        private void Awake() =>  _localScale = transform.localScale;

        public void Setup(BlockType blockType, Sprite sprite)
        {
            _blockType = blockType;
            _spriteRenderer.sprite = sprite;
        }

        public async UniTask ShowAnimation()
        {
            _animationTween?.Kill();
            _animationTween = transform.DOScale(_localScale, 1).SetEase(Ease.OutSine);
            await _animationTween.AsyncWaitForCompletion();
        }

        public async UniTask HideAnimation()
        {
            _animationTween?.Kill();
            _animationTween = transform.DOScale(Vector2.zero, 1).SetEase(Ease.OutSine);
            await _animationTween.AsyncWaitForCompletion();
        }
    }
}