using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCMovement : MonoBehaviour
{
    public CurrentState MoveState;
    
    public bool DebugMovement = false;
    [SerializeField] private GameObject DebugIndicatorPrefab;
    private GameObject test;

    [SerializeField] private NavMeshAgent navAgent;
    private Vector3 nextLocation;
    public float wanderDistance = 10f;
    //private float walkDistance = 10f;

    [SerializeField]private NPC_Controller NpcController;

    private NPCMovement NPCInteract = null;


    //private float IdleWaitTime = -3f;

    private void Start()
    {
       
        Car.Honk += HonkedAt;
        //nextLocation = this.transform.position;

        StartCoroutine(PauseDuringWander());
    }

    private void OnDestroy()
    {
        Car.Honk -= HonkedAt;
    }

    private void LateUpdate()
    {
        //Debug.Log(navAgent.remainingDistance);
        switch (MoveState)
        {
            
            
            case CurrentState.wander:
                DoWander();
                if (CheckInteract())
                {
                    StartCoroutine(InteractDuringWander());
                }
                break;
        }
    }

   


   

   
    private void DoWander()
    {
        //if close choose next location

        if(navAgent.remainingDistance < 1f)
        {
            Vector3 random = Random.insideUnitSphere * wanderDistance;
            random.y = 0f;
            nextLocation = this.transform.position + random;

            if(NavMesh.SamplePosition(nextLocation, out NavMeshHit hit, 5f , 1))
            {
                nextLocation = hit.position;
                navAgent.SetDestination(nextLocation);

                if (DebugMovement)
                {
                    DebugTargetPosition();
                }

                if (navAgent.remainingDistance != 0)
                {
                    StartCoroutine(PauseDuringWander());

                }
            }            
        }

        
    }

    IEnumerator PauseDuringWander()
    {

        float time = Random.Range(2, 7);
        navAgent.isStopped = true;
        NpcController.SetIdle();
        //idle
        yield return new WaitForSeconds(time);
        navAgent.isStopped = false;
        NpcController.SetWalk();
        NPCInteract = null;
        //walk

    }

    private void HonkedAt(GameObject me, Transform car)
    {
        if (me == this.gameObject && canBeHonkedAt)
        {
            this.transform.LookAt(car.position);
            StartCoroutine(HonkedAtDuringWander());
        }
    }

    private bool canBeHonkedAt = true;
    public IEnumerator HonkedAtDuringWander()
    {
        //Debug.Log("Start Interaction");
        canBeHonkedAt = false;
        MoveState = CurrentState.interacting;
        
        float time = Random.Range(2, 4);
        navAgent.isStopped = true;
        NpcController.SetWave();
        //wave
        yield return new WaitForSeconds(time);
        navAgent.isStopped = false;
        NpcController.SetWalk();
        MoveState = CurrentState.wander;
        NPCInteract = null;
        yield return new WaitForSeconds(5);
        canBeHonkedAt = true;


        //walk

    }
    
    public IEnumerator InteractDuringWander()
    {
        //Debug.Log("Start Interaction");
        MoveState = CurrentState.interacting;
        this.transform.LookAt(NPCInteract.transform);
        NPCInteract.transform.LookAt(this.transform);
        float time = Random.Range(2, 4);
        navAgent.isStopped = true;
        NpcController.SetWave();
        //wave
        yield return new WaitForSeconds(time);
        navAgent.isStopped = false;
        NpcController.SetWalk();
        MoveState = CurrentState.wander;
        NPCInteract = null;

        //walk

    }
    
    public enum CurrentState
    {
        walkingToLocation,
        idle,
        wander,
        interacting,
        waiting,
    }

    private bool CheckInteract()
    {
        return NPCInteract;
    }
    

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("NPC"))
        {
            //check if im interacting
            if (MoveState != CurrentState.interacting)
            {
                NPCMovement temp = other.GetComponent<NPCMovement>();
                //check if they interacting
                if (temp.MoveState != CurrentState.interacting)
                {
                    //perform CHECK TALKABLE
                    

                    if (NpcController.InteractCheck() && temp.NpcController.InteractCheck())
                    {
                        //Debug.Log("Iran");
                        temp.NPCInteract = this;
                        NPCInteract = temp;
                    }
                } 
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
       
        if(other.CompareTag("NPC"))
        {
            if (NPCInteract == other.GetComponent<NPCMovement>())
            {
                NPCInteract = null;
            }
        }
        
    }
    


    private void DebugTargetPosition()
    {
        if (test == null)
        {
            test = Instantiate(DebugIndicatorPrefab);
        }

        test.transform.position = navAgent.destination;
    }
}
