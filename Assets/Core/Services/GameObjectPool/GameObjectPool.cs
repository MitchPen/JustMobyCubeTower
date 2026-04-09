using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.GameObjectPool
{
    public class GameObjectPool : IGameObjectPool
    {
        Dictionary<Type, Queue<GameObject>> _pool = new Dictionary<Type, Queue<GameObject>>();
        private Transform _poolContainer;

        public void SetStashContainer(Transform parent) => _poolContainer = parent;

        public bool CheckForAvailable<T>() where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!_pool.TryGetValue(type, out var pool)) return false;
            return pool.Count > 0;
        }

        public T GetAvailable<T>() where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!_pool.TryGetValue(type, out var pool)) return null;
            return pool.Dequeue().GetComponent<T>();
        }

        public void ReturnToPool<T>(T obj) where T : MonoBehaviour
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolContainer);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.rotation = new Quaternion(0, 0, 0, 0);
            var type = obj.GetType();
            if (!_pool.ContainsKey(type))
                _pool.Add(type, new Queue<GameObject>());
            _pool[type].Enqueue(obj.gameObject);
        }
    }
}