using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStateCode : MonoBehaviour
{
    public ButtonMasher script;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player1" && script.player01wins)
        {
            print("player1 wins");

        }

        if (collision.gameObject.tag == "Player2" && script.player02wins)
        {

            print("player2 wins");
        }

    }
}
