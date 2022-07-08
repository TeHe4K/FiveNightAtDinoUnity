using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texttransform : MonoBehaviour
{
    public Transform target;
    public Camera camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);
    }
}
