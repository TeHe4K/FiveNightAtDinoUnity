using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCharacterController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistanceRay;

    private Ray _ray;
    private RaycastHit _hit;

    private void DrawRay()
    {
        if (Physics.Raycast(_ray, out _hit,_maxDistanceRay))
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay , Color.blue);
        }
        if (_hit.transform == null)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay , Color.red);
        }
    }
}
