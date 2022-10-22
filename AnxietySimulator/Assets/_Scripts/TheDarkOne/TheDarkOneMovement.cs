using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheDarkOneMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private NavMeshAgent _player;

    public bool followPlayer = true;
    public float followDistance;
    public float stopDistance;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        _player = GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (followPlayer)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if ( distance > followDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(_player.transform.position);
                animator.SetBool("Swiming", true);

            }
            else if (distance < stopDistance)
            {
                agent.isStopped = true;
                animator.SetBool("Swiming", false);
            }

            if (distance > 10)
            {
                agent.speed = distance;
            }
            else
            {
                agent.speed = 2.5f;
            }
        }
        
    }
}
