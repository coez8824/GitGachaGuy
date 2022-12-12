using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLeave : MonoBehaviour
{
    Tracker a;
    public GameObject b;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            a = GameObject.FindWithTag("Tracker").GetComponent<Tracker>();
            b.GetComponent<Tracker>().give(a);
            DontDestroyOnLoad(b);
            Destroy(GameObject.Find("Important"));
            SceneManager.LoadScene("Win");
        }
    }
}
