namespace Core.Services.SavingService
{
    public interface ISavingService
    {
        public void Save<T>(T item, string saveKey) where T: SaveContainer;
        public bool LoadData<T>(string saveKey, out T container) where T: SaveContainer;
    }
}
