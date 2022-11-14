using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EnemyShooter : MonoBehaviour
{
    public AudioSource bang;

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
    //public Collision col;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(fire(interval));
    }

    public IEnumerator fire(float interval)
    {
        
            yield return new WaitForSeconds(interval);
        if ((target.position.x - transform.position.x) <= range && (target.position.y - transform.position.y) <= range && (target.position.x - transform.position.x) >= -range && (target.position.y - transform.position.y) >= -range /*&& col.health > 0*/)
        {
            bang.Play();
            spawnedProjectile = Instantiate(projectilePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);

            //get physics of spawned projectile
            //projectilePhysics = spawnedProjectile.GetComponent<Rigidbody2D>();
        }
            //Destroy(spawnedProjectile);
            float nextInterval = Random.Range(2f, 5f);
            StartCoroutine(fire(nextInterval));
        
    }

    // Update is called once per frame
    void Update()
    {
            fire(interval);
        testX = target.position.x - transform.position.x;
        testY = target.position.y - transform.position.y;
    }
}
