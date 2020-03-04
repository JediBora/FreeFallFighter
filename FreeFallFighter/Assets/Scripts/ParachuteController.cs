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

    private float initialVelocity;
    private float finalVelocity;

    private Vector2 lastDirection;

    [SerializeField]
    private float ResetRange = 0;
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

    private Rigidbody2D m_parachuteRigidbody2D;

    private Vector2 m_parachuteToNextPosition;
    private Vector2 m_nextPosition;

    private bool m_nextPositionReached;

    private void Start()
    {
        m_parachuteRigidbody2D = GetComponent<Rigidbody2D>();

        m_parachuteRigidbody2D.position = new Vector2(0f, 6f);
        m_nextPosition = Vector2.zero;
        //Debug.Log($"Next Position: {m_nextPosition}");
    }

    private void Update()
    {
        SetRandomNextPosition();
        Movement();

        // Locks location of child sprite
        transform.GetChild(0).transform.rotation = Quaternion.identity;
    }

    private void SetRandomNextPosition()
    {
        m_parachuteToNextPosition = m_nextPosition - m_parachuteRigidbody2D.position;
        //Debug.Log($"ParachuteToNext: {m_parachuteToNextPosition}");
        //Debug.Log($"ParachuteToNextNormalized: {m_parachuteToNextPosition.normalized}");

        if (m_parachuteToNextPosition.magnitude < ResetRange)
        {
            m_nextPositionReached = true;
            m_nextPosition = new Vector3(Random.Range(-ScreenSizeX, ScreenSizeX), Random.Range(-ScreenSizeY, ScreenSizeY));
        }
    }

    private void Movement()
    {
        if (!m_nextPositionReached && m_parachuteToNextPosition.magnitude > ResetRange)
        {
            lastDirection = m_parachuteToNextPosition.normalized;
            //Debug.Log($"LastDirection: {lastDirection}");

            //Debug.Log("ACCELERATE");
            float accelerationRate = MaxSpeed / AccelerationTime;

            if (finalVelocity < MaxSpeed)
            {
                finalVelocity = initialVelocity + (accelerationRate * Time.deltaTime);

                if (finalVelocity > MaxSpeed)
                {
                    finalVelocity = MaxSpeed;
                }
                //Debug.Log(finalVelocity);
            }

            transform.Translate((m_parachuteToNextPosition.normalized * finalVelocity) * Time.deltaTime, Space.World);

            initialVelocity = finalVelocity;
        }
        else
        {
            //Debug.Log("DECELERATE");
            float decelerationRate = MaxSpeed / DecelerationTime;

            if (finalVelocity > 0)
            {
                finalVelocity = initialVelocity - (decelerationRate * Time.deltaTime);

                if (finalVelocity < 0)
                {
                    finalVelocity = 0;
                    m_nextPositionReached = false;
                }
                //Debug.Log(finalVelocity);
            }

            transform.Translate((lastDirection * finalVelocity) * Time.deltaTime, Space.World);

            initialVelocity = finalVelocity;
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
}
