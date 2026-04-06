using System;
using System.Collections.Generic;
using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Conditions;

namespace Core.GamePlay.Level.SetupProvider
{
    [Serializable]
    public class LevelSetup
    {
        public List<BlockType> AvailableBlocks;
        public List<ConditionTypes> LevelConditions;
    }
}