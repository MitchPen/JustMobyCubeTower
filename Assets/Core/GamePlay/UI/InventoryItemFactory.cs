using System.Collections.Generic;
using Core.GamePlay.Level.Block;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.UI
{
    public class InventoryItemFactory
    {
        private InventoryFactoryConfig _config;
        private Dictionary<BlockType, Sprite> _sprites = new Dictionary<BlockType, Sprite>();

        [Inject]
        public InventoryItemFactory(InventoryFactoryConfig config)
        {
            _config = config;
        }

        public InventoryViewItem CreateItem(BlockType type, RectTransform parent)
        {
            if (!_sprites.ContainsKey(type)) return null;
            var item = Object.Instantiate(_config.ViewItemPrefab, parent);
            item.Setup(type, _sprites[type]);
            return item;
        }

        public void PrepareFactory()
        {
            _config.ViewItems.ForEach(i => { _sprites.Add(i.Type, i.Sprite); });
        }
    }
}