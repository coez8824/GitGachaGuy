using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code quaratined with "//-" is code added by Zoom

public class Spawner : MonoBehaviour
{
    //-
    private GameManager gm;
    //-

    public GameObject Enemy1, Boomba;
    Vector3 location;

    private float enemyInterval = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        //-

        StartCoroutine(spawnEnemy(enemyInterval, Enemy1));
        StartCoroutine(spawnEnemy(enemyInterval, Boomba));
        location = this.transform.position;
    }

    //-
    public void setInterval() //Every time the danger level increases by 500 points, the enemy spawn rate decreases by 0.1f
    {
        if(enemyInterval != 0)
        {
            if (gm.dangerLevel >= 500)
                enemyInterval -= .1f;
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
        //-

        spawnEnemy(enemyInterval, Enemy1);
        //spawnEnemy(enemyInterval, Boomba);
    }
}
