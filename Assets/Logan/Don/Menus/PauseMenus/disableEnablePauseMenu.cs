using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableEnablePauseMenu : MonoBehaviour
{
    public GameObject hideOnPause;
    public GameObject pauseMenuCanvas;
    public float menuCooldown;
    public float cooldownTimer;

    void Start()
    {
        hideOnPause = GameObject.FindGameObjectWithTag("hideOnPause");
        pauseMenuCanvas = GameObject.FindGameObjectWithTag("PauseMenuCanvas");
        pauseMenuCanvas.SetActive(false);
        cooldownTimer = menuCooldown;
    }

    // Update is called once per frame
    void Update()
    {

        cooldownTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape) && cooldownTimer < .1f)
        {
            if(pauseMenuCanvas.activeInHierarchy == false)
            {
                
                pauseMenuCanvas.SetActive(true);
            }
            else
            {
                pauseMenuCanvas.SetActive(false);
                cooldownTimer = menuCooldown;
            }
        }

        if(pauseMenuCanvas.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
                if(hideOnPause !=null)
                {
                    hideOnPause.SetActive(false);
                }

        }
        else
        {
            Time.timeScale = 1f;
            hideOnPause.SetActive(true);
        }

    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        cooldownTimer = menuCooldown;
    }


}
