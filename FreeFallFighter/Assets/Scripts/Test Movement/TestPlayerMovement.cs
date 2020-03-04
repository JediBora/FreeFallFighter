using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 FloatDirection = new Vector2(0f, 1f);

    [SerializeField]
    private float FloatSpeed = 1f;
    [SerializeField]
    private float AccelerationTime = 1f;
    [SerializeField]
    private float DecelerationTime = 1f;
    [SerializeField]
    private float MaxSpeed = 1f;
    [SerializeField]
    private float MinSpeed = 0f;

    private Vector2 m_directionalInput;
    private Vector2 m_lastDirection;

    private Vector3 m_movementTranslation;

    private float m_initialVelocity;
    private float m_finalVelocity;

    private bool m_facingRight;


    private void Start()
    {

    }

    private void Update()
    {
        CollectInput();
        FlipSprite();
        Movement();
    }

    private void CollectInput()
    {
        m_directionalInput = new Vector2(Input.GetAxis("Player1Movement"), Input.GetAxis("Player1Upwards"));

        // PECK
        // WING ATTACK
        // POOP
    }

    private void FlipSprite()
    {
        if (m_directionalInput.x > 0)
        {
            m_facingRight = true;
        }
        else
        {
            m_facingRight = false;
        }
    }

    private void Movement()
    {

        // If player is giving input
        if (Mathf.Abs(m_directionalInput.x) > 0 || Mathf.Abs(m_directionalInput.y) > 0)
        {
            m_lastDirection = m_directionalInput;
            //Debug.Log($"Last Direction:{m_lastDirection}");

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

            transform.Translate((m_directionalInput.normalized * m_finalVelocity) * Time.deltaTime, Space.World);

            m_initialVelocity = m_finalVelocity;
        }
        else
        {
            //Debug.Log("DECELERATE");
            float decelerationRate = MaxSpeed / DecelerationTime;

            if (m_finalVelocity > MinSpeed)
            {
                m_finalVelocity = m_initialVelocity - (decelerationRate * Time.deltaTime);

                if (m_finalVelocity < MinSpeed)
                {
                    m_finalVelocity = MinSpeed;
                }
            }

            if (m_lastDirection.normalized.y < 0f)
            {
                FloatSpeed = 1.5f;
            }
            else
            {
                FloatSpeed = 0.5f;
            }

            Vector2 floatSum = FloatDirection * FloatSpeed;
            //Debug.Log($"Float Speed: {FloatSpeed}");
            //Debug.Log($"Float Sum: {floatSum}");
            //Debug.Log($"LDN: {m_lastDirection.normalized}");
            //Debug.Log($"LDN + FS: {m_lastDirection.normalized + floatSum}");
            //Debug.Log($"Final Velocity: {m_finalVelocity}");
            //Debug.Log($"Transltion:{(m_lastDirection.normalized + floatSum) * m_finalVelocity}");

            transform.Translate(((m_lastDirection.normalized + floatSum) * m_finalVelocity ) * Time.deltaTime, Space.World);

            m_initialVelocity = m_finalVelocity;
        }
    }


}
