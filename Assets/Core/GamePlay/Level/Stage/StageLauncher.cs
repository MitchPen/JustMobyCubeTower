using Core.GamePlay.Level.Factory;
using Core.GamePlay.Level.SetupProvider;
using Core.GamePlay.Level.Tower;
using Core.Services.SavingService;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Stage
{
    public class StageLauncher : MonoBehaviour
    {
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private Transform _towerContainer;
        [SerializeField] private StageGameplayHandler stageGameplayHandler;

        private ISavingService _savingService;
        private ILevelSetupProvider _levelSetupProvider;
        private IBlockFactory _blockFactory;

        private TowerSavesData _towerSavesData;
        private BlockTower _blockTower;
        private StageConditionChecker _stageConditionChecker;
        private StageReconstructor _stageReconstructor;
        private StageStateHandler _stageStateHandler;


        [Inject]
        public void Initialize(ISavingService savingService,
            ILevelSetupProvider levelSetupProvider,
            IBlockFactory blockFactory)
        {
            _levelSetupProvider = levelSetupProvider;
            _savingService = savingService;
            _blockFactory = blockFactory;
            _stageConditionChecker = new StageConditionChecker();
            _stageReconstructor = new StageReconstructor();
            _stageStateHandler = new StageStateHandler();
        }

        private void Awake()
        {
            _blockTower = new BlockTower();
            _stageConditionChecker.InitializeLevelConditions(_levelSetupProvider.GetLevelSetup().LevelConditions);
            _blockFactory.Initialize(_poolContainer);
            LoadSaves();
            _stageReconstructor.RestoreStage(new StageReconstructorData()
            {
                SavesData = _towerSavesData,
                Tower = _blockTower,
                Factory = _blockFactory
            });

            stageGameplayHandler.Launch();
        }

        private void LoadSaves()
        {
            if (!_savingService.LoadData(TowerSavesData.SAVES_KEY, out _towerSavesData))
                _towerSavesData = new TowerSavesData();
        }
    }
}