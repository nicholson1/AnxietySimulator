using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnTexts : MonoBehaviour
{
    [SerializeField] private string conversationStartNode;

    private DialogueRunner dialogueRunner;
    private bool interactable = true;
    private bool isCurrentConversation = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation()
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
