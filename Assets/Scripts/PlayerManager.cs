using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour {
    public bool hasHostage = false;
    public GameObject hostagePrefab;
    public float spawnTime = 1f;
    public float playerSpeed = 1f;
    bool loadingHostage = false;
    GameManager manager;

    public int HP = 4;
    public int MaxHP = 4;

    List<Light> lights = new List<Light>();

    public int money = 0;

    //Animations
    enum AnimationSet {IDLE, MOVING};
    AnimationSet animation = AnimationSet.IDLE;
    int AnimationFrame = 0;
    public Sprite[] movingAnimation;
    public Sprite[] idleAnimation;
    public Sprite[] forwardMovingAnimation;
    public Sprite[] forwardIdleAnimation;

    float animationSpeed = 0.15f;
    float tempAnimationTime;

    bool facingForward = false;

    SpriteRenderer render;


	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
        render = this.GetComponent<SpriteRenderer>();
        tempAnimationTime = animationSpeed;

        Light[] li = GameObject.FindObjectsOfType<Light>();
        foreach(Light l in li)
        {
            lights.Add(l);
        }
	}
	
	// Update is called once per frame
	void Update () {
        manager.DisplayMoney(money);
        if(tempAnimationTime > 0)
        {
            tempAnimationTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Changing frame");
            AnimationFrame++;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (animation == AnimationSet.IDLE)
            {
                if (AnimationFrame > idleAnimation.Length - 1)
                    AnimationFrame = 0;

                if (mousePos.y > transform.position.y)
                    render.sprite = idleAnimation[AnimationFrame];
                else
                    render.sprite = forwardIdleAnimation[AnimationFrame];
            }
            else if (animation == AnimationSet.MOVING)
            {
                if (AnimationFrame > movingAnimation.Length - 1)
                    AnimationFrame = 0;

                if (mousePos.y > transform.position.y)
                    render.sprite = movingAnimation[AnimationFrame];
                else
                    render.sprite = forwardMovingAnimation[AnimationFrame];
            }
            tempAnimationTime = animationSpeed;
        }


        if (hasHostage == false & loadingHostage == false)
        {
            loadingHostage = true;
            manager.LoadHostage();
            StartCoroutine(SpawnHostage(manager.currentHostage));
        }
        manager.UpdateHealthbar(HP, MaxHP);

        ManageLights();
	}

    void ManageLights()
    {
        foreach(Light light in lights)
        {
            bool on = Vector2.Distance(transform.position, light.transform.position) < 7 || light.type == LightType.Directional;
            if (light.gameObject.activeInHierarchy != on)
                light.gameObject.SetActive(on);
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

    public bool IsInfoOpen()
    {
        return manager.hostageInfoDisplay.activeInHierarchy;
    }



    IEnumerator SpawnHostage(Hostage hostage)
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject obj = Instantiate(hostagePrefab);

        //Setup object
        obj.name = hostage.name;
        obj.transform.position = this.transform.position;
        obj.GetComponent<HostageManager>().hostage= hostage;
        obj.GetComponent<SpringJoint2D>().distance = 0.25f;
        obj.GetComponent<SpringJoint2D>().connectedAnchor = transform.position;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), obj.GetComponent<Collider2D>());

        //Manage logic variables
        hasHostage = true;
        loadingHostage = false;
    }
} 
