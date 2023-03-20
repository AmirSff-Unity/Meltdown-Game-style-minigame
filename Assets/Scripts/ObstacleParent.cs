using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleParent : MonoBehaviour
{
    public Vector3 rotateSpeed;
    private bool active = true;

    public bool Active { get { return active; } set { active = value; } }

    private void Update()
    {
        if (active)
            transform.eulerAngles = transform.eulerAngles + (rotateSpeed * Time.deltaTime);
    }
}
