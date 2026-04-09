using UnityEngine;

namespace Core.Services.GameObjectPool
{
    public interface IGameObjectPool
    {
        public void SetStashContainer(Transform parent);
        public bool CheckForAvailable<T>() where T : MonoBehaviour;
        public T GetAvailable<T>() where T : MonoBehaviour;
        public void ReturnToPool<T>(T obj) where T : MonoBehaviour;
    }
}