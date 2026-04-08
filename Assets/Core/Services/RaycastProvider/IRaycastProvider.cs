using UnityEngine;

namespace Core.Services.RaycastProvider
{
    public interface IRaycastProvider
    {
        public bool ThrowRay(Vector2 point, out GameObject resultObject);
    }
}