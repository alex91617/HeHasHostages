using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    public bool FadeIn = true;
    public float fadeTime = 1f;
    public bool update = true;
    int opacity = 0;
    float tempVal = 0f;
    bool fade = true;

    public bool delay = false;
    public float delayLength = 1.5f;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        opacity = (int)(image.color.a * 255);
        if(delay)
        {
            fade = false;
            StartCoroutine(doDelay());
        }
    }

    void Update()
    {
        if (fade)
        {
            if (update)
            {
                tempVal = fadeTime / 255;
                update = false;
            }
            tempVal -= Time.deltaTime;
            if (tempVal <= 0)
            {
                if (FadeIn)
                {
                    opacity = Mathf.Clamp(opacity + 1, 0, 255);
                }
                else
                {
                    opacity = Mathf.Clamp(opacity - 1, 0, 255);
                }
                tempVal = fadeTime / 255;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity / 255f);
        }
    }
    IEnumerator doDelay()
    {
        yield return new WaitForSeconds(delayLength);
        fade = true;
    }
}
