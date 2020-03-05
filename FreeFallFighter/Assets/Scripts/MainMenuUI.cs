using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour, ISelectHandler
{

    public float timeLeft = 3.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 
    public float Player1MeterNumber = 0.2f;
    public float sliderNumber = 0f;
    public Scrollbar Player1Meter;
    public Slider sliderProgress;
    public bool TimerOn = true;
    public Text PlayerWins;

    ButtonMasher script;

    void Start()
    {
        script = GetComponent<ButtonMasher>();
    }

    void Update()
    {
        Timer();

        sliderNumber += 1f;
        sliderProgress.GetComponent<Slider>().value = timeLeft;
        PlayerWins.GetComponent<Text>();

        if (Input.GetButtonDown("Jump"))
        {
            Player1MeterNumber += 0.1f;
            Player1Meter.GetComponent<Scrollbar>().size = Player1MeterNumber;
        }

        if (script.player01wins)
        {
            Debug.Log("Player 1");
            PlayerWins.text = "Player 1 Wins";
            PlayerWins.color = Color.red;
        }

        if (script.player02wins)
        {
            Debug.Log("Player 2");
            PlayerWins.text = "Player 2 Wins";
            PlayerWins.color = Color.blue;
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
            startText.text = (timeLeft).ToString("0");
            if (timeLeft < 0)
            {
                TimerOn = false;
                GameOver();
            }
        }
    }

}


