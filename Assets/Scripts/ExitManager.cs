using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour {

    PlayerManager player;
    GameManager manager;

    bool PlayAnimation = false;

    int AnimationFrame = 0;
    public Sprite[] exitAnimation;

    float animationSpeed = 0.15f;
    float tempAnimationTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayAnimation = true;
            StartCoroutine(VictoryGUI());
        }
    }

    IEnumerator VictoryGUI()
    {
        yield return new WaitForSeconds(0.15f * (exitAnimation.Length) * 1.05f);
        int money = PlayerPrefs.GetInt("money", 0);
        money += player.money;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save();
        manager.VICTORY();
    }


    private void Update()
    {
        if (PlayAnimation)
        {
            if (tempAnimationTime > 0)
            {
                tempAnimationTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Changing frame");
                AnimationFrame++;
                if (AnimationFrame > exitAnimation.Length - 1)
                    PlayAnimation = false;

                GetComponent<SpriteRenderer>().sprite = exitAnimation[AnimationFrame];
                tempAnimationTime = animationSpeed;
            }
        }
    }


}
