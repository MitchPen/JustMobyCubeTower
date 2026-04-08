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
      private Sequence _throwSequence;
      
      public bool AvailableToThrow { get; private set; }

      public void DestroyBlock(BaseBlock block)
      {
         AvailableToThrow = false;
         block.ChangeRaycastInteraction(false);
         _throwSequence?.Kill();
         _throwSequence = DOTween.Sequence();
         _throwSequence.Append(block.transform.DOMove(_blockThrowStartPoint.position,_throwSpeed))
            .OnComplete(() =>
            {
               block.SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
               _mask.SetActive(true);
            });
         _throwSequence.Append(block.transform.DOMove(_blockThrowEndPoint.position, _throwSpeed))
            .OnComplete(() =>
            {
               AvailableToThrow = true;
               block.SpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
               block.gameObject.SetActive(false);
               block.ChangeRaycastInteraction(true);
               _mask.SetActive(false);
            });
      }

      private void OnDestroy()
      {
         _throwSequence?.Kill();
         _throwSequence = null;
      }
   }
}
