using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 target_Offset;
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        target_Offset = transform.position - target.position;
    }
    void Update()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+target_Offset, 0.1f);
        }
    }
}
