using System;
using Core.GamePlay.Level.Block;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Core.GamePlay.UI
{
    public class InventoryViewItem : MonoBehaviour
    {
        public const string TAG = "InventoryItem";

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _renderer;
        
        private BlockType _blockType;

        public BlockType BlockType => _blockType;

        public RectTransform GetRectTransform => _rectTransform;

        public void Setup(BlockType blockType, Sprite sprite)
        {
            _renderer.sprite = sprite;
            _blockType = blockType;
        }
    }
}