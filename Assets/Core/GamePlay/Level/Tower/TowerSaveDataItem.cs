using System;
using Core.GamePlay.Level.Block;

namespace Core.GamePlay.Level.Tower
{
    [Serializable]
    public struct TowerSaveDataItem
    {
        public BlockType BlockType;
        public float PositionX;
    }
}