using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopScript : MonoBehaviour
{
    Player1Script p1Script;
    Player2Script p2Script;

    private void Update()
    {
        //player1 = GameObject.FindWithTag("Player1");
        //player2 = GameObject.FindWithTag("Player2");

    }
    private void Awake()
    {
        p1Script = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player1Script>();
        p2Script = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            p1Script.speed = 0;
            Invoke("DelayPlayer1", 3);


        }
        if (collision.gameObject.tag == "Player2")
        {
            p2Script.speed = 0;
            Invoke("DelayPlayer2", 3);
        }
    }
    void DelayPlayer1()
    {

        p1Script.speed = 10;


    }
    void DelayPlayer2()
    {
        p2Script.speed = 10;
    }
}


