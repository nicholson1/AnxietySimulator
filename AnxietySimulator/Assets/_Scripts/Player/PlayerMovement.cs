using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private bool is_walking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit click;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out click, Mathf.Infinity))
            {
                agent.SetDestination(click.point);
                //Debug.Log(is_walking);
                if (is_walking == false)
                {
                    //Debug.Log("iran");
                    animator.SetBool("Walking", true);
                    is_walking = true;

                }
            }
        }

        if (Vector3.Distance(agent.destination , transform.position) < .1f)
        {
            animator.SetBool("Walking", false);
            is_walking = false;

        }
        
        
        
    }
}
