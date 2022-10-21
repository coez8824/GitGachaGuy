using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

public class GunScript : MonoBehaviour
{
    private GameManager gm;

    //GUN STATS - Gun stats are set to a gun and are not modified by any other method
    private int DAM; //Damage
    private float RAT; //Fire rate
    private int CLP; //Max ammo in clip
    private float ACC; //Gun accuracy

    private int curr; //Current ammo in clip

    public bool canShoot; //Whether or not the gun can shoot

    //Spot where bullets come out of
    public Transform firePoint;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        //Default stats
        setStats(1, 1, 7, .5f);
        canShoot = true;

        curr = CLP;
    }

    

    public void setStats(int d, float r, int c, float a)
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
        if(curr != 0) //If there are still bullets in the clip
        {
            float a = Random.Range(-ACC * gm.ps.HND, ACC * gm.ps.HND); //Choose deviation based on ACC stat

            Vector2 r = new Vector2(a, 0); //Turn deviation into Vector2
            Vector2 rayVec = -firePoint.up + (transform.rotation * new Vector3(r.x, r.y, 0)); //Apply deviation

            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, rayVec, 10f); //Actual raycast
            Debug.DrawRay(firePoint.transform.position, rayVec * 10f, Color.red); //Debug raycast

            canShoot=false;
            curr--; //Remove 1 bullet
            StartCoroutine(ShotCoolDown()); 
        }
        else
        {
            Debug.Log("EMPTY");
        }
    }

    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(RAT);
        canShoot = true;
    }

    public void reload()
    {
        if(gm.ps.AMM != 0)
        {
            //STILL NEEDS TO ACCOUNT FOR THE TOTAL AMMO LEFT BEING LESS THAN CLP

            curr = CLP;
            gm.ps.AMM -= (CLP - curr);
        }
        else
        {
            Debug.Log("NO AMMO");
        }
    }
}
