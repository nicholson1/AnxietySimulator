using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

//cribbed most of this script from one of the Yarn example projects on their site
//the sample project is also in our project for ref
public class YarnInteract : MonoBehaviour
{
    [SerializeField] private string conversationStartNode;

    private DialogueRunner dialogueRunner;
    private bool interactable = true;
    private bool isCurrentConversation = false;

    //private Transform playerProximity;
    //public float activationDist = 2f;

    public bool AllowPlayerToWalk = false;
    public static event Action<String> InConvo;
    public static event Action<String> EndConvo;
    public static event Action<String> StartConvo;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);

        //playerProximity = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //ActivateConvo();
    }

    /*private void ActivateConvo() 
    {
        float dist = Vector3.Distance(playerProximity.position, transform.position);
        if (dist <= activationDist && Input.GetKeyUp(KeyCode.E))
        {
            if (interactable && !dialogueRunner.IsDialogueRunning)
            {
                StartConversation();
            }
        }
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("This happened");
            if (interactable && !dialogueRunner.IsDialogueRunning)
            {
                StartConversation();
            }
        }
    }

    public void StartConversation()
    {
        //Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
        
        InConvo(conversationStartNode);
        StartConvo(conversationStartNode);
        
        
        
        
    }

    public void EndConversation()
    {
        if (isCurrentConversation)
        {
            InConvo(null);
            EndConvo(conversationStartNode);

            isCurrentConversation = false;
            //Debug.Log($"Started conversation with {name}.");
            gameObject.SetActive(false);
        }
    }

    //[YarnCommand("disable")]
    //not yet sure how to implement these
    //but we do need to figure out how to turn off dialogues when we finish them
    public void DisableConversation()
    {
        interactable = false;
    }
}
