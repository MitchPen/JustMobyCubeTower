using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay.UI
{
    [CreateAssetMenu(fileName = "InventoryFactoryConfig", menuName = "Configs/InventoryFactoryConfig")]
    public class InventoryFactoryConfig : ScriptableObject
    {
        public InventoryViewItem ViewItemPrefab;
        public List<InventoryFactoryConfigItem> ViewItems;
    }
}