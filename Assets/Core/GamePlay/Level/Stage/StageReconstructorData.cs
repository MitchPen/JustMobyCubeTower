using Core.GamePlay.Level.Factory;
using Core.GamePlay.Level.Tower;

namespace Core.GamePlay.Level.Stage
{
    public struct StageReconstructorData
    {
        public TowerSavesData SavesData;
        public BlockTower Tower;
        public IBlockFactory Factory;
    }
}