using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageManager : MonoBehaviour {

    public Rigidbody2D rb;
    public float releaseTime = 1f;
    public int power = 4;
    bool isPressed = false;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
	}

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }
    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime*0.15f);

        GetComponent<SpringJoint2D>().enabled = false;
        for (int i = 0; i < power; i++)
        {
            rb.AddForce(rb.velocity);
        }
    }

}
