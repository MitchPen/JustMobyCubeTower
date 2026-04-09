using Core.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace Core.Services.RaycastProvider
{
    public class Raycaster : MonoBehaviour, IRaycastProvider
    {
        [Inject] private ICameraProvider _cameraProvider;

        [SerializeField] private LayerMask _layerMask;

        private float _cameraZOffset;
        private Vector3 _rayStartPoint;
        private Vector3 _rayDirection;

        private void Awake() => _cameraZOffset = _cameraProvider.GetCamera().transform.position.z;

        public bool ThrowRay(Vector2 point, out GameObject resultObject)
        {
            resultObject = null;
            _rayStartPoint = new Vector3(point.x, point.y, _cameraZOffset);
            _rayDirection = new Vector3(point.x, point.y, 0) - _rayStartPoint;

            var castResult = Physics2D.RaycastAll(
                _rayStartPoint, _rayDirection,
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