using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour
{ 
    public Text PlayerWins;
    public GameObject playerWinsGO;
    public float endPos;
    public GameObject playAgain;
    public GameObject quit;

    // Start is called before the first frame update
    void Start()
    {
        playerWinsGO.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endPos >= 150)
        {
            playerWinsGO.transform.Translate(-Vector3.up * 50 * Time.deltaTime);
        }

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
