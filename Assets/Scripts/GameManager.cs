using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    Hostage nextHostage;

    public GameObject pauseMenu;
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
       
	}
	
	// Update is called once per frame
	void Update () {
        NextHostageDisplay.Find("Text").GetComponent<Text>().text = currentHostage.name;
        NextHostageDisplay.Find("Image").GetComponent<Image>().sprite = currentHostage.getSprite();
        hostageInfoDisplay.transform.Find("Name").GetComponent<Text>().text = currentHostage.name;
        hostageInfoDisplay.transform.Find("Image").GetComponent<Image>().sprite = currentHostage.getSprite();
        hostageInfoDisplay.transform.Find("Backstory").GetComponent<Text>().text = currentHostage.background;
        hostageInfoDisplay.transform.Find("Mass").GetComponent<Text>().text = "Mass: " + currentHostage.mass.ToString();
        hostageInfoDisplay.transform.Find("Slip").GetComponent<Text>().text = "Slip: " + ((1-currentHostage.friction) * 10).ToString();
        for(int i = 0; i < hostageInfoDisplay.transform.Find("HP").childCount; i++)
        {
            bool active = int.Parse(hostageInfoDisplay.transform.Find("HP").GetChild(i).name) <= currentHostage.hp;
            hostageInfoDisplay.transform.Find("HP").GetChild(i).gameObject.SetActive(active);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu(!pauseMenu.activeSelf);
        }
    }
    public void DisplayMoney(int value)
    {
        money.text = "$" + value.ToString();
    }

    public void ExitApplication()
    {
        Application.Quit();
        if (Application.isEditor)
        {
            Debug.LogWarning("Unable to exit game in editor!");
        }
    }

    public void LoadHostage()
    {
        currentHostage = nextHostage;
        nextHostage = Collectables.GrabAHostage(true);
    }
    public void OpenPauseMenu(bool open = true)
    {
        pauseMenu.SetActive(open);
        UnpauseGame(!open);
    }

    public void OpenHostageDisplay()
    {
        hostageInfoDisplay.SetActive(true);
        UnpauseGame(false);
    }

    public void CloseHostageDisplay()
    {
        hostageInfoDisplay.SetActive(false);
        UnpauseGame();
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
        UnpauseGame(false);
        unlockInfoDisplay.SetActive(true);
    }
    public void UnpauseGame(bool unpause = true)
    {
        bool good = false;
        if (unpause & pauseMenu.activeSelf == false & hostageInfoDisplay.activeSelf == false & unlockInfoDisplay.activeSelf == false)
        {
            good = true;
        }
        else if (unpause == false) 
        {
            good = true;
        }

        if(good)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().enabled = unpause;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach (Shooter shooter in GameObject.FindObjectsOfType<Shooter>())
            {
                shooter.enabled = unpause;
            }
            foreach (Pathfinding.AIPath path in GameObject.FindObjectsOfType<Pathfinding.AIPath>())
            {
                path.enabled = unpause;
            }
            foreach(HostageManager hostage in GameObject.FindObjectsOfType<HostageManager>())
            {
                hostage.enabled = unpause;
            }
        }
    }

    public void CloseUnlockDisplay()
    {
        unlockInfoDisplay.SetActive(false);
        UnpauseGame();
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
