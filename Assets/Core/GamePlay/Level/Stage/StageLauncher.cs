using Core.GamePlay.Level.Factory;
using Core.GamePlay.Level.SetupProvider;
using Core.GamePlay.Level.Tower;
using Core.GamePlay.UI;
using Core.Services.SavingService;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level.Stage
{
    public class StageLauncher : MonoBehaviour
    {
        [SerializeField] private SceneViewAdjuster _sceneViewAdjuster;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private StageGameplayHandler stageGameplayHandler;

        private ISavingService _savingService;
        private ILevelSetupProvider _levelSetupProvider;
        private IBlockFactory _blockFactory;
        private InventoryView _inventoryView;

        private TowerSavesData _towerSavesData;
        private BlockTower _blockTower;
        private StageConditionChecker _stageConditionChecker;
        private StageReconstructor _stageReconstructor;
        private StageStateHandler _stageStateHandler;

        [Inject]
        public void Initialize(ISavingService savingService,
            ILevelSetupProvider levelSetupProvider,
            IBlockFactory blockFactory,
            InventoryView inventoryView)
        {
            _levelSetupProvider = levelSetupProvider;
            _savingService = savingService;
            _blockFactory = blockFactory;
            _inventoryView = inventoryView;
            _stageConditionChecker = new StageConditionChecker();
            _stageReconstructor = new StageReconstructor();
            _stageStateHandler = new StageStateHandler();
        }

        private void Start()
        {
            _blockTower = new BlockTower();
            _stageConditionChecker.InitializeLevelConditions(_levelSetupProvider.GetLevelSetup().LevelConditions);
            _blockFactory.Initialize(_poolContainer, _sceneViewAdjuster.ScaleFactor);
            LoadSaves();
            _stageReconstructor.RestoreStage(new StageReconstructorData()
            {
                SavesData = _towerSavesData,
                Tower = _blockTower,
                Factory = _blockFactory
            });
            _inventoryView.SetupInventory(_levelSetupProvider.GetLevelSetup().AvailableBlocks);
            stageGameplayHandler.Launch();
        }

        private void LoadSaves()
        {
            if (!_savingService.LoadData(TowerSavesData.SAVES_KEY, out _towerSavesData))
                _towerSavesData = new TowerSavesData();
        }
    }
}