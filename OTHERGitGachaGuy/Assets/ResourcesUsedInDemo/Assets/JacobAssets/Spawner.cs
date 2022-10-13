using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy1;
    Vector3 location;

    private float enemyInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, Enemy1));
        location = this.transform.position;
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, location, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy(enemyInterval, Enemy1);
    }
}
