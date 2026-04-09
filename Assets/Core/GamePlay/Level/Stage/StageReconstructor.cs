using System.Collections.Generic;
using Core.GamePlay.Level.Block;
using UnityEngine;

namespace Core.GamePlay.Level.Stage
{
    public class StageReconstructor
    {
        public void RestoreStage(StageReconstructorData restoreData)
        {
            if (restoreData.SavesData.Data.Count == 0) return;
            var towerRestoreData = new List<(BaseBlock block, float shift)>();
            foreach (var item in restoreData.SavesData.Data)
            {
                var block = restoreData.Factory.CreateBlock(item.BlockType, Vector2.zero);
                block.ChangeRaycastInteraction(true);
                towerRestoreData.Add((block, item.PositionX));
            }
            restoreData.Tower.LoadTowerSetup(towerRestoreData.ToArray());
        }
    }
}