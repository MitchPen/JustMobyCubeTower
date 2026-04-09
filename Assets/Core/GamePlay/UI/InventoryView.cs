using System;
using System.Collections.Generic;
using Core.GamePlay.Input;
using Core.GamePlay.Level.Block;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Core.GamePlay.UI
{
    public class InventoryView : MonoBehaviour
    {
        [Inject] private InventoryItemFactory _factory;
        [Inject] private IInputService _input;

        [SerializeField] private CustomScroll _scroll;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private RectTransform _rectTransform;

        private List<InventoryViewItem> _items;
        private CompositeDisposable _disposables = new CompositeDisposable();

        private Subject<BlockType> _blockPicked = new Subject<BlockType>();
        public IObservable<BlockType> BlockPickedEvent => _blockPicked;

        public Vector2 GetCanvasResolution() => new Vector2(_rectTransform.rect.width, _rectTransform.rect.height);

        public Vector2 GetInventoryScreenSize() =>
            new Vector2(_scroll.Background.rect.width, _scroll.Background.rect.height);


        public void SetupInventory(List<BlockType> availableBlocks)
        {
            _factory.PrepareFactory();
            _items = new List<InventoryViewItem>(availableBlocks.Count);
            foreach (var item in availableBlocks)
            {
                var newItemView = _factory.CreateItem(item, _scroll.Container);
                _items.Add(newItemView);
            }

            var rectSize = _items[0].GetRectTransform.rect.size.x * _items.Count - 1;
            _scroll.SetWidth(rectSize);
            _scroll.OnTryPickItem.Subscribe(CheckItem).AddTo(_disposables);
            _scroll.ChangeScrollState(true);
        }

        private void CheckItem(PointerEventData eventData)
        {
            var results = new List<RaycastResult>();
            _raycaster.Raycast(eventData, results);
            foreach (var item in results)
            {
                if (!item.gameObject.CompareTag(InventoryViewItem.TAG)) continue;

                if (item.gameObject.TryGetComponent<InventoryViewItem>(out var viewItem))
                {
                    _scroll.StopDrag();
                    _blockPicked.OnNext(viewItem.BlockType);
                    break;
                }
            }
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}