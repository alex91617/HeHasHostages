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
        info.transform.Find("Mass").GetComponent<Text>().text = "Mass: " + hostage.mass.ToString();
        info.transform.Find("Slip").GetComponent<Text>().text = "Slip: " + ((1 - hostage.friction) * 10).ToString();
        for (int i = 0; i < info.transform.Find("HP").childCount; i++)
        {
            bool active = int.Parse(info.transform.Find("HP").GetChild(i).name) <= hostage.hp;
            info.transform.Find("HP").GetChild(i).gameObject.SetActive(active);
        }
        info.gameObject.SetActive(true);
    }
}
