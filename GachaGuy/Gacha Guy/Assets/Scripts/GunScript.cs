using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //GUN STATS
    private int DAM; //Damage
    private float RAT; //Fire rate
    private int CLP; //Max ammo count
    private float ACC; //Gun accuracy

    private int curr; //Current ammo in clip

    public bool canShoot; //Whether or not the gun can shoot

    //Spot where bullets come out of
    public Transform firePoint;

    private void Start()
    {
        //Default stats
        setStats(1, 1, 7, 1);
        canShoot = true;
    }

    public void setStats(int d, int r, int c, int a)
    {
        DAM = d;
        RAT = r;
        CLP = c;
        ACC = a;
    }

    void FixedUpdate()
    {

    }

    public void shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, firePoint.TransformDirection(-Vector2.up), 10f);
        Debug.DrawRay(firePoint.transform.position, firePoint.TransformDirection(-Vector2.up) * 10f, Color.red);

        //canShoot=false;
        StartCoroutine(ShotCoolDown());
    }

    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(RAT);
        canShoot = true;
    }
}
