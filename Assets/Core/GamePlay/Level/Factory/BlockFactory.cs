using Core.GamePlay.Level.Block;
using Core.Services.GameObjectPool;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private BlockFactoryConfig _config;
        private IGameObjectPool _pool;
        
        [Inject]
        public BlockFactory(BlockFactoryConfig config, IGameObjectPool pool)
        {
            _config = config;
            _pool = pool;
        }
        
        public BaseBlock CreateBlock(BlockType type, Vector2 position)
        {
            return null;
        }
    }
}