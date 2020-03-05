using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public Text timestartText; 
    public float Player1MeterNumber = 0.2f;
    public float sliderNumber = 0f;
    public Scrollbar Player1Meter;
    public Slider sliderProgress;
    public bool TimerOn = true;


    void Start()
    {

    }

    void Update()
    {
        Timer();

        sliderNumber += 1f;
        sliderProgress.GetComponent<Slider>().value = timeLeft;

        if (Input.GetButtonDown("Jump"))
        {
            Player1MeterNumber += 0.1f;
            Player1Meter.GetComponent<Scrollbar>().size = Player1MeterNumber;
        }


    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + "this was selected");

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("In-Game UI Layout");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu UI Layout");
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene("Character Selection");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Timer()
    {
        if (TimerOn == true)
        {
            timeLeft -= Time.deltaTime;

            timestartText.text = (timeLeft).ToString("0");

            startText.text = (timeLeft).ToString("0");

            if (timeLeft < 0)
            {
                TimerOn = false;
                GameOver();
            }
        }
    }

}


