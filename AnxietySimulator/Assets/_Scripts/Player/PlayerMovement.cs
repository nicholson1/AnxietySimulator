using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Vector3 targetPos;


    private bool is_walking = false;
    private bool is_idle ;

    private void Awake()
    {
        YarnInteract.InConvo += Convo;
        //YarnInteract.EndConvo += EndConvo;

        
    }
    private void OnDestroy()
    {
        YarnInteract.InConvo -= Convo;
        //YarnInteract.EndConvo += EndConvo;

    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    

    void Update()
    {

        if (!_inConvo)
        {


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit click;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                if (!isOverUI && Physics.Raycast(ray, out click, Mathf.Infinity))
                {
                    agent.SetDestination(click.point);
                    targetPos = click.point;
                    //Debug.Log(is_walking);
                    if (!is_walking)
                    {

                        animator.SetTrigger("Move");
                        is_walking = true;
                        is_idle = false;

                    }
                }
            }

            if (Vector3.Distance(targetPos, this.transform.position) < .25f)
            {
                if (!is_idle)
                {
                    is_walking = false;
                    is_idle = true;
                    animator.SetTrigger("Idle");
                }
            }
        }
        
    
       
        
        
        
    }

    private bool _inConvo;

    private void Convo(string inConvo)
    {
        if (inConvo != null)
        {
            _inConvo = true;
        }
        else
        {
            _inConvo = false;
        }
        

        if (inConvo == "DarkSelfConfrontation" || inConvo == "MomCall")
        {
            _inConvo = false;
        }
    }
    
    public void FixAnimationBug()
    {
        agent.SetDestination(this.transform.position);
    }
   

   

   

   
        
        
        
    }

