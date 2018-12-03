using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostagePack : MonoBehaviour {
    public string[] unlocks;
    GameManager manager;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int findCount = 0;
            foreach (string unlock in unlocks)
            {
                foreach (Hostage hostage in Collectables.UnlockedHostages)
                {
                    if (hostage.id == unlock)
                    {
                        findCount++;
                        break;
                    }
                }
            }

            if (findCount < unlocks.Length-1 )
            {
                Hostage unlockThisOne = null;
                while (unlockThisOne == null)
                {
                    string unlock = unlocks[Random.Range(0, unlocks.Length - 1)];
                    bool alreadyUnlocked = false;
                    foreach (Hostage hostage in Collectables.UnlockedHostages)
                    {
                        if (hostage.id == unlock)
                        {
                            alreadyUnlocked = true;
                            break;
                        }
                    }
                    if (alreadyUnlocked == false)
                    {
                        foreach (Hostage hostage in Collectables.AllHostages)
                        {
                            if (hostage.id == unlock)
                            {
                                unlockThisOne = hostage;
                            }
                        }

                        if (unlockThisOne == null)
                        {
                            Debug.LogError("hostage:" + unlock + " does not exists!");
                            break;
                        }

                        manager.UnlockNewHostage(unlockThisOne);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
