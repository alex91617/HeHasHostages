using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public bool hasHostage = false;
    public GameObject hostagePrefab;
    public float spawnTime = 1f;
    bool loadingHostage = false;
    GameManager manager;


	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hasHostage == false & loadingHostage == false)
        {
            loadingHostage = true;
            manager.LoadHostage();
            StartCoroutine(SpawnHostage(manager.currentHostage));
        }
	}
    IEnumerator SpawnHostage(Hostage hostage)
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject obj = Instantiate(hostagePrefab);

        //Setup object
        obj.name = hostage.name;
        obj.transform.position = this.transform.position;
        obj.GetComponent<SpriteRenderer>().sprite = hostage.getSprite();
        obj.GetComponent<SpringJoint2D>().distance = 0.25f;
        obj.GetComponent<SpringJoint2D>().connectedAnchor = transform.position;

        //Manage logic variables
        hasHostage = true;
        loadingHostage = false;
    }
}
