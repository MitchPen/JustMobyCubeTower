using System.Collections.Generic;
using Core.GamePlay.Level.Block;
using DG.Tweening;
using UnityEngine;
using SysRand = System.Random;

namespace Core.GamePlay.Level.Tower
{
    public class BlockTower :  MonoBehaviour
    {
        private BlockTowerData _blockTowerData =  new BlockTowerData();
        private int _shiftMultiplier = 0;
        
        public BlockTowerData BlockTowerData => _blockTowerData;
        
        public void AddBlock(BaseBlock newBaseBlock)
        {
            newBaseBlock.transform.SetParent(transform);
            if (_blockTowerData.BlockCount == 0)
            {
                var currentPosition = newBaseBlock.transform.localPosition;
                currentPosition.y = 0;
                newBaseBlock.transform
                    .DOLocalMove(currentPosition, 0.25f).SetEase(Ease.InSine)
                    .OnComplete(()=>newBaseBlock.ChangeRaycastInteraction(true));
            }
            else
            {
                var lastBlock = _blockTowerData.GetLastBlock();
                var lastBlockPosition = lastBlock.transform.position;
                var blockSize = lastBlock.transform.lossyScale.y;
                var horizontalShift = Random.Range(0, blockSize / 2);
                _shiftMultiplier = _shiftMultiplier == 0
                    ? (new SysRand().Next(0, 1) > 0 ? -1 : 1)
                    : _shiftMultiplier * -1;
                horizontalShift *= _shiftMultiplier;
                var nextBlockPoint = lastBlockPosition + new Vector3(horizontalShift, blockSize, 0);
                var pointAboveLatBlock = nextBlockPoint + new Vector3(0, blockSize/2, 0);
                var motionSequence = DOTween.Sequence();
                motionSequence.Append(newBaseBlock.transform
                    .DOMove(pointAboveLatBlock, 0.25f).SetEase(Ease.InSine));
                motionSequence.Append(newBaseBlock.transform
                    .DOMove(nextBlockPoint, 0.25f).SetEase(Ease.Linear)
                    .OnComplete(()=>newBaseBlock.ChangeRaycastInteraction(true)));
            }
            
            _blockTowerData.AddBlock(newBaseBlock);
        }

        public void RemoveBlock(BaseBlock blockToRemove)
        {
            if (_blockTowerData.BlockCount > 1)
            {
                var nodeData = _blockTowerData.BlocksData[blockToRemove];
                nodeData = nodeData.Next;
                while (nodeData != null)
                {
                    var block = nodeData.CurrentBaseBlock;
                    var blockSize = block.transform.lossyScale.y;
                    var yPosition = block.transform.position.y;
                    block.ChangeRaycastInteraction(false);
                    block.transform
                        .DOMoveY(yPosition - blockSize, 0.25f ).SetEase(Ease.InSine)
                        .OnComplete(()=>block.ChangeRaycastInteraction(true));
                    nodeData =  nodeData.Next;
                }
            }
            _blockTowerData.RemoveBlock(blockToRemove);
        }

        public void LoadTowerSetup((BaseBlock block, float shift)[] setupData)
        {
            var yPos = 0f;
            var resultSetup =new List<BaseBlock>();
            foreach (var item in setupData)
            {
                item.block.transform.SetParent(transform);
                item.block.transform.localPosition = new Vector3(item.shift, yPos, 0f);
                item.block.ChangeRaycastInteraction(true);
                item.block.ChangeVisibility(true);
                yPos+= item.block.transform.localScale.y;
                resultSetup.Add(item.block);
            }
          
            _blockTowerData.LoadTowerSetup(resultSetup.ToArray());
        }
    }
}