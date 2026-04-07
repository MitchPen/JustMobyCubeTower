using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Tower;

namespace Core.GamePlay.Level.Conditions.Implementation
{
    public class NotSameAsPreviousCondition : Condition
    {
        public override bool CheckCondition(BaseBlock newBlock, BlockTower model)
        {
            return base.CheckCondition(newBlock, model);
        }
    }
}