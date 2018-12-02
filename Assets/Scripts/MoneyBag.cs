using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour {

    public int value = 5;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerManager>().money += value;
            if(effect != null)
            {
                Instantiate(effect).transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}
