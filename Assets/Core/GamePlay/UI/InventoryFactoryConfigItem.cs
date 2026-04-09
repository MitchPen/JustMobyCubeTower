using System;
using Core.GamePlay.Level.Block;
using UnityEngine;

namespace Core.GamePlay.UI
{
    [Serializable]
    public struct InventoryFactoryConfigItem
    {
        public BlockType Type;
        public Sprite Sprite;
    }
}