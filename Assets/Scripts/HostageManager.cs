using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageManager : MonoBehaviour {

    public Rigidbody2D rb;
    public PlayerManager player;
    public Hostage hostage;

    public float releaseTime = 1f; //Delay until joint is disconnected
    public int power = 45; //Speed at release

    int HP = 0;

    float TimeOut = 30f;

    const float MAX_DISTANCE = 0.15f;

    //Logic variables
    bool isPressed = false;
    bool isConnected = true;
    Vector2 lastPos;

	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerManager>();
        HP = hostage.hp;

        rb.freezeRotation = true;
        rb.angularDrag = hostage.friction;
        rb.drag = hostage.friction;
        rb.mass = hostage.mass;
	}
	
	// Update is called once per frame
	void Update () {
        //Allow player to move hostage
		if(isPressed & isConnected)
        {
            //Check if in allowed distance
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Move to optimal position
            Rigidbody2D hook = player.GetComponent<Rigidbody2D>();
            if (Vector3.Distance(mousePos, hook.position) > MAX_DISTANCE)
                rb.position = hook.position + (mousePos - hook.position).normalized * MAX_DISTANCE;
            else
                rb.position = mousePos;
        }
        else if(isPressed == false & isConnected)
        {
            transform.position = player.transform.position + new Vector3(0.12f,-.12f,0);
        }
        if(TimeOut > 0 & isConnected == false)
        {
            TimeOut -= Time.deltaTime;
        } else if(TimeOut <= 0)
        {
            Destroy(this.gameObject);
        }


        if(isConnected & Input.GetKeyDown(KeyCode.Space))
        {
            rb.isKinematic = false;
            GetComponent<SpringJoint2D>().enabled = false;
            GameObject.FindObjectOfType<PlayerManager>().hasHostage = false;
            isConnected = false;
        }
        if(isConnected & Input.GetMouseButton(0))
        {
            isPressed = true;
            rb.isKinematic = true;
        }
        else if(isConnected & Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            isConnected = false;
            StartCoroutine(Release());
        }
	}

    public void ComputeDeath()
    {
        if(isConnected == false & --HP <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        if (isConnected)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }
    }
    

    IEnumerator Release()
    {
        GetComponent<SpriteRenderer>().sprite = hostage.getSprite();
        yield return new WaitForSeconds(releaseTime*0.15f);

        rb.freezeRotation = false;
        gameObject.layer = 2;
        rb.isKinematic = false;
        GetComponent<SpringJoint2D>().enabled = false;
        rb.AddForce((player.transform.position - transform.position).normalized * power * -1);
        GameObject.FindObjectOfType<PlayerManager>().hasHostage = false;
    }

}
