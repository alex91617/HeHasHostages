using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    Hostage nextHostage;

    public GameObject hostageInfoDisplay;
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
        hostageInfoDisplay.transform.Find("Name").GetComponent<Text>().text = nextHostage.name;
        hostageInfoDisplay.transform.Find("Image").GetComponent<Image>().sprite = nextHostage.getSprite();
        hostageInfoDisplay.transform.Find("Backstory").GetComponent<Text>().text = nextHostage.background;
    }
    public void LoadHostage()
    {
        currentHostage = nextHostage;
        nextHostage = Collectables.GrabAHostage(true);
    }

    public void OpenHostageDisplay()
    {
        hostageInfoDisplay.SetActive(true);
    }

    public void CloseHostageDisplay()
    {
        hostageInfoDisplay.SetActive(false);
    }
}
