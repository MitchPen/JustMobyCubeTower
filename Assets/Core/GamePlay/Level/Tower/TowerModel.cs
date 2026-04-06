using System.Collections.Generic;
using Core.GamePlay.Level.Block;

namespace Core.GamePlay.Level.Tower
{
    public class TowerModel
    {
        private Dictionary<BaseBlock, TowerNode> _blocks = new();
        private TowerNode _lastElement;
        
        public Dictionary<BaseBlock, TowerNode> GetTowerData() => _blocks;

        public void LoadTowerSetup(BaseBlock[] setup)
        {
            foreach (var block in setup)
                AddBlock(block);
        }

        public void AddBlock(BaseBlock newBaseBlock)
        {
            var newElement = new TowerNode()
            {
                CurrentBaseBlock = newBaseBlock,
                Previous = _blocks?.Count > 0 ? _lastElement : null,
                Next = null
            };

            if (_lastElement != null)
                _lastElement.Next =  newElement;
            
            _blocks?.Add(newBaseBlock, newElement);
            _lastElement = newElement;
        }

        public void RemoveBlock(BaseBlock baseBlock)
        {
            var node = _blocks[baseBlock];
            _blocks.Remove(baseBlock);
            
            if (node.Previous != null)
                node.Previous.Next = node.Next;
        }
    }
}