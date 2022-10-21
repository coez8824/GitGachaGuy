using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

public class GunScript : MonoBehaviour
{
    private GameManager gm;

    //GUN STATS - Gun stats are set to a gun and are not modified by any other method
    [SerializeField]
    private int DAM; //Damage
    [SerializeField]
    private float RAT; //Fire rate
    [SerializeField]
    private int CLP; //Max ammo in clip
    [SerializeField]
    private float ACC; //Gun accuracy

    [SerializeField]
    private int curr; //Current ammo in clip

    public bool canShoot; //Whether or not the gun can shoot

    //Spot where bullets come out of
    public Transform firePoint;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        canShoot = true;
    }

    

    public void setStats(int d, float r, int c, float a)
    {
        DAM = d;
        RAT = r;
        CLP = c;
        ACC = a;

        curr = CLP;
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
            int a = CLP - curr;

            if(gm.ps.AMM >= a)
            {
                curr = CLP;
                gm.ps.AMM -= a;
            }
            else if ((gm.ps.AMM < a))
            {
                curr += gm.ps.AMM;
                gm.ps.AMM = 0;
            }
        }
        else
        {
            Debug.Log("NO AMMO");
        }
    }
}
