using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    public GameObject player;
    public GameObject i;

    public float x;
    public float y;

    void Start()
    {
        DontDestroyOnLoad(i);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            change();
        }
    }

    public void change()
    {
        
        SceneManager.LoadScene(sceneName);
        player.transform.position = new Vector3(x, y, 0);
    }
}
