using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject controls;
    public GameObject notControls;
    public GameObject loading;

    public void beginGame()
    {
        notControls.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene("Room1");
    }

    public void quitGame()
    {
        Application.Quit();
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
