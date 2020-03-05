﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 
    public float Player1MeterNumber = 0.2f;
    public float sliderNumber = 0f;
    public Scrollbar Player1Meter;
    public Slider sliderProgress;
    public bool TimerOn = true;
    public Text PlayerWins;

    public Text blinkingText;
    public float blinkingTimer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        Timer();
        Blink();
        Debug.Log("Woring");

        sliderNumber += 1f;
        sliderProgress.GetComponent<Slider>().value = timeLeft;

        if (Input.GetButtonDown("Jump"))
        {
            Player1MeterNumber += 0.1f;
            Player1Meter.GetComponent<Scrollbar>().size = Player1MeterNumber;
        }

    }

    public void Blink()
    {

        Debug.Log("Wokring");

        blinkingTimer += Time.deltaTime;

        if (blinkingTimer >= 0.5)
        {
            blinkingText.GetComponent<Text>().enabled = true;
        }

        if (blinkingTimer >= 1)
        {
            blinkingText.GetComponent<Text>().enabled = false;
            blinkingTimer = 0;
        }
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
        if (TimerOn == true) {
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


