using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 

    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            //Do something useful or Load a new game scene depending on your use-case
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


