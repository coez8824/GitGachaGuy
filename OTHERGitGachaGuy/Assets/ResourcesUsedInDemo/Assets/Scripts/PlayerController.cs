using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Turbo Makes Games on YouTube for Twin Stick Tutorial.
public class PlayerController : MonoBehaviour
{
    public GameObject all;

    public ShakeBehavior sB;

    public float playerSpeed;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots;

    //public float reloadTime;
    public bool canReload;
    public bool reloading;
    
    public bool canShoot;

    private Rigidbody2D rb;
    private Vector2 leftStickInput;
    private Vector2 rightStickInput;

    public Sprite UP;
    public Sprite RI;
    public Sprite LE;
    public Sprite BO;
    public PlayerVisual playerVisual;

    public int currClip;
    public int clipMax;
    public int ammoCount;

    public ReloadScript rS;

    public int playerHealth;
    public int maxHealth;

    public AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = 25;
        playerHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        canReload = false;
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rightStickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));
    }

    private void FixedUpdate()
    {
        if(playerHealth==0)
        {
            Debug.Log("You lose!");
            Destroy(all);
        }

        Vector2 currMovement = leftStickInput * playerSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + currMovement);

        if (rightStickInput.magnitude > 0f)
        {
            Vector3 currRotation = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
            Quaternion playerRotation = Quaternion.LookRotation(currRotation, Vector3.forward);

            rb.SetRotation(playerRotation);

            if (Input.GetAxis("R_Horizontal") > 0)
                playerVisual.changeSprite(RI);
            if (Input.GetAxis("R_Horizontal") < 0)
                playerVisual.changeSprite(LE);
            if (Input.GetAxis("R_Vertical") < 0)
                playerVisual.changeSprite(UP);
            if (Input.GetAxis("R_Vertical") > 0)
                playerVisual.changeSprite(BO);

            if (canShoot && (currClip != 0))
            Shoot();
        }
    }

    private void Shoot()
    {
        canShoot = false;
        canReload = true;
        aS.Play();
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        sB.TriggerShake();
        currClip--;
        StartCoroutine(ShotCoolDown());
    }

    public void Reload(float r)
    {
        if(canReload)
        {
            canReload = false;
            canShoot = false;
            Debug.Log("RELOAD");
            StartCoroutine(Reloading(r));
        }
    }

    IEnumerator Reloading(float r)
    {
        reloading = true;
        yield return new WaitForSeconds(r);

        if (rS.skip == true)
            yield break;

        int i = clipMax - currClip;
        ammoCount -= i;
        currClip += i;
        Debug.Log("RELOADED");
        canShoot = true;
        reloading = false;
    }

    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}
