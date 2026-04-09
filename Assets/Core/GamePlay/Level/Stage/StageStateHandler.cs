using System;
using System.Collections.Generic;
using System.Linq;
using Core.GamePlay.Level.Tower;
using Core.Services.SavingService;
using UniRx;

namespace Core.GamePlay.Level.Stage
{
    public class StageStateHandler : IDisposable
    {
        private TowerSavesData _savesData;
        private BlockTowerData _blockTower;
        private ISavingService _savingService;
        private CompositeDisposable _disposable;

        public void Setup(ISavingService savingService, TowerSavesData savesData, BlockTowerData blockTower)
        {
            _savingService = savingService;
            _disposable = new CompositeDisposable();
            _savesData = savesData;
            _blockTower = blockTower;
            _blockTower.OnDataUpdate.Subscribe(_ => { UpdateSaves(); }).AddTo(_disposable);
        }

        private void UpdateSaves()
        {
            _savesData.Data = new List<TowerSaveDataItem>(_blockTower.BlockCount);
            var blocks = _blockTower.BlocksData.ToArray();
            foreach (var block in blocks)
            {
                _savesData.Data.Add(new TowerSaveDataItem()
                {
                    BlockType = block.Key.BlockType,
                    PositionX = block.Key.transform.localPosition.x
                });
            }

            _savingService.Save(_savesData, TowerSavesData.SAVES_KEY);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}