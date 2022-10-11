using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDarkOneSizeController : MonoBehaviour
{
    [SerializeField] private GameObject model;
    private void Start()
    {
        GameManager.AnxietyChanged += AnxietyHasChanged;
    }

    private void OnDestroy()
    {
        GameManager.AnxietyChanged -= AnxietyHasChanged;
    }

    private Vector3 targetScale;
    private void AnxietyHasChanged(int anxietyLevel)
    {
        float scale = ((anxietyLevel * 3.0f) / 100) + .25f;

        if (scale < .25f)
        {
            scale = .25f;
        }
        targetScale = new Vector3(scale, scale, scale);
        changeSize = true;
    }

    private bool changeSize = false;
    private void Update()
    {
        if (changeSize)
        {
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, targetScale, Time.deltaTime);

            if (Vector3.Distance(model.transform.localScale,targetScale) < .05f)
            {
                changeSize = false;
            }
        }
    }
}
