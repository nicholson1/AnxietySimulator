using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int AnxietyLevel = 33;
    
    public static event Action<int> AnxietyChanged;

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
