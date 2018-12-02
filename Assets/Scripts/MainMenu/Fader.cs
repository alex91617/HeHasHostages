using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    public bool FadeIn = true;
    public float fadeTime = 1f;
    public bool update = true;


	void Update () {
        if (update)
        {
            if (FadeIn)
            {
                GetComponent<Image>().CrossFadeColor(Color.clear, fadeTime, true, true);
            }
            else
            {
                GetComponent<Image>().CrossFadeColor(Color.black, fadeTime, false, true);
            }
            update = false;
        }
	}
}
