using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    Hostage nextHostage;

    public GameObject victoryObject;
    public GameObject gameOver;
    public GameObject unlockInfoDisplay;
    public GameObject hostageInfoDisplay;
    public Hostage currentHostage;
    public Transform NextHostageDisplay;
    public Text money;

    public Slider healthbar;

	// Use this for initialization
	void Start () {
        Collectables.LoadCollectables();
        nextHostage = Collectables.GrabAHostage(true);

        GetComponent<AudioSource>().Play();
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
        if(current <= 0)
        {
            GAMEOVER();
        }
    }
    public void UnlockNewHostage(Hostage hostage)
    {
        CloseHostageDisplay();
        unlockInfoDisplay.transform.Find("Name").GetComponent<Text>().text = hostage.name;
        unlockInfoDisplay.transform.Find("Image").GetComponent<Image>().sprite = hostage.getSprite();
        unlockInfoDisplay.transform.Find("Backstory").GetComponent<Text>().text = hostage.background;
        Collectables.UnlockedHostages.Add(hostage);
        Collectables.SaveCollectables();
        unlockInfoDisplay.SetActive(true);
    }
    public void CloseUnlockDisplay()
    {
        unlockInfoDisplay.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        Debug.Log("Moving to main menu");
        SceneManager.LoadScene(0);
    }
    public void GAMEOVER()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        gameOver.SetActive(true);
    }
    public void VICTORY()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        victoryObject.transform.Find("Money").GetComponent<Text>().text = money.text;
        victoryObject.SetActive(true);
    }
}
