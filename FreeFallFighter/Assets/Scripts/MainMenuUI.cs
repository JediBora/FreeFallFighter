using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 
    public float Player1MeterNumber = 0.2f;
    public Scrollbar Player1Meter;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            //Do something
            
        }

        if (Input.GetButtonDown("Jump"))
        {
            Player1MeterNumber += 0.1f;
            Player1Meter.GetComponent<Scrollbar>().size = Player1MeterNumber;
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("In-Game UI Layout");
    }

    public void Quit()
    {
        Application.Quit();
    }

}


