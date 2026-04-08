using System;
using UnityEngine;

namespace Core.GamePlay.Level.Tower
{
    public class TowerWorldContainer : MonoBehaviour
    {
        [SerializeField] private Vector2 _floor;


        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, new Vector3(5, 1, 1));
        }
    }
}
