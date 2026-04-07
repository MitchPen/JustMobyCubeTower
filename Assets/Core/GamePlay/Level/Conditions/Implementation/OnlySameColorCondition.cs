using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Tower;

namespace Core.GamePlay.Level.Conditions.Implementation
{
    public class OnlySameColorCondition : Condition
    {
        public override bool CheckCondition(BaseBlock newBlock, BlockTower model)
        {
            if (model.GetLastBlock() == null) return true;
            if (newBlock.BlockType != model.GetLastBlock().BlockType) return false;
            return true;
        }
    }
}