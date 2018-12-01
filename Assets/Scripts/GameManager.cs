using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    Hostage nextHostage;
    public Transform NextHostageDisplay;
	// Use this for initialization
	void Start () {
        Collectables.LoadCollectables();
        nextHostage = Collectables.GrabAHostage();
	}
	
	// Update is called once per frame
	void Update () {
        NextHostageDisplay.Find("Text").GetComponent<Text>().text = nextHostage.name;
        NextHostageDisplay.Find("Image").GetComponent<Image>().sprite = nextHostage.getSprite();
        if(Input.GetMouseButtonDown(0))
          {
            nextHostage = Collectables.GrabAHostage();
        }
	}
}
