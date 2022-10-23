using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DisableSelf : MonoBehaviour
{
    public Button myButton;
    public DialogueRunner DR;
    public void DisableMySelf()
    {
        this.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (DR.IsDialogueRunning)
        {
            myButton.interactable = false;
        }
        else
        {
            myButton.interactable = true;
        }
    }
}
