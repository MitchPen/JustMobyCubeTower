using System;
using System.Collections.Generic;
using Core.GamePlay.Level.Block;
using UniRx;

namespace Core.GamePlay.Level.Tower
{
    public class BlockTowerData
    {
        private Subject<Unit> _onDataUpdate = new Subject<Unit>();
        private Dictionary<BaseBlock, TowerNode> _blocks = new();
        private TowerNode _lastElement = new();
        
        public Dictionary<BaseBlock, TowerNode> GetTowerData() => _blocks;
        
        public BaseBlock GetLastBlock() => _lastElement.CurrentBaseBlock;
        
        public IObservable<Unit> OnDataUpdate => _onDataUpdate;
        
        public int BlockCount => _blocks?.Count ?? 0;
        
        public Dictionary<BaseBlock, TowerNode>  BlocksData => _blocks;
        
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
            _onDataUpdate.OnNext(Unit.Default);
        }

        public void RemoveBlock(BaseBlock baseBlock)
        {
            var node = _blocks[baseBlock];
            _blocks.Remove(baseBlock);
            
            if (node.Previous != null)
                node.Previous.Next = node.Next;
            
            _onDataUpdate.OnNext(Unit.Default);
        }
    }
}