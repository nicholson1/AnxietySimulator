using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class ComentsController : MonoBehaviour
{

    private int _anxietyLevel = 0;
    private bool _inConvo;
    private float TimeTillNextComment = 15f;
    
    

    [SerializeField] public GameObject Button1;
    [SerializeField] public GameObject Button2;
    
    [SerializeField] public GameObject MyCanvas;
    private void Awake()
    {
        GameManager.AnxietyChanged += AnxietyHasChanged;
        Car.Honk += HonkedAt;
        YarnInteract.InConvo += Convo;
    }

    private void OnDestroy()
    {
        GameManager.AnxietyChanged -= AnxietyHasChanged;
        Car.Honk -= HonkedAt;
        YarnInteract.InConvo += Convo;
    }
    
    private void AnxietyHasChanged(int anxietyLevel)
    {
        _anxietyLevel = anxietyLevel;
        TimeTillNextComment = (51 - (_anxietyLevel) / 2) /2f;
        if (timeCounter > TimeTillNextComment)
        {
            timeCounter = TimeTillNextComment;
        }

    }

    private void Start()
    {
        timeCounter = TimeTillNextComment;
    }

    private float timeCounter;
    private void Update()
    {
        if (!_inConvo)
        {
            timeCounter -= Time.deltaTime;

            if (timeCounter < 0)
            {
                CreateNewComment();
                timeCounter = TimeTillNextComment;
            
            }
        }
        
    }

    private bool isButton1 = true;
    public int mod = 0;
    private void CreateNewComment(bool HonkedAt = false)
    {
        //alternate bewtween Left and right
        GameObject temp;

        if (isButton1)
        {
            temp = Instantiate(Button1, MyCanvas.transform); 
        }
        else
        {
            temp = Instantiate(Button2, MyCanvas.transform);
        }

        temp.GetComponent<PopupComment>().fadeTime = Random.Range(5,12);

        if (HonkedAt == true)
        {
            temp.GetComponent<PopupComment>().HonkedAt = true;
        }
        // adjust position

       
        mod = temp.transform.parent.childCount % 6 + 1;
        
        
        //how many sibling;
        if (temp.transform.parent.childCount > 2)
        {
            if (temp.transform.parent.childCount % 2 == 0)
            {
                temp.GetComponent<RectTransform>().localPosition +=new Vector3(Random.Range(-40, 40), mod * 40, 0);
            }
            else
            {
                temp.GetComponent<RectTransform>().localPosition +=new Vector3(Random.Range(-40, 40), mod * -40, 0);
            }
        }
        
        
        //temp.GetComponent<RectTransform>().localPosition +=new Vector3(Random.Range(-40, 40), Random.Range(-20, 20), 0);
        float ScaleAdjust = Random.Range(-.1f, .2f);
        temp.GetComponent<RectTransform>().localScale += new Vector3(ScaleAdjust, ScaleAdjust, ScaleAdjust);
        isButton1 = !isButton1;
        

    }

    private void HonkedAt(GameObject player, Transform car)
    {
        if (player.CompareTag("Player"))
        {
            CreateNewComment(true);
            

        }
    }

    private void Convo(bool inconvo)
    {
        _inConvo = inconvo;
    }
    
}
