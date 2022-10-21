using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    public GameObject startScene;

    // Start is called before the first frame update
    void Start()
    {
        startScene.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        Debug.Log("Play Button Pressed");
        startScene.SetActive(false);
    }
}
