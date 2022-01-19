using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void playGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void creditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
