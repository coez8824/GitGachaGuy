using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompScript : MonoBehaviour
{
    private GameManager gm;
    public Boss1Script Boss1;
    public Animator stompAnimator;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        Boss1 = FindObjectOfType<Boss1Script>();
        StartCoroutine(destroyStomp());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.playerDamaged(20);
        }
    }

    public IEnumerator destroyStomp()
    {
        yield return new WaitForSeconds(.45f);
        Destroy(gameObject);
    }
}
