using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Boss1Script : MonoBehaviour
{
    public GameObject stomp;
    private Transform target;
    public float range = 10f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(StompAttack());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public IEnumerator StompAttack()
    {
        Debug.Log("STOMP!");
        if ((target.position.x - transform.position.x) <= range && (target.position.y - transform.position.y) <= range && (target.position.x - transform.position.x) >= -range && (target.position.y - transform.position.y) >= -range)
        {
            int randStomp = Random.Range(0, 4);
            if(randStomp == 3)
            {
                stomp.SetActive(true);
                yield return new WaitForSeconds(.1f);
                stomp.SetActive(false);
            }
            else{
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(StompAttack());
    }
}

//list of things to do
//stomp attack
//shoot pattern
//explosive attack?