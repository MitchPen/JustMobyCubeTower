using System.Collections.Generic;
using Core.GamePlay.Level.Block;
using UnityEngine;

namespace Core.GamePlay.Level.Factory
{
    [CreateAssetMenu(fileName = "BlockFactoryConfig", menuName = "Configs/BlockFactoryConfig")]
    public class BlockFactoryConfig : ScriptableObject
    {
        public BaseBlock Prefab;
        public List<BlockFactoryConfigItem> Sprites;
    }
}