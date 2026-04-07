namespace Core.GamePlay.Level.Stage
{
    public class StageReconstructor
    {
        public void RestoreStage(StageReconstructorData  restoreData)
        {
            if(restoreData.SavesData.Data.Count == 0) return;
            foreach (var item in restoreData.SavesData.Data)
            {
               var block = restoreData.Factory.CreateBlock(item.BlockType, item.Position);
               restoreData.Tower.AddBlock(block);
            }
        }
    }
}