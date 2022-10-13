using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Code Monkey on YouTube for some starting code.
public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField]
    private Transform prefBullet;

    public void shoot(Vector2 firePoint)
    {
        Transform bulletTransform = Instantiate(prefBullet, firePoint, Quaternion.identity);
        Vector3 shootDir = firePoint.normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }
}
