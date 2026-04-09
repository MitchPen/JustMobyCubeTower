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
        [SerializeField] private BlockTower _blockTower;

        private ISavingService _savingService;
        private ILevelSetupProvider _levelSetupProvider;
        private IBlockFactory _blockFactory;
        private InventoryView _inventoryView;
        private TowerSavesData _towerSavesData;
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
            _stageReconstructor = new StageReconstructor();
            _stageStateHandler = new StageStateHandler();
        }

        private void Start()
        {
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
            _stageStateHandler.Setup(_savingService, _towerSavesData, _blockTower.BlockTowerData);
        }

        private void LoadSaves()
        {
            if (!_savingService.LoadData(TowerSavesData.SAVES_KEY, out _towerSavesData))
                _towerSavesData = new TowerSavesData();
        }
    }
}