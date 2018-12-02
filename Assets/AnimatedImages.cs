using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedImages : MonoBehaviour {

    public GameObject[] frames;
    int frame = 0;
    public float animationSpeed = 0.5f;
    float timeTillNextAnimation;

    public float timeUntilDeath = 5f;


	// Use this for initialization
	void Start () {
        timeTillNextAnimation = animationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeUntilDeath > 0)
        {
            if (timeTillNextAnimation < 0)
            {
                frame++;
                if (frame > frames.Length - 1)
                {
                    frame = 0;
                }
                foreach (GameObject obj in frames)
                {
                    obj.SetActive(false);
                }
                frames[frame].SetActive(true);

                timeTillNextAnimation = animationSpeed;
            }
            timeUntilDeath -= Time.deltaTime;
            timeTillNextAnimation -= Time.deltaTime;
        }
        else
        {
            foreach(GameObject obj in frames)
            {
                obj.SetActive(false);
            }
            this.enabled = false;
        }
	}
}
