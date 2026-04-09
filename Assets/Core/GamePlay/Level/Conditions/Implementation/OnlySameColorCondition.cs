using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Tower;

namespace Core.GamePlay.Level.Conditions.Implementation
{
    public class OnlySameColorCondition : Condition
    {
        //example class
        
        public override bool CheckCondition(BaseBlock newBlock, BlockTowerData data)
        {
            if (data.GetLastBlock() == null) return true;
            if (newBlock.BlockType != data.GetLastBlock().BlockType) return false;
            return true;
        }
    }
}