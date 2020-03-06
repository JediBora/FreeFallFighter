using System.Collections;
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
    private float EnteringSpeed = 1f;

    [SerializeField]
    private float PositionResetRange = 0.1f;
    [SerializeField]
    private float SlowDownRange = 0.1f;
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
    public GameObject Player01;
    public GameObject Player02;

    private GameObject m_playerWhoCollected;
    private GameObject m_playerWhoCollectedLast;
    private SpriteRenderer m_spriteRenderer;

    private float m_initialVelocity;
    private float m_finalVelocity;

    private Vector2 m_parachuteToNextPosition;
    private Vector2 m_lastDirection;
    private Vector3 m_nextPosition;
    private Vector3 m_directionOpposite;

    private bool m_nextPositionReached;
    private bool m_startPositionReached;

    public bool m_collected;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = new Vector2(0f, 6f);
        m_nextPosition = Vector2.zero;
        m_startPositionReached = false;

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
        // DETERMINE DIRECTION OPPOSITE OF NEAREST PLAYER

        Vector3 ParachuteToPlayer01 = Player01.transform.position - transform.position;
        Vector3 ParachuteToPlayer02 = Player02.transform.position - transform.position;

        float closestPlayer = Mathf.Min(ParachuteToPlayer01.magnitude, ParachuteToPlayer02.magnitude);

        if (closestPlayer == ParachuteToPlayer01.magnitude)
        {
            m_directionOpposite = -ParachuteToPlayer01;
        }
        else if (closestPlayer == ParachuteToPlayer02.magnitude)
        {
            m_directionOpposite = -ParachuteToPlayer02;
        }
        else
        {
            Debug.LogError("NO player is close");
        }

        m_parachuteToNextPosition = m_directionOpposite;
    }

    private void Movement()
    {
        if (!m_collected)
        {
            if (m_startPositionReached)
            {
                //Debug.Log($"DistanceBetweenPlayers: {m_parachuteToNextPosition.magnitude}" + $" Slow Down Range: {SlowDownRange}");
                //if (!m_nextPositionReached && m_parachuteToNextPosition.magnitude > PositionResetRange)
                if (m_parachuteToNextPosition.magnitude < SlowDownRange)
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
                }

                transform.Translate((m_lastDirection * m_finalVelocity) * Time.deltaTime, Space.World);

                m_initialVelocity = m_finalVelocity;
            }
            else
            {
                IntroMovement();
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
    private void IntroMovement()
    {
        m_parachuteToNextPosition = m_nextPosition - transform.position;
        //Debug.Log($"ParachuteToNext: {m_parachuteToNextPosition}");
        //Debug.Log($"ParachuteToNextNormalized: {m_parachuteToNextPosition.normalized}");

        transform.position = Vector3.Lerp(transform.position, m_nextPosition, EnteringSpeed) * Time.deltaTime;

        if (m_parachuteToNextPosition.magnitude < PositionResetRange)
        {
            // INITIAL START POSITION
            m_startPositionReached = true;

            // DETERMINE DIRECTION RANDOMLY 
            //m_nextPositionReached = true;
            //m_nextPosition = new Vector3(Random.Range(-ScreenSizeX, ScreenSizeX), Random.Range(-ScreenSizeY, ScreenSizeY));
        }
    }

    // Called in Movement();
    private void LaunchIfLost()
    {
        if (ButtonMasherObject.GetComponent<ButtonMasher>().WinningPlayer != null)
        {
            if (ButtonMasherObject.GetComponent<ButtonMasher>().WinningPlayer.name == m_playerWhoCollected.name)
            {
                // WIN ANIMATION
                
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

            ButtonMasherObject.GetComponent<ButtonMasher>().ActivateObject(false, m_playerWhoCollected);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            //iTween.
            m_playerWhoCollected = other.gameObject;
            m_spriteRenderer.color = Color.white;
            m_collected = true;

            if (m_playerWhoCollectedLast != null)
            {
                ButtonMasherObject.GetComponent<ButtonMasher>().ActivateObject(false, m_playerWhoCollectedLast);
            }
            
            ButtonMasherObject.GetComponent<ButtonMasher>().ActivateObject(true, m_playerWhoCollected);       

            m_playerWhoCollectedLast = m_playerWhoCollected;
        }
    }
}
