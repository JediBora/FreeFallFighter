using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour
{

    public Text PlayerWins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Player 1");
            PlayerWins.text = "Player 1 Wins";
            PlayerWins.color = Color.red;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Player 2");
            PlayerWins.text = "Player 2 Wins";
            PlayerWins.color = Color.blue;
        }
    }
}
