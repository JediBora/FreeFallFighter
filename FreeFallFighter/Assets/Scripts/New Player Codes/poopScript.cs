using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopScript : MonoBehaviour
{
    TestPlayerMovement playerScript1;
    TestPlayerMovement playerScript2;
    public GameObject player1;
    public GameObject player2;
    //Player1Script p1Script;
    //Player2Script p2Script;




    private void Awake()
    {
        playerScript1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<TestPlayerMovement>();
        playerScript2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<TestPlayerMovement>();
        //p1Script = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player1Script>();
        //p2Script = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
    }
    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            playerScript1.stunned = true;
            Invoke("DelayPlayer1", 1);


        }
        if (collision.gameObject.tag == "Player2")
        {
            playerScript2.stunned = true;
            Invoke("DelayPlayer2", 1);
        }
    }
    void DelayPlayer1()
    {

        playerScript1.stunned = false;



    }
    void DelayPlayer2()
    {
        playerScript2.stunned = false;

    }
}


