using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteract : MonoBehaviour
{
    [SerializeField] private string conversationStartNode;

    public DialogueRunner dialogueRunner;
    public bool interactable = true;
    public bool isCurrentConversation = false;

    public Transform playerProximity;
    public float activationDist = 2f;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);

        playerProximity = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(playerProximity.position, transform.position);
        if (dist <= activationDist && Input.GetKeyUp(KeyCode.E)) 
        {
           if (interactable && !dialogueRunner.IsDialogueRunning)
           {
               StartConversation();
           }
        }
    }

    /*private void OnTriggerEnter(Collider other)
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
*/
    private void StartConversation()
    {
        //Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            //Debug.Log($"Started conversation with {name}.");
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
