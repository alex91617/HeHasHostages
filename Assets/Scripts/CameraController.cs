using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Transform player;
    public Transform target;
    public float MAX_DISTANCE = 2.5f;
    const float CAMERA_Z = -10f;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player;
	}
	
	void Update () {
        Vector3 pos = Vector3.zero;
        //Check if in allowed distance
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Move to optimal position
        if (Vector3.Distance(mousePos, target.position) > MAX_DISTANCE)
            pos = target.position + (mousePos - target.position).normalized * MAX_DISTANCE;
        else
            pos = mousePos;

        pos.z = CAMERA_Z;
        transform.position = pos;
    }
}

//Check if in allowed distance
//Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//Move to optimal position
//Rigidbody2D hook = player.GetComponent<Rigidbody2D>();
  //          if (Vector3.Distance(mousePos, hook.position) > MAX_DISTANCE)
    //            rb.position = hook.position + (mousePos - hook.position).normalized* MAX_DISTANCE;
