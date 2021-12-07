using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableEnablePauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenuCanvas;
    public float menuCooldown;
    public float cooldownTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Time.timeScale = 1f;
            player.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
    }


}
