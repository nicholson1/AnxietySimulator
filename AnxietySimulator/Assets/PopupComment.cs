using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PopupComment : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _myText;
    [SerializeField] private Image image;
    public float fadeTime = 10f;
    public bool HonkedAt = false;

    public static event Action PopUpClicked;
    
    private String[] Comments = new[]
    {
        "You left your phone behind.",
        "I’m pretty sure you locked yourself out this morning.",
        "Don’t trip!",
        "They’re staring at you.",
        "Your music's too loud, everyone can hear it.",
        "You’re late.",
        "Is somebody following you?",
        "You forgot your wallet. You can’t pay for anything.",
        "Get out of the way, you’re in the way.",
        "You should just go home.",
        "Are you lost? I think you’re lost.",
        "You’re going the wrong way.",
        "You’re never going to get all of this stuff done.",
        "You’re forgetting something. I don’t know what it is, but you forgot it.",
        "Moving here was a mistake.",
        "You aren’t cut out for grad school.",
        "I bet everyone at the orientation last week thought you were a freak.",
        "Why are you like this?",
        "You walk weirdly and everyone notices it.",
        "You don’t deserve to make any new friends here.",
        "I bet they accepted you to this grad program by mistake.",
        "You look so awkward right now… I just know it.",
        "Stop. Just, in general… stop.",
        "Your mom was right about you.",
        "No wonder your dad barely wants to acknowledge you.",
        "Why can’t you be more like your brother?",
        "That facial expression you’re making is creeping everyone out…",
        "You’re existing wrong. I don’t know how, you just are.",
        "Your body looks gross and your clothes don’t fit right.",
        "Your heart is beating so fast, I bet you’ll get a heart attack!",
        "How does someone like you have friends, let alone a girlfriend?",
        "You’re gonna bump into someone. They’re gonna hate you for it.",
        "Listen to your own heartbeat. Isn’t that unnerving…?",
        "You didn’t sleep enough to get through today…",

    };

    private String[] HonkComments = new[]
    {
        "You’re going to get hit by a car!",
        "You're in the way of that car!",
        "Move they're honking at you!",
        "I wish they would just hit you...",
        "Maybe you deserve to be hit…",
        "Isn’t it embarrassing how you can’t drive?",
        "Today would be easier if you had a license.",
        "I bet that person wanted to hit you.",

    };
    private void Start()
    {
        if (!HonkedAt)
        {
            _myText.text = Comments[Random.Range(0, Comments.Length)];
        }
        else
        {
            _myText.text = HonkComments[Random.Range(0, HonkComments.Length)];
        }
        

        StartCoroutine(Fade());
    }
    
    private IEnumerator Fade()
    {
        
        Color initialColor = image.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, .25f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeTime);
            yield return null;
        }
        
        Destroy(this.gameObject);
    }

    public void ClickOnComment()
    {
        //++ anxiety
        PopUpClicked();
        Destroy(this.gameObject);
    }

    //fade then disable
    // onclick disable + anxiety
    // random text
}
