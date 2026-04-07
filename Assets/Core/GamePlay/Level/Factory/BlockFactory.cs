using System.Collections.Generic;
using System.Linq;
using Core.GamePlay.Level.Block;
using Core.Services.GameObjectPool;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private const int _poolSize = 10;
        private BlockFactoryConfig _config;
        private IGameObjectPool _pool;
        private Dictionary<BlockType, Sprite> _blockView;

        [Inject]
        public BlockFactory(BlockFactoryConfig config, IGameObjectPool pool)
        {
            _config = config;
            _pool = pool;
        }

        public BaseBlock CreateBlock(BlockType type, Vector2 position)
        {
            BaseBlock block;
            if (_pool.CheckForAvailable<BaseBlock>())
                block = _pool.GetAvailable<BaseBlock>();
            else
                block = Object.Instantiate(_config.Prefab, position, Quaternion.identity);
            
            _blockView.TryGetValue(type, out Sprite sprite);
            block.Setup(type, sprite);
            block.transform.position = position;
            return block;
        }

        private void InitializePool(Transform poolContainer)
        {
            _pool.SetStashContainer(poolContainer);

            for (int i = 0; i < _poolSize; i++)
            {
                var emptyBlock = Object.Instantiate(_config.Prefab, Vector3.zero, Quaternion.identity);
                _pool.ReturnToPool(emptyBlock);
            }
        }

        public void Initialize(Transform poolContainer)
        {
            InitializePool(poolContainer);
            _blockView = new Dictionary<BlockType, Sprite>();
            foreach (var item in _config.Sprites)
                _blockView.Add(item.Type, item.Sprite);
        }
    }
}