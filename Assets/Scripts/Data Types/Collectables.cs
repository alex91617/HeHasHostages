using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collectables {

    public static List<Hostage> AllHostages;
    public static List<Hostage> UnlockedHostages;
	
    public static void LoadCollectables()
    {
        AllHostages = new List<Hostage>();
        UnlockedHostages = new List<Hostage>();
        
        //Grab all hostage file
        var hostages = Resources.LoadAll<TextAsset>("Hostages");

        //Convert text -> hostage and store data
        foreach(TextAsset hostage in hostages)
        {
            Hostage obj = JsonUtility.FromJson<Hostage>(hostage.text);
            AllHostages.Add(obj);
            Debug.Log("Loading - hostage:" + obj.id);
        }

        //Load unlocks from PlayerPrefs
        string unlockString = PlayerPrefs.GetString("unlocks", "json,richard,mike");
        string[] unlocks = unlockString.Split(',');
        foreach(string unlock in unlocks)
        {
            foreach(Hostage hostage in AllHostages)
            {
                if(unlock == hostage.id)
                {
                    UnlockedHostages.Add(hostage);
                    break;
                }
            }
        }
        Debug.Log("Player Unlocks: " + unlockString);
    }

    static public void SaveCollectables()
    {
        string hostageIds = "";
        foreach(Hostage hostage in UnlockedHostages)
        {
            hostageIds += hostage.id + ",";
        }
        PlayerPrefs.SetString("unlocks",hostageIds);
        PlayerPrefs.Save();
    }



    //Returns a random hostage or attempt to find a unique hostage randomly
    public static Hostage GrabAHostage(bool OnlyUnlocked = false)
    {
        if(OnlyUnlocked)
        {
            return UnlockedHostages[Random.Range(0, UnlockedHostages.Count)];
        }
        else
        {
            //Returns a random hostage
            return AllHostages[Random.Range(0, AllHostages.Count)];
        }
    }
}
