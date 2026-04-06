using Zenject;

namespace Core.GamePlay.Level.SetupProvider
{
    public class LocalLevelSetupProvider : ILevelSetupProvider
    {
        private LevelSetup _levelSetup;
        
        [Inject]
        public LocalLevelSetupProvider(LocalLevelSetup localLevelSetup)
        {
            _levelSetup = localLevelSetup.LevelSetup;
        }

        public LevelSetup GetLevelSetup() => _levelSetup;
    }
}