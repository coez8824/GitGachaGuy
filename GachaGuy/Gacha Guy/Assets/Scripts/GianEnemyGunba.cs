using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GianEnemyGunba : MonoBehaviour
{
    public GunbaMovement gun;

    // Update is called once per frame
    void Update()
    {
        if (gun.health <= 1)
        {
            Destroy(GameObject.Find("Important"));
            SceneManager.LoadScene("YouWin");
        }
    }
}
