using UnityEngine;

namespace Core.Services.CameraProvider
{
    public class CameraProvider : MonoBehaviour, ICameraProvider
    {
        [SerializeField] private Camera _camera;
        
        public Camera GetCamera() => _camera;
    }
}