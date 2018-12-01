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
    }

    static void SaveCollectables()
    {
    }



    //Returns a random hostage or attempt to find a unique hostage randomly
    public static Hostage GrabAHostage(bool OnlyLocked = false)
    {
        if(OnlyLocked)
        {
            //Generate an empty hostage
            Hostage hostage = new Hostage();

            //Attempt to find a unique hostage (semi-randomly)
            for(int i = 0; i < AllHostages.Count*2; i++)
            {
                bool IsUnique = true;
                Hostage possibleHostage = AllHostages[Random.Range(0, AllHostages.Count)];
                foreach(Hostage AHostage in AllHostages)
                {
                    if(AHostage.id == possibleHostage.id)
                    {
                        IsUnique = false;
                    }
                }
                //Return hostage if unique
                if(IsUnique)
                {
                    hostage = possibleHostage;
                    break;
                }
            }
            //Return results
            return hostage;
        }
        else
        {
            //Returns a random hostage
            return AllHostages[Random.Range(0, AllHostages.Count)];
        }
    }
}
