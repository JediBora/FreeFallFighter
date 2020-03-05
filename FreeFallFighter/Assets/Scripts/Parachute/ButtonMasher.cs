﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMasher : MonoBehaviour
{
    public GameObject WinningPlayer;
    public GameObject Player01;
    public GameObject Player02;

    [SerializeField]
    private Slider Player01Gauge;
    [SerializeField]
    private Slider Player02Gauge;
    [SerializeField]
    private float ValueDecrement;
    [SerializeField]
    private float ValueIncrementGate;
    [SerializeField]
    private int PlayerScore = 10;

    private float m_timeBetweenMash01;
    private float m_timeBetweenMash02;

    private float m_lastPressedTime01;
    private float m_lastPressedTime02;

    private int player01ButtonAmount = 0;
    private int player02ButtonAmount = 0;

    public bool player01wins = false;
    public bool player02wins = false;

    public ParachuteController script;

    void Start()
    {
        WinningPlayer = null;
        Player01Gauge.value = 0f;
        Player01Gauge.value = 0f;
    }

    void Update()
    {
        MashInputCollector();
        UpdateUIGauge();
        CheckForWin();

        print(player01ButtonAmount);
        print(player02ButtonAmount);
    }

    public void ActivateObject(bool state)
    {
        Player01Gauge.gameObject.SetActive(state);
        Player02Gauge.gameObject.SetActive(state);
    }

    private void UpdateUIGauge()
    {
        Player01Gauge.value += ValueDecrement * Time.deltaTime;
        Player02Gauge.value += ValueDecrement * Time.deltaTime;
        
    }

    private void MashInputCollector()
    {
        float currentTime = Time.time;

        if (Input.GetButtonDown("Player1Mash"))
        {
            if (script.m_collected)
            {
                player01ButtonAmount += 1;
            }
            m_timeBetweenMash01 = currentTime - m_lastPressedTime01;
            //Debug.Log($"Time Between Mash Player 01: {m_timeBetweenMash01}");

            Player01Gauge.value += ValueIncrementGate - m_timeBetweenMash01;
            //Debug.Log($"01 Value: {Player01Gauge.value}");

            m_lastPressedTime01 = Time.time;
        }

        if (Input.GetButtonDown("Player2Mash"))
        {
            if (script.m_collected)
            {
                player02ButtonAmount += 1;
            }

            m_timeBetweenMash02 = currentTime - m_lastPressedTime02;
            //Debug.Log($"Time Between Mash Player 02: {m_timeBetweenMash02}");

            Player02Gauge.value += ValueIncrementGate - m_timeBetweenMash02;

            m_lastPressedTime02 = Time.time;
        }
    }

    public void CheckForWin()
    {
        if (Player01Gauge.value >= 0.98f)
        {
            WinningPlayer = Player01;
        }
        else if (Player02Gauge.value >= 0.98f)
        {
            WinningPlayer = Player02;
        }
        else
        {
            WinningPlayer = null;
        }

        if (script.m_collected && Player01 && player01ButtonAmount == PlayerScore)
        {
            
            //print("player 1 wins");
            player01wins = true;
        }
        if (script.m_collected && Player02 && player02ButtonAmount == PlayerScore)
        {
            // print("player 2 wins");
            player02wins = true;

        }
    }
}
