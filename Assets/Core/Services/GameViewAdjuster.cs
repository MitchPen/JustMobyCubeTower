using UnityEngine;

namespace Core.Services
{
    public class GameViewAdjuster : MonoBehaviour
    {
        private const float _ppi = 100f;
        
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _gameField;
        [SerializeField] private float _tabletAspectRatio;
        [SerializeField] private Vector2 _targetScreenRatio;
        [SerializeField] private Vector2 _orthographicSizeRange;

        private void Awake()
        {
            var currentScreen = Screen.currentResolution;
            var aspectRatio = (float)currentScreen.width / (float)currentScreen.height;

            var cameraSize = Mathf.Clamp(_orthographicSizeRange.x, _orthographicSizeRange.y,
                currentScreen.height / (aspectRatio * _ppi));
            _camera.orthographicSize = cameraSize;

            if (aspectRatio <= _tabletAspectRatio)
            {
                var targetAspectRatio = _targetScreenRatio.x / _targetScreenRatio.y;
                var filedScaleMultiplier =aspectRatio / targetAspectRatio;
                _gameField.localScale *=  filedScaleMultiplier;
            }
        }
    }
}