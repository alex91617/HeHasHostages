using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    Fader fader;

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
        fader.FadeIn = false;
        StartCoroutine(LoadGame());
    }

    public void LoadCredits(string creator)
    {
        Application.OpenURL("https://ldjam.com/users/" + creator + "/");
    }

    //Go between main menu and settings
    public void Settings()
    {
        transform.Find("Default").gameObject.SetActive(false);
        transform.Find("Settings").gameObject.SetActive(true);
    }
    public void Back()
    {
        PlayerPrefs.Save();
        transform.Find("Settings").gameObject.SetActive(false);
        transform.Find("Default").gameObject.SetActive(true);
    }
    void Start()
    {
        fader = FindObjectOfType<Fader>();
        
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(fader.fadeTime*4);
        SceneManager.LoadScene(1);
    }
}
