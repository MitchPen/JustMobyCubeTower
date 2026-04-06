using System.Collections.Generic;
using Core.GamePlay.Level.Conditions;
using Core.GamePlay.Level.SetupProvider;
using Core.GamePlay.Level.Tower;
using Core.Services.SavingService;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level
{
    public class Level : MonoBehaviour
    {
        private ISavingService _savingService;
        private ILevelSetupProvider _levelSetupProvider;

        private TowerSavesData _towerSavesData;
        private TowerModel _towerModel;
        private List<Condition> _levelConditions;

        [Inject]
        public void Initialize(ISavingService savingService, ILevelSetupProvider levelSetupProvider)
        {
            _levelSetupProvider = levelSetupProvider;
            _savingService = savingService;
            _towerModel = new TowerModel();
            Setup();
        }

        private void Setup()
        {
            InitializeLevelConditions();
            LoadSaves();
        }

        private void LoadSaves()
        {
            if (_savingService.LoadData(TowerSavesData.SAVES_KEY, out _towerSavesData))
            {
                ConstructTower();
            }
            else
            {
                _towerSavesData = new TowerSavesData();
            }
        }

        private void InitializeLevelConditions()
        {
            var conditionFactory = new ConditionFactory();
            var conditions = _levelSetupProvider.GetLevelSetup().LevelConditions;
            if (conditions == null || conditions.Count == 0) return;
            _levelConditions = new List<Condition>(conditions.Count);
            foreach (var conditionType in conditions)
            {
                _levelConditions.Add(conditionFactory.CreateCondition(conditionType));
            }
        }

        private void ConstructTower()
        {
        }
    }
}