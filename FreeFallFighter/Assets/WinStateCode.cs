using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStateCode : MonoBehaviour
{
    public ButtonMasher ButtonMasherScript;
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

        if (collision.gameObject.tag == "Player1" && ButtonMasherScript.player01wins)
        {
            SceneManager.LoadScene("GameOver PLayer 1 Wins");
            print("player1 wins");

        }

        if (collision.gameObject.tag == "Player2" && ButtonMasherScript.player02wins)
        {
            SceneManager.LoadScene("GameOver PLayer 2 Wins");
            print("player2 wins");
        }

    }
}
