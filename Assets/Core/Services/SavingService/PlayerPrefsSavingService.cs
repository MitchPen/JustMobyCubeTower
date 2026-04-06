using System;
using UnityEngine;

namespace Core.Services.SavingService
{
    public class PlayerPrefsSavingService : ISavingService
    {
        
        public void Save<T>(T item, string saveKey) where T : SaveContainer
        {
            var stringData = JsonUtility.ToJson(item);
            SaveStringToPrefs(saveKey,  stringData);
        }

        public bool LoadData<T>(string saveKey, out T output) where T : SaveContainer
        {
            var stringValue = LoadStringFromPrefs(saveKey);
            if (string.IsNullOrEmpty(stringValue))
            {
                output = null;
                return false;
            }
            
            try
            {
                output = JsonUtility.FromJson<T>(stringValue);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Can't load saved data by key = {saveKey} with message: {e.Message}");
                output = null;
                return false;
            }
        }

        private string LoadStringFromPrefs(string saveKey)
        {
            var str = PlayerPrefs.GetString(saveKey);
            return str;
        }

        private void SaveStringToPrefs(string saveKey, string stringValue)
        {
            PlayerPrefs.SetString(saveKey, stringValue);
        }
    }
}
