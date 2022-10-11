using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Turbo Makes Games on YouTube for Twin Stick Tutorial.
public class BulletController : MonoBehaviour
{
    public float speed;
    public float timeToLive;

    public int bulletDamage;

    Vector3 moveVector;

    // Start is called before the first frame update
    private void Start()
    {
        bulletDamage = 1;

        moveVector = Vector3.up * speed * Time.fixedDeltaTime;
        StartCoroutine(DestroyBullet());
    }

    private void FixedUpdate()
    {
        transform.Translate(moveVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<TestEnemy>().loseHealth(bulletDamage);

            Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
