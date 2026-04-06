using Core.GamePlay.Level.Tower;
using Core.Services.SavingService;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level
{
    public class Level : MonoBehaviour
    {
        private ISavingService _savingService;
        private TowerSavesData _towerSavesData;
        private TowerModel _towerModel;

        [Inject]
        public void Initialize(ISavingService savingService)
        {
            _savingService = savingService;
            _towerModel = new TowerModel();
            InitializeSaveContainer();
        }
        
        private void InitializeSaveContainer()
        {
            if (_savingService.LoadData(TowerSavesData.SAVES_KEY, out _towerSavesData))
            {
                ReconstructLevel();
            }
            else
            {
                _towerSavesData = new TowerSavesData();
            }
        }

        private void ReconstructLevel()
        {
            
        }
    }
}