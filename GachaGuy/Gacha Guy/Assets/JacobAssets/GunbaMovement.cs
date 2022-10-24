using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunbaMovement : MonoBehaviour
{
    float moveSpeed = 10f;
    Rigidbody2D rb;
    private Transform target;
    private Transform wall;
    private Transform obstacle;
    public Vector3 moveDirection;
    public GameObject enemy;
    public int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(6, 7, true);
        //Physics2D.IgnoreLayerCollision(7, 7, true);
        enemy = GameObject.FindWithTag("Shooter");
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        moveDirection = (target.position - transform.position).normalized * moveSpeed;
        obstacle = GameObject.FindWithTag("Obstacle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveDirection = (target.position + transform.position).normalized * moveSpeed;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = (target.position - transform.position).normalized * moveSpeed;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            moveDirection = (obstacle.position + transform.position).normalized * moveSpeed;
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            health = health - 1;
        }
    }
}
