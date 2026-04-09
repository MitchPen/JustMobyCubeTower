using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Tower;

namespace Core.GamePlay.Level.Conditions
{
    public abstract class Condition
    {
        public virtual bool CheckCondition(BaseBlock newBlock, BlockTowerData data)
        {
            return true;
        }
    }
}