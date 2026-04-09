using System.Collections.Generic;
using Core.Services.SavingService;

namespace Core.GamePlay.Level.Tower
{
    [System.Serializable]
    public class TowerSavesData : SaveContainer
    {
        public const string SAVES_KEY = "BlockTower/TowerSavesData/Saves";
        public List<TowerSaveDataItem> Data = new List<TowerSaveDataItem>();
    }
}