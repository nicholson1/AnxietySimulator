using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewCameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 target_Offset;
    [SerializeField] private GameObject camera;

    private bool rotate = true;
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
        
        

        if (rotate)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, target.eulerAngles, Time.deltaTime/2);
        }
    }
}
