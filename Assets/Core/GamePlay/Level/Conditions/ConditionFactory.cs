using Core.GamePlay.Level.Conditions.Implementation;

namespace Core.GamePlay.Level.Conditions
{
    public class ConditionFactory
    {
        public Condition CreateCondition(ConditionTypes type)
        {
            switch (type)
            {
                case ConditionTypes.ONLY_SAME_COLOR: return new OnlySameColorCondition();
                case ConditionTypes.NOT_SAME_AS_PREVIOUS_COLOR: return new NotSameAsPreviousCondition();
                case ConditionTypes.ONLY_UNIQUE_COLOR: return new OnlyUniqueColorCondition();
            }
            return null;
        }
    }
}