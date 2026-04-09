using Core.GamePlay.UI;
using Core.Services;
using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.GamePlay.Level
{
    public class SceneViewAdjuster : MonoBehaviour
    {
        [Inject] private ICameraProvider _cameraProvider;
        [Inject] private InventoryView _inventoryView;

        [SerializeField] private GameScreenAdjuster gameScreenAdjuster;
        [SerializeField] private Vector2 _targetScreenRatio;
        [SerializeField] private Transform _pitTransform;
        [SerializeField] private Transform _towerContainer;
        [SerializeField] private Transform _gameField;
        [SerializeField] private float _bottonOffset;

        public float ScaleFactor { get; private set; }

        public void Awake()
        {
            ScaleFactor = 1f;
            var currentScreen = Screen.currentResolution;
            var aspectRatio = (float)currentScreen.width / (float)currentScreen.height;

            if (gameScreenAdjuster.IsTablet)
            {
                var targetAspectRatio = _targetScreenRatio.x / _targetScreenRatio.y;
                ScaleFactor = aspectRatio / targetAspectRatio;
            }

            _gameField.transform.localScale *= ScaleFactor;

            var quarterWidth = currentScreen.width / 4;
            var halfHeight = currentScreen.height / 2;
            var newPitPosition = CalcGlobalPosition(new Vector2(quarterWidth, halfHeight));
            var totalUiHeight = currentScreen.height *
                                (_inventoryView.GetInventoryScreenSize().y / _inventoryView.GetCanvasResolution().y);

            var newTowerContainerPosition = CalcGlobalPosition(new Vector2(
                currentScreen.width - quarterWidth,
                totalUiHeight));
            _pitTransform.position = newPitPosition;
            _towerContainer.position = newTowerContainerPosition;
        }

        private Vector2 CalcGlobalPosition(Vector2 screenPosition)
        {
            return _cameraProvider.GetCamera().ScreenToWorldPoint(screenPosition);
        }
    }
}