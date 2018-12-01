using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    Hostage nextHostage;
    public Hostage currentHostage;
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
	}
    public void LoadHostage()
    {
        currentHostage = nextHostage;
        nextHostage = Collectables.GrabAHostage();
    }
}
