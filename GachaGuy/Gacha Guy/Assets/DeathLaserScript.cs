using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLaserScript : MonoBehaviour
{
    private GameManager gm;
    public ShakeBehavior sb;
    // Start is called before the first frame update
    void Start()
    {
        sb = FindObjectOfType<ShakeBehavior>();
        gm = FindObjectOfType<GameManager>();
        StartCoroutine(StopLasering());
    }

    // Update is called once per frame
    void Update()
    {
        sb.TriggerShake();
    }

    IEnumerator StopLasering()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.playerDamaged(10);
        }
    }
}
