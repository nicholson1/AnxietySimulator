using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public int AnxietyLevel = 33;
    
    public static event Action<int> AnxietyChanged;

    [SerializeField] private GameObject[] thingsToActive;
    

    private GameObject Player;
    private GameObject DarkSelf;

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
        
        AnxietyChanged(AnxietyLevel);
    }

    IEnumerator WaitThenActivatePhone()
    {
        yield return new WaitForSeconds(20);
        thingsToActive[5].SetActive(true);
    }


    private void SetEndConvo(string scene)
    {
        switch (scene)
        {
            case "Start":
                StartCoroutine(WaitThenActivatePhone());// activate the phone
                break;
            case "HardwareStore":
                Player.SetActive(true);
                DarkSelf.SetActive(true);
                thingsToActive[0].SetActive(true);
                StartCoroutine(WaitThenActivatePhone());// activate the phone
                break;
            case "Lunch":
                Player.SetActive(true);
                DarkSelf.SetActive(true);
                thingsToActive[1].SetActive(true);
                break;
                
            case "DarkSelfConfrontation":
                thingsToActive[2].SetActive(true);
                StartCoroutine(WaitThenActivatePhone());// activate the phone

                break;
            case "GroceryStore":
                Player.SetActive(true);
                DarkSelf.SetActive(true);
                thingsToActive[3].SetActive(true);
                break;
            case "Rideshare":
                thingsToActive[4].SetActive(true);
                break;
            case "RideshareEnd":
                thingsToActive[6].SetActive(true);
                break;



        }
    }

    private void SetStartConvo(string scene)
    {
        switch (scene)
        {
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
            
        }
    }

    
}
