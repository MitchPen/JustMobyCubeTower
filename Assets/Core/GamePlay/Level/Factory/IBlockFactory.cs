using Core.GamePlay.Level.Block;
using UnityEngine;

namespace Core.GamePlay.Level.Factory
{
    public interface IBlockFactory
    {
        
        public BaseBlock CreateBlock(BlockType type, Vector2 position);

        public void Initialize(Transform poolContainer);
    }
}