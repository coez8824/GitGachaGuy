using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public GameManager gM;

    public int enemyHealth;

    public GameObject player;

    public float speed;

    public AudioSource aS;

    public GameObject HPickup;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 5;
        player = GameObject.FindWithTag("Player");
        gM = FindObjectOfType<GameManager>();
    }

    public void loseHealth(int i)
    {
        aS.Play();
        enemyHealth = enemyHealth - i;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth == 0)
        {
            int i = Random.Range(0, 4);
            //int i = 1;

            if(i == 1)
                Instantiate(HPickup, this.transform.position, this.transform.rotation);

            gM.cash+=25;
            Destroy(gameObject);
        }

        if(player != null)
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerController>().playerHealth--;

    }
}
