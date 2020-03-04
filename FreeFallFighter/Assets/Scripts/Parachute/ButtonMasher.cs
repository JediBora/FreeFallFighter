using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMasher : MonoBehaviour
{
    [SerializeField]
    private Slider Player01Gauge;
    [SerializeField]
    private Slider Player02Gauge;
    [SerializeField]
    private float ValueDecrement;
    [SerializeField]
    private float ValueIncrementGate;

    private float m_timeBetweenMash01;
    private float m_timeBetweenMash02;

    private float m_lastPressedTime01;
    private float m_lastPressedTime02;

    void Start()
    {
        Player01Gauge.value = 0f;
        Player01Gauge.value = 0f;
    }

    void Update()
    {
        MashInputCollector();
        UpdateUIGauge();
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
            m_timeBetweenMash01 = currentTime - m_lastPressedTime01;
            Debug.Log($"Time Between Mash Player 01: {m_timeBetweenMash01}");

            Player01Gauge.value += ValueIncrementGate - m_timeBetweenMash01;
            Debug.Log($"01 Value: {Player01Gauge.value}");

            m_lastPressedTime01 = Time.time;
        }

        if (Input.GetButtonDown("Player2Mash"))
        {
            m_timeBetweenMash02 = currentTime - m_lastPressedTime02;
            Debug.Log($"Time Between Mash Player 02: {m_timeBetweenMash02}");

            Player02Gauge.value += ValueIncrementGate - m_timeBetweenMash02;

            m_lastPressedTime02 = Time.time;
        }
    }

    private void CheckForWin()
    {
        if (Player01Gauge.value == 1f)
        {
            //Player 01 Wins
        }
        else if (Player02Gauge.value == 1f)
        {
            //Player 2 Wins
        }
        else
        {

        }
    }
}
