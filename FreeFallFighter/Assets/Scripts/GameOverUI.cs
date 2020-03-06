using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour
{ 
    public Text PlayerWins;
    public GameObject playerWinsGO;
    public float endPos;
    public Image playAgain;
    public Image quit;

    public int moveNum = 10;


    public void Awake()
    {
        Color c = playAgain.color;
        c.a = 0;
        playAgain.color = c;
        quit.color = c;

    }

    // Start is called before the first frame update
    void Start()
    {
        playerWinsGO.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endPos <= moveNum)
        {
            playerWinsGO.transform.Translate(-Vector3.up * 2 * Time.deltaTime);
            endPos += 0.1f;
        }

        if(endPos >= moveNum)
        {
            StartCoroutine(UIDelay());
        }

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Debug.Log("Player 1");
        //    PlayerWins.text = "Player 1 Wins";
        //    PlayerWins.color = Color.red;
        //}

        //if (Input.GetButtonDown("Fire2"))
        //{
        //    Debug.Log("Player 2");
        //    PlayerWins.text = "Player 2 Wins";
        //    PlayerWins.color = Color.blue;
        //}
    }

    IEnumerator UIDelay()
    {

        yield return new WaitForSeconds(0.75f);

        Color c = playAgain.color;
        c.a = 250;
        playAgain.color = c;

        yield return new WaitForSeconds(1.35f);

        quit.color = c;

        StopCoroutine(UIDelay());
    }

}
