using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    public bool isUber = false;
    public bool activated = false;
    
    [SerializeField] private Material[] Materials;
    [SerializeField] private MeshRenderer[] Skins;
    [SerializeField] private NavMeshAgent navAgent;

    public Transform CarryPos;
    public YarnInteract startEndRideshareConvo;
    

    private SoundManager _soundManager;
    private AudioSource CarAudioSource;
    private AudioClip myHonk;

    public Transform Path;

    private List<Vector3> Waypoints = new List<Vector3>();

    private bool Moving = true;

    private float moveSpeed;
    
    public static event Action<GameObject, Transform> Honk;

    private void Awake()
    {
        YarnInteract.EndConvo += SetEndConvo;
    }

    private void OnDestroy()
    {
        YarnInteract.EndConvo -= SetEndConvo;
    }

    public void Initialize()
    {
        foreach (Transform point in Path.GetComponentsInChildren<Transform>())
        {
            Waypoints.Add(point.position);
        }
        currentTarget = 0;
        navAgent.SetDestination(Waypoints[currentTarget]);
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

        if (_soundManager != null)
        {
            myHonk = _soundManager.GetRandomHonk();
        }




    }

    private int currentTarget = 0;

    private void Start()
    {
        
        _soundManager = FindObjectOfType<SoundManager>();
        CarAudioSource = GetComponent<AudioSource>();
        Initialize();

        
        
        

    }


    private bool waitingForConvoEnd = false;
    private int honkAtCarCounter = 0;
    private GameObject Honktarget;
    private float movingTime;
    private void LateUpdate()
    {
        if (isUber)
        {
            if (activated)
            {
                navAgent.isStopped = false;
                player.transform.position = CarryPos.position;
                
            }
            else
            {
                navAgent.isStopped = true;
            }
        }
        

        if (Moving)
        {
            movingTime += Time.deltaTime;
            if (movingTime > .5f)
            {
                Honktarget = null;
            }
            RaycastHit hit;

            Vector3 p1 = transform.position;
            //float distanceToObstacle = 0;

            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.
            if (Physics.SphereCast(p1, 2f, transform.forward, out hit, 3))
            {
                if (hit.transform.CompareTag("NPC") || hit.transform.CompareTag("Player") || hit.transform.CompareTag("Car"))
                {
                    
                    //Debug.Log(hit.transform.name);
                    if (Honk != null)
                    {
                        Honk(hit.transform.gameObject, this.transform);
                    }

                    if (honkAtCarCounter < 2)
                    {
                        StartCoroutine(StopHonkAndWait());
                    }
                    

                    if (hit.transform.CompareTag("Car"))
                    {
                        if (hit.transform.gameObject != Honktarget)
                        {
                            Honktarget = hit.transform.gameObject;
                        }
                        else
                        {
                            honkAtCarCounter += 1;
                        }
                    }
                    else
                    {
                        honkAtCarCounter = 0;
                    }
                    


                }
            }
        
        

            if (Vector3.Distance(this.transform.position,Waypoints[currentTarget] ) < 1)
            {
                //Debug.Log("Go to next point");
                currentTarget += 1;
                if (currentTarget > Waypoints.Count - 1)
                {
                    
                    if (isUber)
                    {
                        currentTarget -= 1;

                        if (waitingForConvoEnd)
                        {
                            return;
                        }
                        else
                        {
                            //startEndRideshareConvo.StartConversation();
                            DropPlayer();
                            waitingForConvoEnd = true;

                        }
                       
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                    
                    
                }
            
                navAgent.SetDestination(Waypoints[currentTarget]);

            }
        }
        
        
        
    }

    private void HonkSounds()
    {
        CarAudioSource.clip = myHonk;
        CarAudioSource.volume = .25f;
        CarAudioSource.Play();
    }

    IEnumerator StopHonkAndWait()
    {
        HonkSounds();

        navAgent.isStopped = true;
        Moving = false;
        //honk
        Debug.Log("HONK");
        yield return new WaitForSeconds(Random.Range(2, 4));
        Moving = true;
        navAgent.isStopped = false;
        movingTime = 0;
    }

    public GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (isUber)
        {
            if (other.CompareTag("Player"))
            {
                if (!activated)
                {
                    
                    activated = true;
                    player = other.gameObject;
                    player.SetActive(false);
                    navAgent.speed = 4;
                    
                    
                    GetComponentInChildren<YarnInteract>().StartConversation();
                    


                }
            }
        }
    }

    public void DropPlayer()
    {
        player.transform.position = transform.localPosition + Vector3.right * 3;
        player.SetActive(true);
        StartCoroutine(waitThenLeave());
        player.GetComponent<PlayerMovement>().FixAnimationBug();
        //startEndRideshareConvo.EndConversation();
        player = null;
        isUber = false;
        currentTarget = 0;
        waitingForConvoEnd = false;
        navAgent.SetDestination(Waypoints[currentTarget]);


    }

    IEnumerator waitThenLeave()
    {
        navAgent.isStopped = true;
        yield return new WaitForSeconds(5);
        navAgent.isStopped = false;
    }

    private void SetEndConvo(string convo)
    {
        switch(convo)
        {
            case "Rideshare":
                //DropPlayer();
                break;
        }
    }
}
