using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParentRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = Vector3.one;

    private void Update()
    {
        transform.eulerAngles += rotationSpeed * Time.deltaTime;    
    }
}
