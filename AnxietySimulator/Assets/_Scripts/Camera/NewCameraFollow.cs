using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewCameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 target_Offset;
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform lineTargetL;
    [SerializeField] private Transform lineTargetR;
    

    private bool rotate = true;
    public float rotateSpeed;
    
    private void Awake()
    {
        GameManager.AnxietyChanged += AnxietyHasChanged;
    }

    private void OnDestroy()
    {
        GameManager.AnxietyChanged -= AnxietyHasChanged;
    }
   
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        target_Offset = transform.position - target.position;
        //rotateTarget = transform.eulerAngles;
    }
    void Update()
    {
        
       
        transform.Rotate(0, Input.GetAxis("Horizontal")*rotateSpeed*Time.deltaTime, 0);
        
        
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+target_Offset, 0.1f);
        }
        
        
    }

    private void AnxietyHasChanged(int anxietyLevel)
    {
        if (anxietyLevel > 50)
        {
            rotateSpeed = anxietyLevel * 10;
        }
        else
        {
            rotateSpeed = (anxietyLevel * 2) + 50;
        }
        
    }
}
