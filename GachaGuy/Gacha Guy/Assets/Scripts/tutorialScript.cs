using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{
    public GameObject stuff;
    public GameObject loading;

    public void beginGame()
    {
        stuff.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene("Room1");
    }
}
