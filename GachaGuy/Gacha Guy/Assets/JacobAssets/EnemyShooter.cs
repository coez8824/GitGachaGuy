using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EnemyShooter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectilePrefab; //drag prefab to this variable in editor
    private GameObject spawnedProjectile;
    private Rigidbody2D projectilePhysics;
    //private Transform target;
    //public float projectileSpeed = 10f;
    public float interval = 3f;
    void Start()
    {
        //target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(fire(interval));
    }

    private IEnumerator fire(float interval)
    {
        yield return new WaitForSeconds(interval);
        spawnedProjectile = Instantiate(projectilePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);

        //get physics of spawned projectile
        //projectilePhysics = spawnedProjectile.GetComponent<Rigidbody2D>();

        //Destroy(spawnedProjectile);
        float nextInterval = Random.Range(2f, 5f);
        StartCoroutine(fire(nextInterval));
    }

    // Update is called once per frame
    void Update()
    {
        
        fire(interval);
    }
}
