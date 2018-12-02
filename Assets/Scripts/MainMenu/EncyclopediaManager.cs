using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaManager : MonoBehaviour {

    public GameObject unlockTemplate;

    public void DoUnlocks()
    {
        Transform unlocked = transform.Find("Unlocked").Find("Viewport").Find("Content");
        for (int i = unlocked.childCount; i > 0 ; i-- )
        {
            Destroy(unlocked.GetChild(i));
        }
        Collectables.LoadCollectables();
        foreach(Hostage hostage in Collectables.UnlockedHostages)
        {
            GameObject gobj = Instantiate(unlockTemplate);
            gobj.transform.SetParent(unlocked);
            gobj.transform.Find("Image").GetComponent<Image>().sprite = hostage.getSprite();
            gobj.transform.Find("Name").GetComponent<Text>().text = hostage.name;
            gobj.GetComponent<Button>().onClick.AddListener(() => DisplayHostage(hostage));
        }

    }
    void DisplayHostage(Hostage hostage)
    {
        Transform info = transform.Find("Info");
        info.Find("Name").GetComponent<Text>().text = hostage.name;
        info.Find("Image").GetComponent<Image>().sprite = hostage.getSprite();
        info.Find("Backstory").GetComponent<Text>().text = hostage.background;
        info.gameObject.SetActive(true);
    }
}
