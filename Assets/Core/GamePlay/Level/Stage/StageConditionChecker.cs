using System.Collections.Generic;
using System.Linq;
using Core.GamePlay.Level.Block;
using Core.GamePlay.Level.Conditions;
using Core.GamePlay.Level.Tower;

namespace Core.GamePlay.Level.Stage
{
    public class StageConditionChecker
    {
        private List<Condition> _levelConditions;

        public void InitializeLevelConditions(List<ConditionTypes> conditionSetup)
        {
            var conditionFactory = new ConditionFactory();
            if (conditionSetup == null || conditionSetup.Count == 0) return;
            _levelConditions = new List<Condition>(conditionSetup.Count);
            foreach (var conditionType in conditionSetup)
            {
                _levelConditions.Add(conditionFactory.CreateCondition(conditionType));
            }
        }

        public bool CheckForCondition(BaseBlock newBlock, BlockTowerData data)
        {
            return _levelConditions.All(t => t.CheckCondition(newBlock, data) != false);
        }
    }
}