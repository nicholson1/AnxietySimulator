using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public int AnxietyLevel = 33;
    public LookAt pointer;
    public static event Action<int> AnxietyChanged;

    [SerializeField] private GameObject[] thingsToActive;

    public GameObject Uber;
    public Transform UberStartPos;

    private GameObject Player;
    private GameObject DarkSelf;

    public DialogueRunner DialogueRunner;

    private void Awake()
    {
        Car.Honk += HonkedAt;
        PopupComment.PopUpClicked += IncreaseAnxiety;
        YarnInteract.EndConvo += SetEndConvo;
        YarnInteract.StartConvo += SetStartConvo;
    }
    
    private void OnDestroy()
    {
        Car.Honk -= HonkedAt;
        PopupComment.PopUpClicked -= IncreaseAnxiety;
        YarnInteract.EndConvo -= SetEndConvo;
        YarnInteract.StartConvo -= SetStartConvo;
    }
    
    private void HonkedAt(GameObject player, Transform car)
    {
        if (player.CompareTag("Player"))
        {
            AdjustAnxietyLevel(1);
            //Debug.Log("We Ran");
        }
    }

    private void IncreaseAnxiety()
    {
        AdjustAnxietyLevel(1);
    }

    private void Start()
    {
        AnxietyChanged(AnxietyLevel);
        Player = FindObjectOfType<PlayerMovement>().gameObject;
        DarkSelf = FindObjectOfType<TheDarkOneMovement>().gameObject;
        
        DisableConvos();
    }
    
    
    private void DisableConvos()
    {
        foreach (GameObject i in thingsToActive )
        {
            i.SetActive(false);
        }
    }

    [YarnCommand("ChangeAnxiety")]
    public void AdjustAnxietyLevel(int changeAmount)
    {
        AnxietyLevel += changeAmount;
        Debug.Log("we changes our shit");
        // clamp anxiety levels between 0 and 100;
        if (AnxietyLevel < 0)
        {
            AnxietyLevel = 0;
        }

        if (AnxietyLevel > 100)
        {
            AnxietyLevel = 100;
        }
        DialogueRunner.VariableStorage.SetValue("$globalAnxiety", AnxietyLevel);
        
        AnxietyChanged(AnxietyLevel);
    }

    IEnumerator WaitThenActivatePhone()
    {
        yield return new WaitForSeconds(20);
        thingsToActive[7].SetActive(true);
    }


    private void SetEndConvo(string scene)
    {
        switch (scene)
        {
            case "Start":
                StartCoroutine(WaitThenActivatePhone());// activate the phone
                pointer.SetTarget(thingsToActive[8]);
                break;
            case "HardwareStore":
                Player.SetActive(true);
                DarkSelf.SetActive(true);
                thingsToActive[0].SetActive(true);
                StartCoroutine(WaitThenActivatePhone());// activate the phone
                Player.GetComponent<PlayerMovement>().FixAnimationBug();
                pointer.SetTarget(thingsToActive[0]);

                break;
            case "Lunch":
                Player.SetActive(true);
                DarkSelf.SetActive(true);
                thingsToActive[1].SetActive(true);
                Player.GetComponent<PlayerMovement>().FixAnimationBug();
                pointer.SetTarget(thingsToActive[1]);

                break;
                
            case "DarkSelfConfrontation":
                thingsToActive[2].SetActive(true);
                StartCoroutine(WaitThenActivatePhone());// activate the phone
                pointer.SetTarget(thingsToActive[2]);


                break;
            case "GroceryStore":
                Player.SetActive(true);
                DarkSelf.SetActive(true);
                thingsToActive[3].SetActive(true);
                Uber.transform.position = UberStartPos.position;
                Player.GetComponent<PlayerMovement>().FixAnimationBug();
                pointer.SetTarget(thingsToActive[3]);

                break;
            case "Rideshare":
                //thingsToActive[4].SetActive(true);
                thingsToActive[6].SetActive(true);
                pointer.SetTarget(thingsToActive[6]);
                break;
            case "RideshareEnd":
                thingsToActive[6].SetActive(true);
                pointer.SetTarget(thingsToActive[6]);
                
                Debug.Log("Activate mom Call");
                Debug.Log(thingsToActive[6].name);

                break;



        }
    }

    private void SetStartConvo(string scene)
    {
        switch (scene)
        {
            case "Start":
                thingsToActive[8].SetActive(true);
                break;
            case "GroceryStore":
                Player.SetActive(false);
                DarkSelf.SetActive(false);
                break;
            
            case "Lunch":
                Player.SetActive(false);
                DarkSelf.SetActive(false);
                break;
            case "HardwareStore":
                Player.SetActive(false);
                DarkSelf.SetActive(false);
                break;
            case "DarkSelfConfrontation":
                pointer.ClearTarget();
                break;
            //case 
        }
    }

    
}
