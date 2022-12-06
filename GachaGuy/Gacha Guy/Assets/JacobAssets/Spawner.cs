using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code quaratined with "//-" is code added by Zoom

public class Spawner : MonoBehaviour
{
    //-
    private GameManager gm;
    //-

    public GameObject Enemy1, Boomba, Gunba, Sawba, Aciba, point1, point2, point3;
    Vector3 location;

    private float enemyInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();

        //setInterval();
        spawn();
        //-

        //StartCoroutine(spawnEnemy(enemyInterval, Enemy1));
        //StartCoroutine(spawnEnemy(enemyInterval, Boomba));
        //location = this.transform.position;
    }

    //-
    /*public void setInterval() //Every time the danger level increases by 500 points, the enemy spawn rate decreases by 0.1f
    {
        if(enemyInterval != 0)
        {
            if (gm.dangerLevel >= 500)
            {
                gm.dangerLevel -= 500;
                enemyInterval -= .1f;
            }
        }


    }*/
    //-

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval / 2);

        int k = Random.Range(0, 3);
        if (k == 0)
        {
            location = new Vector3(point1.transform.position.x, point1.transform.position.y, 0);
            point1.SetActive(true);
        }
        else if (k == 1)
        {
            location = new Vector3(point2.transform.position.x, point2.transform.position.y, 0);
            point2.SetActive(true);
        }
        else if (k == 2)
        {
            location = new Vector3(point3.transform.position.x, point3.transform.position.y, 0);
            point3.SetActive(true);
        }

        yield return new WaitForSeconds(interval / 2);

        point1.SetActive(false);
        point2.SetActive(false);
        point3.SetActive(false);

        GameObject newEnemy = Instantiate(enemy, location, Quaternion.identity);

        //StartCoroutine(spawnEnemy(interval, enemy));
        //-
        //setInterval();
        spawn();
        //-
    }

    // Update is called once per frame
    void Update()
    {
        //-
        //setInterval();
        //spawn();
        //-

        //spawnEnemy(enemyInterval, Enemy1);
        //spawnEnemy(enemyInterval, Boomba);
    }

    //-
    private void spawn()
    {
        int i = 0;

       /* if(enemyInterval <= 1)
        {
            i = 4;
        }
        else if (enemyInterval <= 3)
        {
            i = 3;
        }
        else
        {
            i = 2;
        } */

        if(gm.dangerLevel < 500)
        {
            i = 2;
            enemyInterval = 3;
        }
        else if (gm.dangerLevel >= 1000)
        {
            i = 5;
            enemyInterval = 1;
        }
        else if (gm.dangerLevel >= 500)
        {
            i = 3;
            enemyInterval = 2;
        }

        int j = Random.Range(0, i);

        //StartCoroutine(off());

        if (j == 0)
            StartCoroutine(spawnEnemy(enemyInterval, Enemy1));
        else if (j == 1)
            StartCoroutine(spawnEnemy(enemyInterval, Sawba));
        else if (j == 2)
            StartCoroutine(spawnEnemy(enemyInterval, Gunba));
        else if (j == 3)
            StartCoroutine(spawnEnemy(enemyInterval, Boomba));
        else if (j == 4)
            StartCoroutine(spawnEnemy(enemyInterval, Aciba));
    }

    IEnumerator off()
    {
        yield return new WaitForSeconds(.5f);
        point1.SetActive(false);
        point2.SetActive(false);
        point3.SetActive(false);
    }
    //-
}
