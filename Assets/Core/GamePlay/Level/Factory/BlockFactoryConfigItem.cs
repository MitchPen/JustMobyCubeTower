using System;
using Core.GamePlay.Level.Block;
using UnityEngine;

namespace Core.GamePlay.Level.Factory
{
    [Serializable]
    public struct BlockFactoryConfigItem
    {
        public BlockType Type;
        public Sprite Sprite;
    }
}