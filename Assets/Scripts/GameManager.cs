using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    Hostage nextHostage;




    public GameObject hostageInfoDisplay;
    public Hostage currentHostage;
    public Transform NextHostageDisplay;
    public Text money;

    public Slider healthbar;

	// Use this for initialization
	void Start () {
        Collectables.LoadCollectables();
        nextHostage = Collectables.GrabAHostage();
	}
	
	// Update is called once per frame
	void Update () {
        NextHostageDisplay.Find("Text").GetComponent<Text>().text = currentHostage.name;
        NextHostageDisplay.Find("Image").GetComponent<Image>().sprite = currentHostage.getSprite();
        hostageInfoDisplay.transform.Find("Name").GetComponent<Text>().text = currentHostage.name;
        hostageInfoDisplay.transform.Find("Image").GetComponent<Image>().sprite = currentHostage.getSprite();
        hostageInfoDisplay.transform.Find("Backstory").GetComponent<Text>().text = currentHostage.background;
    }
    public void DisplayMoney(int value)
    {
        money.text = "$" + value.ToString();
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
    public void UpdateHealthbar(int current, int max)
    {
        healthbar.maxValue = max;
        healthbar.value = Mathf.Clamp(current,0,max);
    }
}
