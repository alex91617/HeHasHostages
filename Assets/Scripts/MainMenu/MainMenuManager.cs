using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    Fader fader;
    public Texture2D cursor;


    

	public void ExitGame()
    {
        Application.Quit();
        if (Application.isEditor)
        {
            Debug.LogWarning("Unable to exit game in editor!");
        }
    }
    public void NewGame()
    {
        fader.FadeIn = true;
        StartCoroutine(LoadGame());
    }

    public void LoadCredits(string creator)
    {
        Application.OpenURL("https://ldjam.com/users/" + creator + "/");
    }

    //Go between main menu and settings
    public void Settings()
    {
        transform.Find("Encyclopedia").gameObject.SetActive(false);
        transform.Find("Default").gameObject.SetActive(false);
        transform.Find("Settings").gameObject.SetActive(true);
    }
    public void Back()
    {
        PlayerPrefs.Save();
        transform.Find("Encyclopedia").gameObject.SetActive(false);
        transform.Find("Settings").gameObject.SetActive(false);
        transform.Find("Default").gameObject.SetActive(true);
    }
    public void Encyclopeida()
    {
        
        transform.Find("Settings").gameObject.SetActive(false);
        transform.Find("Default").gameObject.SetActive(false);
        transform.Find("Encyclopedia").gameObject.SetActive(true);
        transform.Find("Encyclopedia").GetComponent<EncyclopediaManager>().DoUnlocks();
    }
    void Start()
    {
        fader = FindObjectOfType<Fader>();
        Cursor.SetCursor(cursor, new Vector2(1,1), CursorMode.Auto);
        
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
    }
}
