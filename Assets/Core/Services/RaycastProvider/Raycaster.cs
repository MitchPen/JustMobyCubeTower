
using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.Services.RaycastProvider
{
    public class Raycaster : MonoBehaviour, IRaycastProvider
    {
        [Inject] private ICameraProvider _cameraProvider;

        [SerializeField] private LayerMask _layerMask;
        private Transform _camera;
        private Vector3 _rayStartPoint;
        private Vector3 _rayDirection;

        private void Awake()
        {
            _camera = _cameraProvider.GetCamera().transform;
        }

        public bool ThrowRay(Vector2 point, out GameObject resultObject)
        {
            resultObject = null;
            _rayStartPoint = _camera.position;
            _rayDirection = new Vector3(point.x, point.y, 0) - _camera.position;
           

            var castResult = Physics2D.RaycastAll(
                _camera.position,_rayDirection,
                Mathf.Infinity, _layerMask);

            if (castResult.Length <= 0) return false;
            resultObject = castResult[0].transform.gameObject;
            return true;

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_rayStartPoint, _rayDirection * 10);
        }
    }
}