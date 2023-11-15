using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Village1");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}