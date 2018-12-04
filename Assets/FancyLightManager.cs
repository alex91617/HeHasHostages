using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyLightManager : MonoBehaviour {
    public float ChangeSpeed = 1f;
    float tempNum;
    public Sprite blue;
    public Sprite red;
    bool isRed = true;

	// Use this for initialization
	void Start () {
        tempNum = ChangeSpeed;
	}
	
	// Update is called once per frame
	void Update () {


        tempNum -= Time.deltaTime;
        if(tempNum <= 0)
        {
            isRed = !isRed;
            tempNum = ChangeSpeed;

            if(isRed)
            {
                GetComponent<Light>().color = Color.red;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = red;
            }
            else
            {
                GetComponent<Light>().color = Color.blue;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = blue;
            }
        }
	}
}
