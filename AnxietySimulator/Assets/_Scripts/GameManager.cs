using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int AnxietyLevel = 33;
    
    public static event Action<int> AnxietyChanged;

    private void Awake()
    {
        Car.Honk += HonkedAt;
        PopupComment.PopUpClicked += IncreaseAnxiety;
    }
    
    private void OnDestroy()
    {
        Car.Honk -= HonkedAt;
        PopupComment.PopUpClicked -= IncreaseAnxiety;
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
    }

    public void AdjustAnxietyLevel(int changeAmount)
    {
        AnxietyLevel += changeAmount;

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
    
}
