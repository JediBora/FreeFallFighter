﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{
    [SerializeField]
    private float AccelerationTime = 1;
    [SerializeField]
    private float DecelerationTime = 1;
    [SerializeField]
    private float MaxSpeed = 1;
    [SerializeField]
    private float MinSpeed = 0.5f;
    [SerializeField]
    private float AddedLaunchSpeed = 1f;
    
    [SerializeField]
    private float PositionResetRange = 0.1f;
    [SerializeField]
    private float PositionOffsetWhileCollected = 1f;


    //[SerializeField]
    //private float Speed = 1.0f;
    //[SerializeField]
    //private float RotateSpeedInDegrees = 45.0f;
    //[SerializeField]
    //private float EasingMultiplier = 1f;

    [SerializeField]
    private float ScreenSizeX = 5.5f;
    [SerializeField]
    private float ScreenSizeY = 4.3f;

    public GameObject ButtonMasherObject;

    private GameObject m_playerWhoCollected;
    private Rigidbody2D m_parachuteRigidbody2D;
    private SpriteRenderer m_spriteRenderer;

    private float m_initialVelocity;
    private float m_finalVelocity;   

    private Vector2 m_parachuteToNextPosition;
    private Vector2 m_nextPosition;
    private Vector2 m_lastDirection;

    private bool m_nextPositionReached;

    public bool m_collected;

    private void Start()
    {
        m_parachuteRigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_parachuteRigidbody2D.position = new Vector2(0f, 6f);
        m_nextPosition = Vector2.zero;

        //Debug.Log($"Next Position: {m_nextPosition}");
    }

    private void Update()
    {
        SetRandomNextPosition();
        Movement();

        // Locks location of child sprite
        //transform.GetChild(0).transform.rotation = Quaternion.identity;
    }

    private void SetRandomNextPosition()
    {
        m_parachuteToNextPosition = m_nextPosition - m_parachuteRigidbody2D.position;
        //Debug.Log($"ParachuteToNext: {m_parachuteToNextPosition}");
        //Debug.Log($"ParachuteToNextNormalized: {m_parachuteToNextPosition.normalized}");

        if (m_parachuteToNextPosition.magnitude < PositionResetRange)
        {
            m_nextPositionReached = true;
            m_nextPosition = new Vector3(Random.Range(-ScreenSizeX, ScreenSizeX), Random.Range(-ScreenSizeY, ScreenSizeY));
        }
    }

    private void Movement()
    {
        if (!m_collected)
        {
            if (!m_nextPositionReached && m_parachuteToNextPosition.magnitude > PositionResetRange)
            {
                m_lastDirection = m_parachuteToNextPosition.normalized;
                //Debug.Log($"LastDirection: {lastDirection}");

                //Debug.Log("ACCELERATE");
                float accelerationRate = MaxSpeed / AccelerationTime;

                if (m_finalVelocity < MaxSpeed)
                {
                    m_finalVelocity = m_initialVelocity + (accelerationRate * Time.deltaTime);

                    if (m_finalVelocity > MaxSpeed)
                    {
                        m_finalVelocity = MaxSpeed;
                    }
                    //Debug.Log(finalVelocity);
                }

                transform.Translate((m_parachuteToNextPosition.normalized * m_finalVelocity) * Time.deltaTime, Space.World);

                m_initialVelocity = m_finalVelocity;
            }
            else
            {
                //Debug.Log("DECELERATE");
                float decelerationRate = MaxSpeed / DecelerationTime;

                if (m_finalVelocity > 0)
                {
                    m_finalVelocity = m_initialVelocity - (decelerationRate * Time.deltaTime);

                    if (m_finalVelocity < MinSpeed)
                    {
                        //finalVelocity = 0;
                        m_nextPositionReached = false;
                    }
                    //Debug.Log(finalVelocity);
                }

                transform.Translate((m_lastDirection * m_finalVelocity) * Time.deltaTime, Space.World);

                m_initialVelocity = m_finalVelocity;
            }
        }
        else
        {
            transform.position = new Vector2(m_playerWhoCollected.transform.position.x, m_playerWhoCollected.transform.position.y + PositionOffsetWhileCollected);

            LaunchIfLost();
        }

        // First method, movement looks like its being remote controlled
        //m_parachuteRigidbody2D.position = Vector3.MoveTowards(m_parachuteRigidbody2D.position, m_nextPosition, Agility);

        // Second method, movement is better, but not right within our desired context
        //Vector3 parachuteToNextPositionNormalized = Vector3.Normalize(m_nextPosition - m_parachuteRigidbody2D.position);

        //float dotProduct = Vector3.Dot(transform.right, parachuteToNextPositionNormalized);

        //float directionOfRotation = Mathf.Sign(dotProduct) * -1;

        //transform.Translate(transform.up * ((Speed *(m_distance.magnitude * EasingMultiplier)) * Time.deltaTime), Space.World);

        //transform.Rotate(0, 0, (RotateSpeedInDegrees * Time.deltaTime) * directionOfRotation, Space.Self);
    }

    // Called in Movement();
    private void LaunchIfLost()
    {
        if (ButtonMasherObject.GetComponent<ButtonMasher>().WinningPlayer != null)
        {
            ButtonMasherObject.GetComponent<ButtonMasher>().ActivateObject(false);

            if (ButtonMasherObject.GetComponent<ButtonMasher>().WinningPlayer == m_playerWhoCollected)
            {
                // WIN CONDITION

            }
            else
            {
                // Set a new position
                m_nextPosition = new Vector3(Random.Range(-ScreenSizeX, ScreenSizeX), Random.Range(-ScreenSizeY, ScreenSizeY));

                // Increase the Speed, Decrease AccelerationTime so that object moves away faster
                MaxSpeed += AddedLaunchSpeed;

                // Set to false so that Movement returns
                m_collected = false;
            }           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            m_playerWhoCollected = other.gameObject;
            m_spriteRenderer.color = Color.white;
            m_collected = true;

            ButtonMasherObject.GetComponent<ButtonMasher>().ActivateObject(true);
        }
    }
}
