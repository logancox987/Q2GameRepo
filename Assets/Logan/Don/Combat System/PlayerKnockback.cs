using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] float knockbackLength = 0.5f;
    [SerializeField] float knockbackForce = 15f;

    playerMoveScript playerScript;
    Rigidbody2D rb2;
    public GameObject player;
    public GameObject playerAttack;
    public GameObject BlackBean;
    public GameObject KidneyBean;
    public GameObject PintoBean;
    public GameObject RedBean;
    public float knockbackTime;

    public bool IsHurt
    {
        get { return isHurt;  }
    }

    bool isHurt = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<playerMoveScript>();
        rb2 = GetComponent<Rigidbody2D>();
    }
    public void DoKnockback()
    {
        StartCoroutine(DisablePlayerMovement(knockbackLength));
        if (player.transform.position.x > BlackBean.transform.position.x || player.transform.position.x > KidneyBean.transform.position.x || player.transform.position.x > PintoBean.transform.position.x || player.transform.position.x > RedBean.transform.position.x)
        {
            rb2.velocity = new Vector2(knockbackForce, knockbackForce *0);
        }
        if (player.transform.position.x < BlackBean.transform.position.x || player.transform.position.x < KidneyBean.transform.position.x || player.transform.position.x < PintoBean.transform.position.x || player.transform.position.x < RedBean.transform.position.x)
        {
            rb2.velocity = new Vector2(-knockbackForce, knockbackForce*0);
        }

    }

    IEnumerator DisablePlayerMovement(float time)
    {
        playerAttack.GetComponent<playerAttack>().enabled = false;
        player.GetComponent<playerMoveScript>().enabled = false;
        rb2.velocity = new Vector2(rb2.velocity.x * 0, rb2.velocity.y);
        player.GetComponent<playerMoveScript>().playerAnimator.SetBool("moving", false);
        isHurt = true;

        yield return new WaitForSeconds(knockbackTime);

        playerAttack.GetComponent<playerAttack>().enabled = true;
        player.GetComponent<playerMoveScript>().enabled = true;
        isHurt = false;
    }

}
