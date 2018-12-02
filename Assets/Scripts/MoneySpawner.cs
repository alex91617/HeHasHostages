using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoneySpawner : MonoBehaviour {

    public float radius = 2f;
    public int countMin = 1;
    public int countMax = 5;

    public GameObject moneybag;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < countMax; i++)
        {
            Instantiate(moneybag).transform.position = GetPos() + transform.position;
        }
    }
	
    Vector3 GetPos()
    {
        return Random.insideUnitCircle * radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
