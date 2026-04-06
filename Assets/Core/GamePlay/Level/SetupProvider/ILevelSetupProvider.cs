using System.Collections.Generic;
using Core.GamePlay.Level.Conditions;

namespace Core.GamePlay.Level.SetupProvider
{
    public interface ILevelSetupProvider
    {
        public LevelSetup GetLevelSetup();
    }
}