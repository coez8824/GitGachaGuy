using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

//Code quaratined with "//!" is code added by Jacob

public class GunScript : MonoBehaviour
{
    //IEnumerator courVis;

    private GameManager gm;
    public PlayerStats ps;
    public ShakeBehavior sb;
    public AudioSource bang;
    public AudioSource re;
    public AudioSource doneRe;
    public AudioSource empt;

    //GUN STATS - Gun stats are set to a gun and are not modified by any other method
    [SerializeField]
    private int DAM; //Damage
    [SerializeField]
    private float RAT; //Fire rate
    
    public int CLP; //Max ammo in clip
    [SerializeField]
    private float ACC; //Gun accuracy

    /*[SerializeField]
    private float shake; //How hard the recoil shake is*/ //Not actually sure if I want this

    
    public int curr; //Current ammo in clip
    public int curr1; //clip for gun1
    public int curr2; //clip for gun2

    public bool inf; //Whether or not gun will use ammo from AMM 

    public bool canShoot; //Whether or not the gun can shoot
    public bool canReload; //Whether or not the player can reload
    private bool reloading; //If currently reloading
    private bool skip; //Used when skipping reload
    public GameObject reloadVisual; //Empty parent holding reload visual parts
    public GameObject spinG; //Spinning part of reload visual

    public GameObject regButtons; //The regular buttons in UI
    public GameObject colorButtons; //Colored buttons for reload event
    private string buttonColor; //Current color for reload

    //Spot where bullets come out of
    public Transform firePoint;

    //private bool G;

    private bool canReloadLock;

    private void Start()
    {
        //G = false;

        gm = FindObjectOfType<GameManager>();

        canShoot = true;
        canReload = false;
        reloading = false;
        skip = false;

        canReloadLock = false;
    }


    private void Update()
    {
        if(curr == CLP)
        {
            canReload = false;
        }
        if(!canReloadLock)
        {
            if(curr < CLP)
                canReload = true;
        }
    }


    public void setStats(int d, float r, int c, float a, int currNUM)
    {
        DAM = d;
        RAT = r;
        CLP = c;
        ACC = a;

        currSetter(currNUM);
    }

    public void currSetter(int i)
    {
        if(i==0)
        {
            curr = CLP;
        }
        else if(i==1)
        {
            curr2 = curr;
            curr = curr1;
        }
        else if (i == 2)
        {
            curr1 = curr;
            curr = curr2;
        }
    }

    public void shoot()
    {
        if(curr != 0) //If there are still bullets in the clip
        {
            bang.Play();
            float a = Random.Range(-ACC * gm.ps.HND, ACC * gm.ps.HND); //Choose deviation based on ACC stat

            Vector2 r = new Vector2(a, 0); //Turn deviation into Vector2
            Vector2 rayVec = -firePoint.up + (transform.rotation * new Vector3(r.x, r.y, 0)); //Apply deviation

            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, rayVec, 10f); //Actual raycast

            if(hit)
            {
                if (hit.transform.tag == "HEX")
                {
                    hit.transform.GetComponent<EvilHexagon>().rip();
                    if(gm.vampirismActive == true)
                    {
                        if (ps.currHTH + (1 * gm.vampirismLevel) <= ps.getHTH())
                        {
                            ps.currHTH += (1 * gm.vampirismLevel);
                        }
                        else
                        {
                            ps.currHTH = ps.getHTH();
                        }
                    }
                    gm.ps.aggro++;
                }
                if (hit.transform.tag == "Shooter")
                {
                    hit.transform.GetComponent<Collision>().playerShot(DAM + gm.ps.PAM + gm.ps.aggroBonus);
                    if (gm.vampirismActive == true)
                    {
                        if (ps.currHTH + (1 * gm.vampirismLevel) <= ps.getHTH())
                        {
                            ps.currHTH += (1 * gm.vampirismLevel);
                        }
                        else
                        {
                            ps.currHTH = ps.getHTH();
                        }
                    }
                    gm.ps.aggro++;
                }
                if (hit.transform.tag == "Boomba")
                {
                    hit.transform.GetComponent<BoombaScript>().playerShot();
                    if (gm.vampirismActive == true)
                    {
                        if (ps.currHTH + (1 * gm.vampirismLevel) <= ps.getHTH())
                        {
                            ps.currHTH += (1 * gm.vampirismLevel);
                        }
                        else
                        {
                            ps.currHTH = ps.getHTH();
                        }
                    }
                    gm.ps.aggro++;
                }
                if (hit.transform.tag == "Gunba")
                {
                    hit.transform.GetComponent<GunbaMovement>().health--;
                    if (gm.vampirismActive == true)
                    {
                        if (ps.currHTH + (1 * gm.vampirismLevel) <= ps.getHTH())
                        {
                            ps.currHTH += (1 * gm.vampirismLevel);
                        }
                        else
                        {
                            ps.currHTH = ps.getHTH();
                        }
                    }
                    gm.ps.aggro++;
                }
                if (hit.transform.tag == "Sawba")
                {
                    hit.transform.GetComponent<RoombaMovement>().health--;
                    if (gm.vampirismActive == true)
                    {
                        if (ps.currHTH + (1 * gm.vampirismLevel) <= ps.getHTH())
                        {
                            ps.currHTH += (1 * gm.vampirismLevel);
                        }
                        else
                        {
                            ps.currHTH = ps.getHTH();
                        }
                    }
                    gm.ps.aggro++;
                }
                //!
                if (hit.transform.tag == "Melee")
                {
                    hit.transform.GetComponent<Collision>().playerShot(1);
                    if (gm.vampirismActive == true)
                    {
                        if (ps.currHTH + (1 * gm.vampirismLevel) <= ps.getHTH())
                        {
                            ps.currHTH += (1 * gm.vampirismLevel);
                        }
                        else
                        {
                            ps.currHTH = ps.getHTH();
                        }
                    }
                    gm.ps.aggro++;
                }
                //!
            }

            sb.TriggerShake(); //Shakes camera
            Debug.DrawRay(firePoint.transform.position, rayVec * 10f, Color.red); //Debug raycast

            canShoot=false;
            curr--; //Remove 1 bullet
            //canReload = true; //With a bullet gone, the gun can be reloaded
            StartCoroutine(ShotCoolDown()); 
        }
        else
        {
            Debug.Log("EMPTY");
            //empt.Play();
        }
    }

    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(RAT);
        if (!reloading)
            canShoot = true;
    }

    public void reload()
    {
        /*Debug.Log("RELOAD");
        if (gm.ps.AMM != 0)
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
        }*/
        if (((canReload)&&(gm.ps.AMM != 0)) || ((canReload) && (inf)))
        {
            //G = false;
            //if(courVis!=null)
            //StopCoroutine(courVis);

            //courVis = spinReload(6);
            re.Play();
            regButtons.SetActive(false);
            colorButtons.SetActive(true);

            buttonPicker();

            canReloadLock = true;
            canReload = false;
            canShoot = false;
            Debug.Log("RELOAD");
            reloadVisual.SetActive(true);
            StartCoroutine(Reloading(1));
            StartCoroutine(spinReload(1));
        }
        else
        {
            Debug.Log("CAN'T RELOAD");
        }
    }

    IEnumerator Reloading(float r)
    {
        reloading = true;
        yield return new WaitForSeconds(r);

        if (skip == true)
        {
            //Debug.Log("G");
            skip = false;
            yield break;
        }
        else// if (!G)
        {
            int a = CLP - curr;

            if(inf)
            {
                curr = CLP;
            }
            else if (gm.ps.AMM >= a)
            {
                curr = CLP;
                gm.ps.AMM -= a;
            }
            else if ((gm.ps.AMM < a))
            {
                curr += gm.ps.AMM;
                gm.ps.AMM = 0;
            }

            Debug.Log("RELOADED");
            doneRe.Play();
            canShoot = true;
            reloading = false;
            canReloadLock = false;

            colorButtons.SetActive(false);
            regButtons.SetActive(true);
        }
        //yield return null;
        
    }

    IEnumerator spinReload(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while ((t < duration))
        {
            if (skip == true)
            {
                //t = duration;
                //spinG.transform.eulerAngles = new Vector3(0, 0, 0);
                //StopCoroutine(Reloading(6));
                //skip = false;
                reloadVisual.SetActive(false);
                yield break;
            }
            else
            {
                t += Time.deltaTime;
                float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                spinG.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                yield return null;
            }
        }
        reloadVisual.SetActive(false);
    }

    private void buttonPicker()
    {
        int i = Random.Range(0, 3);

        if (i == 0)
        {
            buttonColor = "Red";
            spinG.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (i == 1)
        {
            buttonColor = "Blue";
            spinG.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (i == 2)
        {
            buttonColor = "Pink";
            spinG.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    public void reloadRed()
    {
        if(buttonColor == "Red")
        {
            skip = true;
            //G = true;
            skipReload();
        }
        else
        {
            Debug.Log("GOT IT WRONG");
            colorButtons.SetActive(false);
            regButtons.SetActive(true);
        }
    }

    public void reloadBlue()
    {
        if (buttonColor == "Blue")
        {
            skip = true;
            //G = true;
            skipReload();
        }
        else
        {
            Debug.Log("GOT IT WRONG");
            colorButtons.SetActive(false);
            regButtons.SetActive(true);
        }
    }

    public void reloadPink()
    {
        if (buttonColor == "Pink")
        {
            skip = true;
            //G = true;
            skipReload();
        }
        else
        {
            Debug.Log("GOT IT WRONG");
            colorButtons.SetActive(false);
            regButtons.SetActive(true);
        }
    }

    private void skipReload()
    {
        //G = true;

        int a = CLP - curr;

        if (inf)
        {
            curr = CLP;
        }
        else if(gm.ps.AMM >= a)
        {
            curr = CLP;
            gm.ps.AMM -= a;
        }
        else if ((gm.ps.AMM < a))
        {
            curr += gm.ps.AMM;
            gm.ps.AMM = 0;
        }
        Debug.Log("SKIP RELOADED");
        doneRe.Play();
        canShoot = true;
        reloading = false;
        canReloadLock = false;

        colorButtons.SetActive(false);
        regButtons.SetActive(true);
    }
}
