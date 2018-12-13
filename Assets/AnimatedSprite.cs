using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{

    public Sprite[] frames;
    int frame = 0;
    public float animationSpeed = 0.5f;
    float timeTillNextAnimation;


    // Use this for initialization
    void Start()
    {
        timeTillNextAnimation = animationSpeed;
    }

    // Update is called once per frame
    void Update()
    {

            if (timeTillNextAnimation < 0)
            {
                frame++;
                if (frame > frames.Length - 1)
                {
                    frame = 0;
                }

            GetComponent<SpriteRenderer>().sprite = frames[frame];

                timeTillNextAnimation = animationSpeed;
            }
            timeTillNextAnimation -= Time.deltaTime;
        }
}
