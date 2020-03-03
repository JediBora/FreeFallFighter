using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{
    [SerializeField]
    private float ResetRange = 0;
    [SerializeField]
    private float Speed = 1.0f;
    [SerializeField]
    private float RotateSpeedInDegrees = 45.0f;
    [SerializeField]
    private float EasingMultiplier = 1f;

    [SerializeField]
    private float ScreenSizeX = 5.5f;
    [SerializeField]
    private float ScreenSizeY = 4.3f;

    private Rigidbody2D m_parachuteRigidbody2D;

    private Vector2 m_distance;

    private Vector2 m_nextPosition;

    private void Start()
    {
        m_parachuteRigidbody2D = GetComponent<Rigidbody2D>();

        m_parachuteRigidbody2D.position = new Vector2(0f, ScreenSizeY + 1);
        m_nextPosition = Vector2.zero;
        //Debug.Log($"Next Position: {m_nextPosition}");
    }

    private void Update()
    {
        SetRandomNextPosition();
        Movement();
    }

    private void SetRandomNextPosition()
    {
        m_distance = m_parachuteRigidbody2D.position - m_nextPosition;

        if (m_distance.magnitude < ResetRange)
        {
            m_nextPosition = new Vector3(Random.Range(-ScreenSizeX, ScreenSizeX), Random.Range(-ScreenSizeY, ScreenSizeY));
        }
    }

    private void Movement()
    {
        // First method, movement looks like its being remote controlled
        //m_parachuteRigidbody2D.position = Vector3.MoveTowards(m_parachuteRigidbody2D.position, m_nextPosition, Agility);

        Vector3 parachuteToNextPositionNormalized = Vector3.Normalize(m_nextPosition - m_parachuteRigidbody2D.position);

        float dotProduct = Vector3.Dot(transform.right, parachuteToNextPositionNormalized);

        float directionOfRotation = Mathf.Sign(dotProduct) * -1;

        transform.Translate(transform.up * ((Speed *(m_distance.magnitude * EasingMultiplier)) * Time.deltaTime), Space.World);

        transform.Rotate(0, 0, (RotateSpeedInDegrees * Time.deltaTime) * directionOfRotation, Space.Self);
    }
}
