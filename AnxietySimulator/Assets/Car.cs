using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField] private Material[] Materials;
    [SerializeField] private MeshRenderer[] Skins;
    [SerializeField] private NavMeshAgent navAgent;

    [SerializeField] private Transform Path;

    private List<Vector3> Waypoints = new List<Vector3>();

    private bool Moving = true;

    private float moveSpeed;
    
    public static event Action<GameObject, Transform> Honk;
    
    private void Randomize()
    {
        // disable all models
        // enable random 1
        // assign material to random 1

        foreach (var skin in Skins)
        {
            skin.gameObject.SetActive(false);
        }

        int SkinIndex = Random.Range(0, Skins.Length);

        Skins[SkinIndex].gameObject.SetActive(true);
        Skins[SkinIndex].material = Materials[Random.Range(0, Materials.Length)];

        moveSpeed = Random.Range(3, 7);
        navAgent.speed = moveSpeed;

    }

    private int currentTarget = 0;

    private void Start()
    {
        Randomize();
        foreach (Transform point in Path.GetComponentsInChildren<Transform>())
        {
            Waypoints.Add(point.position);
        }

        navAgent.SetDestination(Waypoints[currentTarget]);

    }

    private void LateUpdate()
    {

        if (Moving)
        {
            RaycastHit hit;

            Vector3 p1 = transform.position;
            float distanceToObstacle = 0;

            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.
            if (Physics.SphereCast(p1, 3, transform.forward, out hit, 3))
            {
                if (hit.transform.CompareTag("NPC") || hit.transform.CompareTag("Player") || hit.transform.CompareTag("Car"))
                {
                    
                    //Debug.Log(hit.transform.name);
                    if (Honk != null)
                    {
                        Honk(hit.transform.gameObject, this.transform);
                    }
                    StartCoroutine(StopHonkAndWait());
                    


                }
            }
        
        

            if (navAgent.remainingDistance < 1)
            {
                currentTarget += 1;
                if (currentTarget > Waypoints.Count - 1)
                {
                    currentTarget = 0;
                }
            
                navAgent.SetDestination(Waypoints[currentTarget]);

            }
        }
        
        
        
    }

    IEnumerator StopHonkAndWait()
    {
        navAgent.isStopped = true;
        Moving = false;
        //honk
        Debug.Log("HONK");
        yield return new WaitForSeconds(Random.Range(2, 4));
        Moving = true;
        navAgent.isStopped = false;
    }
}
