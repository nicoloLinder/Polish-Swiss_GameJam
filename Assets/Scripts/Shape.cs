using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Shape : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.up*100);
        }
    }
}