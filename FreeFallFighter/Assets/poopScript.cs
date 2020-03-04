using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopScript : MonoBehaviour
{
    GameObject player1;
    GameObject player2;

    private void Update()
    {
        //player1 = GameObject.FindWithTag("Player1");
        //player2 = GameObject.FindWithTag("Player2");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            GameObject.FindGameObjectWithTag("Player1").GetComponent<Player1Script>().enabled = false;
            Invoke("DelayPlayer1", 2);


        }
        if (collision.gameObject.tag == "Player2")
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>().enabled = false;
            Invoke("DelayPlayer2", 2);
        }
    }
    void DelayPlayer1()
    {
        GameObject.FindGameObjectWithTag("Player1").GetComponent<Player1Script>().enabled = true;



    }
    void DelayPlayer2()
    {
        GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>().enabled = true;
        print("test");
    }
}


