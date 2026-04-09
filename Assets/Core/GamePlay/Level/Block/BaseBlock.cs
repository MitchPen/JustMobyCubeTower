using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Level.Block
{
    public class BaseBlock : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _animationtiming;

        private BlockType _blockType;
        private Tween _animationTween;
        private int _sortingOrder;

        public BlockType BlockType => _blockType;

        public void ChangeRaycastInteraction(bool value) => _collider.enabled = value;

        public void ChangeMaskInteraction(bool value) =>
            _spriteRenderer.maskInteraction =
                value ? SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.None;

        public void ChangeSortingOrder(bool makeVisible)
        {
            if (makeVisible)
                _spriteRenderer.sortingOrder += 1;
            else
                _spriteRenderer.sortingOrder = _sortingOrder;
        }

        public void ChangeVisibility(bool value) => gameObject.SetActive(value);

        private void Awake() => _sortingOrder = _spriteRenderer.sortingOrder;

        public void Setup(BlockType blockType, Sprite sprite)
        {
            _blockType = blockType;
            _spriteRenderer.sprite = sprite;
        }

        public async UniTask ShowAnimation(Action onComplete = null)
        {
            _spriteRenderer.transform.localScale = Vector2.zero;
            _animationTween?.Kill();
            _animationTween = _spriteRenderer.transform
                .DOScale(Vector2.one, _animationtiming)
                .SetEase(Ease.OutSine)
                .OnComplete(() => onComplete?.Invoke());
            await _animationTween.AsyncWaitForCompletion();
        }

        public async UniTask HideAnimation(Action onComplete = null)
        {
            _animationTween?.Kill();
            _animationTween = _spriteRenderer.transform
                .DOScale(Vector2.zero, _animationtiming).SetEase(Ease.OutSine)
                .OnComplete(() => onComplete?.Invoke());
            await _animationTween.AsyncWaitForCompletion();
        }

        private void OnDestroy() => _animationTween?.Kill();
    }
}