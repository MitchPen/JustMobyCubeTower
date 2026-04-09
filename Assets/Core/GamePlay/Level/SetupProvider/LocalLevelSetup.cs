using UnityEngine;

namespace Core.GamePlay.Level.SetupProvider
{
    [CreateAssetMenu(fileName = "Level Setup", menuName = "Configs/Level Setup")]
    public class LocalLevelSetup : ScriptableObject
    {
        public LevelSetup LevelSetup;
    }
}