using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject controls;
    public GameObject notControls;

    public void beginGame()
    {
        SceneManager.LoadScene("Room1");
    }

    public void openControls()
    {
        notControls.SetActive(false);
        controls.SetActive(true);
    }

    public void closeControls()
    {
        controls.SetActive(false);
        notControls.SetActive(true);
    }
}
