using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public bool hasHostage = false;
    public GameObject hostagePrefab;
    public float spawnTime = 1f;
    public float playerSpeed = 1f;
    bool loadingHostage = false;
    GameManager manager;

    //Animations
    enum AnimationSet {IDLE, MOVING};
    AnimationSet animation = AnimationSet.IDLE;
    int AnimationFrame = 0;
    public Sprite[] movingAnimation;
    public Sprite[] idleAnimation;

    float animationSpeed = 0.15f;
    float tempAnimationTime;

    SpriteRenderer render;


	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
        render = this.GetComponent<SpriteRenderer>();
        tempAnimationTime = animationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if(tempAnimationTime > 0)
        {
            tempAnimationTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Changing frame");
            AnimationFrame++;
            if(animation == AnimationSet.IDLE)
            {
                if (AnimationFrame > idleAnimation.Length - 1)
                    AnimationFrame = 0;

                render.sprite = idleAnimation[AnimationFrame];
            }
            else if (animation == AnimationSet.MOVING)
            {
                if (AnimationFrame > movingAnimation.Length - 1)
                    AnimationFrame = 0;

                render.sprite = movingAnimation[AnimationFrame];
            }
            tempAnimationTime = animationSpeed;
        }


        if (hasHostage == false & loadingHostage == false)
        {
            loadingHostage = true;
            manager.LoadHostage();
            StartCoroutine(SpawnHostage(manager.currentHostage));
        }
	}

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetAxis("Vertical") > 0)
        {
            movement += new Vector3(0, playerSpeed, 0);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            movement -= new Vector3(0, playerSpeed, 0);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            movement += new Vector3(playerSpeed, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            movement -= new Vector3(playerSpeed, 0, 0);
        }
        GetComponent<Rigidbody2D>().velocity = movement;

        if (movement == Vector3.zero)
        {
            animation = AnimationSet.IDLE;
        }
        else
        {
            animation = AnimationSet.MOVING;
        }
    }




    IEnumerator SpawnHostage(Hostage hostage)
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject obj = Instantiate(hostagePrefab);

        //Setup object
        obj.name = hostage.name;
        obj.transform.position = this.transform.position;
        obj.GetComponent<SpriteRenderer>().sprite = hostage.getSprite();
        obj.GetComponent<SpringJoint2D>().distance = 0.25f;
        obj.GetComponent<SpringJoint2D>().connectedAnchor = transform.position;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), obj.GetComponent<Collider2D>());

        //Manage logic variables
        hasHostage = true;
        loadingHostage = false;
    }
}
