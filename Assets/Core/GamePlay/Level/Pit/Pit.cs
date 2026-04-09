using Core.GamePlay.Level.Block;
using Core.Services.GameObjectPool;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Pit
{
    public class Pit : MonoBehaviour
    {
        [Inject] IGameObjectPool _gameObjectPool;
        
        [SerializeField] private GameObject _mask;
        [SerializeField] private Transform _blockThrowStartPoint;
        [SerializeField] private Transform _blockThrowEndPoint;
        [SerializeField] private float _throwSpeed;
        [SerializeField] private Collider2D[] _colliders;
        
        private Sequence _throwSequence;

        public bool AvailableToThrow { get; private set; }

        private void Awake() => AvailableToThrow = true;

        public void DestroyBlock(BaseBlock block)
        {
            AvailableToThrow = false;
            block.transform.SetParent(transform);
            block.ChangeRaycastInteraction(false);
            _throwSequence?.Kill();
            _throwSequence = DOTween.Sequence();
            _throwSequence.Append(block.transform
                .DOMove(_blockThrowStartPoint.position, _throwSpeed)
                .OnComplete(() =>
                {
                    block.ChangeMaskInteraction(true);
                    _mask.SetActive(true);
                }));
            _throwSequence.Append(block.transform
                .DOMove(_blockThrowEndPoint.position, _throwSpeed)
                .OnComplete(() =>
                {
                    AvailableToThrow = true;
                    block.ChangeVisibility(false);
                    block.ChangeMaskInteraction(false);
                    _mask.SetActive(false);
                    _gameObjectPool.ReturnToPool(block);
                }));
        }

        public void ChangeRaycastInteraction(bool value)
        {
            foreach (var col in _colliders)
            {
                col.enabled = value;
            }
        }

        private void OnDestroy()
        {
            _throwSequence?.Kill();
            _throwSequence = null;
        }
    }
}