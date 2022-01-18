using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerOnTop : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.enableMove = false;
        Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        Invoke("TurnOnPlayerMovement", 2.5f);



    }

    private void TurnOnPlayerMovement()
    {
        GameManager.enableMove = true;
    }

}
