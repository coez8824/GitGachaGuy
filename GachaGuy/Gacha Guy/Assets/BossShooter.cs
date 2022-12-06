using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    //public AudioSource bang;

    // Start is called before the first frame update
    public GameObject projectilePrefab; //drag prefab to this variable in editor
    private GameObject spawnedProjectile;
    private Rigidbody2D projectilePhysics;
    public float range = 10f;
    private Transform target;
    //public float projectileSpeed = 10f;
    public float interval = 3f;
    public float testX;
    public float testY;
    public Boss1Script Boss1Script;
    public Rigidbody2D rb;
    public GameObject boss;
    public float angle;
    //public Collision col;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(fire(interval));
        rb = this.GetComponent<Rigidbody2D>();
    }

    public IEnumerator fire(float interval)
    {

        yield return new WaitForSeconds(interval);
        if ((target.position.x - transform.position.x) <= range && (target.position.y - transform.position.y) <= range && (target.position.x - transform.position.x) >= -range && (target.position.y - transform.position.y) >= -range && Boss1Script.health > 0 && Boss1Script.SuperDeathLaserFiring == false && Boss1Script.stomping == false)
        {
            //bang.Play();
            spawnedProjectile = Instantiate(projectilePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);

            //get physics of spawned projectile
            //projectilePhysics = spawnedProjectile.GetComponent<Rigidbody2D>();
        }
        //Destroy(spawnedProjectile);
        float nextInterval = interval;
        StartCoroutine(fire(nextInterval));

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        rb.rotation = angle + 270;
        rb.position = boss.transform.position;
        fire(interval);
        testX = target.position.x - transform.position.x;
        testY = target.position.y - transform.position.y;
    }
}
