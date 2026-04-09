using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.Services
{
    public class GameScreenAdjuster : MonoBehaviour
    {
        private const float _ppi = 100f;
        
        [Inject] private ICameraProvider _cameraProvider;
        
        [SerializeField] private float _tabletAspectRatio;
       
        [SerializeField] private Vector2 _orthographicSizeRange;
        
        public bool IsTablet {get; private set;}
       

        private void Awake()
        {
            var currentScreen = Screen.currentResolution;
            var aspectRatio = (float)currentScreen.width / (float)currentScreen.height;

            var cameraSize = Mathf.Clamp(_orthographicSizeRange.x, _orthographicSizeRange.y,
                currentScreen.height / (aspectRatio * _ppi));
            _cameraProvider.GetCamera().orthographicSize = cameraSize;

            IsTablet = aspectRatio <= _tabletAspectRatio;
        }
    }
}