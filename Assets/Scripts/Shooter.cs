using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    LineRenderer line;
    PlayerManager player;
    private Vector3 playerLaserOffset = new Vector3(0, 0.125f, 0);

    //Give up times
    public float giveUpTime = 10f;
    float SearchTime;
    Transform startingPos;

    //Shooting
    public float fireRate = 3.5f;
    float TimeUntilFiring;
    public bool canShoot = true;
    public GameObject bulletPrefab;
    public float bulletPower = 4f;
    bool reloading = false;
    public float reloadTime = 1;
    public bool outOfRange;
    public bool debug = false;

    //Voice lines
    public List<AudioClip> voiceLines;


    Pathfinding.AIPath pathing;

    //Line of sight
    const float RANGE = 3f;
    const float ACTIVE_RANGE = 2f;

    bool active = false;


    void Start() {
        GameObject temp = Instantiate(new GameObject());
        temp.transform.position = this.transform.position;
        startingPos = temp.transform;

        SearchTime = giveUpTime;

        line = this.GetComponent<LineRenderer>();
        player = GameObject.FindObjectOfType<PlayerManager>();
        line.useWorldSpace = true;
        TimeUntilFiring = fireRate;
        pathing = transform.parent.GetComponent<Pathfinding.AIPath>();

        outOfRange = Vector2.Distance(this.transform.position, player.transform.position) < RANGE;

        active = CheckLineOfSight(ACTIVE_RANGE);
    }


    void Update()
    {
        if (active)
        {
            line.enabled = canShoot & (outOfRange == false);
            if (line.enabled)
            {
                //Update laser display
                line.SetPosition(0, transform.position + new Vector3(0.055f, -0.075f, 0f));
                line.SetPosition(1, player.transform.position + playerLaserOffset);
            }


            outOfRange = !CheckLineOfSight(RANGE);
            if(outOfRange)
            {
                SearchTime -= Time.deltaTime;
            }
            else
            {
                SearchTime = giveUpTime;
            }

            if(SearchTime <= 0)
            {
                active = false;
            }


            //Countdown
            TimeUntilFiring -= Time.deltaTime;
        }
        else
        {
            active = CheckLineOfSight(ACTIVE_RANGE);
            if(active)
            {
                AudioSource audio = transform.parent.GetComponent<AudioSource>();
                AudioClip clip = voiceLines[Random.Range(0, voiceLines.Count)];
                audio.clip = clip;
                audio.Play();
                SearchTime = giveUpTime;
            }
        }
    }
    private void FixedUpdate()
    {
        if (active)
        {
            if (canShoot & TimeUntilFiring <= 0 & (outOfRange == false))
            {
                pathing.canMove = false;
                StartCoroutine(Shoot());
            }
            if (canShoot == false & reloading == false)
            {
                pathing.canMove = false;
                reloading = true;
                StartCoroutine(Reload());
            }

            if (outOfRange)
            {
                pathing.canMove = true;
                pathing.target = player.transform;
            }
        }
        else
        {
            pathing.canMove = true;
            pathing.target = startingPos;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        line.enabled = true;
        TimeUntilFiring = fireRate;
        canShoot = true;
        reloading = false;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        line.enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().Play();
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

    public bool CheckLineOfSight(float distance)
    {

        //precompute our ray settings
        Vector3 start = transform.position;
        Vector3 direction = (player.transform.position - transform.position).normalized;


        //draw the ray in the editor
        Debug.DrawRay(start, direction * Mathf.Clamp(Vector2.Distance(this.transform.position, player.transform.position), 0, distance), Color.yellow);

        //do the ray test
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position, Mathf.Clamp(Vector2.Distance(this.transform.position,player.transform.position),0,distance));

        //now iterate over all results to work out what has happened
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D sightTest = hits[i];
            if (sightTest.collider.gameObject != player.gameObject & sightTest.collider.tag != "Money" & sightTest.collider.tag != "Hostage")
            {
                return false;
            }
        }
        return true & (Vector2.Distance(this.transform.position, player.transform.position) <= distance);
    }
}
