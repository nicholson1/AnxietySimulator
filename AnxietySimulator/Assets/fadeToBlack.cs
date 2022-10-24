using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeToBlack : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 5;

    public GameObject ActivateObj;
    
    public void waitThenFadeToBlack(float seconds)
    {
        StartCoroutine(waitThenFade(seconds));
    }

    private IEnumerator waitThenFade(float s)
    {
        yield return new WaitForSeconds(s);
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Color initialColor = image.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        if (image.color.a >= 1 )
        {
            ActivateObj.SetActive(true);

            ChangeText ct = ActivateObj.GetComponent<ChangeText>();
            if (ct != null)
            {
                ct.ChangeTheText(GameObject.FindObjectOfType<GameManager>().AnxietyLevel);
            }
            else
            {
                FindObjectOfType<SoundManager>().PingPhone();

            }
        }
    }
}
