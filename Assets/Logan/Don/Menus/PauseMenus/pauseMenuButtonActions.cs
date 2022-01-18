using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuButtonActions : MonoBehaviour
{


    void Start()
    {
        
    }


    void Update()
    {
        
    }


    public void quitQame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }    

    public void mainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
