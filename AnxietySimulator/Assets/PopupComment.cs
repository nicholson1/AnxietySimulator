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
    };

    private String[] HonkComments = new[]
    {
        "You’re going to get hit by a car!",
        "You're in the way of that car!",
        "Move they're honking at you!",
        "I wish they would just hit you..."
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
