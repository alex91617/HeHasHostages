using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    LineRenderer line;
    PlayerManager player;
    private Vector3 playerLaserOffset = new Vector3(0,0.125f,0);


    public float TimeUntilFiring = 5;
    public bool canShoot = true;
    public GameObject bulletPrefab;
    public float bulletPower = 13f;


	void Start () {
        line = this.GetComponent<LineRenderer>();
        player = GameObject.FindObjectOfType<PlayerManager>();
    }
	

	void Update () {
        //Update laser display
        line.SetPosition(0, transform.position);
        line.SetPosition(1, player.transform.position + playerLaserOffset);

        //Countdown
        TimeUntilFiring -= Time.deltaTime;
	}
    private void FixedUpdate()
    {
        if (canShoot & TimeUntilFiring <= 0)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        line.enabled = false;
        canShoot = false;

        yield return new WaitForSeconds(0.5f);
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;

        //Rotate bullet to face target
        Vector3 vectorToTarget = player.transform.position - bullet.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);

        //Add force to send bullet towards target
        bullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * bulletPower * 100);
    }
}
