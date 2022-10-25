using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code quaratined with "//-" is code added by Zoom

public class Spawner : MonoBehaviour
{
    //-
    private GameManager gm;
    //-

    public GameObject Enemy1, Boomba, Gunba, Sawba, point1, point2, point3;
    Vector3 location;

    private float enemyInterval = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        //-

        StartCoroutine(spawnEnemy(enemyInterval, Enemy1));
        //StartCoroutine(spawnEnemy(enemyInterval, Boomba));
        //location = this.transform.position;
    }

    //-
    public void setInterval() //Every time the danger level increases by 500 points, the enemy spawn rate decreases by 0.1f
    {
        if(enemyInterval != 0)
        {
            if (gm.dangerLevel >= 500)
            {
                gm.dangerLevel -= 500;
                enemyInterval -= .1f;
            }
        }
    }
    //-

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, location, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    // Update is called once per frame
    void Update()
    {
        //-
        setInterval();
        spawn();
        //-

        //spawnEnemy(enemyInterval, Enemy1);
        //spawnEnemy(enemyInterval, Boomba);
    }

    //-
    private void spawn()
    {
        int i;

        if(enemyInterval <= 5)
        {
            i = 4;
        }
        else if (enemyInterval <= 8)
        {
            i = 3;
        }
        else
        {
            i = 2;
        }

        int j = Random.Range(0, i);

        int k = Random.Range(0, 3);

        if (k == 0)
            location = point1.transform.position;
        else if (k == 1)
            location = point2.transform.position;
        else if (k == 2)
            location = point3.transform.position;

        if (j == 0)
            spawnEnemy(enemyInterval, Enemy1);
        else if (j == 1)
            spawnEnemy(enemyInterval, Sawba);
        else if (j == 2)
            spawnEnemy(enemyInterval, Gunba);
        else if (j == 3)
            spawnEnemy(enemyInterval, Boomba);
    }
    //-
}
