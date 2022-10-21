using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform lookTarget;
    private void Start()
    {
        lookTarget = Camera.main.transform;
    }

    void Update()
    {
        if (lookTarget == null)
        {
            lookTarget = Camera.main.transform;
        }
        
        transform.LookAt(new Vector3(lookTarget.position.x, 0, lookTarget.position.z));
    }
}
