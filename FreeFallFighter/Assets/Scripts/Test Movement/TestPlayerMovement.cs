using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    [Range(1, 2)]
    public int PlayerNum;


    [SerializeField]
    private Vector2 FloatDirection = new Vector2(0f, 1f);

    [SerializeField]
    private float FloatSpeed = 1f;
    [SerializeField]
    private float AccelerationTime = 1f;
    [SerializeField]
    private float DecelerationTime = 1f;
    [SerializeField]
    public float MaxSpeed = 1f;
    [SerializeField]
    private float MinSpeed = 0f;

    private Vector2 m_directionalInput;
    private Vector2 m_lastDirection;

    private Vector3 m_movementTranslation;



    private float m_initialVelocity;
    private float m_finalVelocity;

    private bool m_facingRight;
    private bool m_facingUp;

    //GameObjects
    public GameObject poop;
    private GameObject poops;
    public GameObject parachute;

    public GameObject player1;
    public GameObject player2;
    //public GameObject player1Sprite;
    //public GameObject player2Sprite;

    //Booleans
    public bool dashing = false;
    public bool peckActive = false;


    //Floats
    public float dashSpeed = 2;
    public float poopChaseSpeed = 4;
    private float poopCooldown = 2;
    private float poopTimerStart = 0;
    public float timer = 4;
    public float force = 4;

    //Vectors
    private Vector3 startPos;
    private Vector3 endPos;

    //SpriteRenderers
    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    //Colors
    public Color greyColor;

    //Colliders
    public Collider2D player1Collider;
    public Collider2D player2Collider;

    //Rigidbodies
    public Rigidbody2D player1rb;
    public Rigidbody2D player2rb;
    private bool CanDash = true;
    public bool stunned = false;
    //public bool stunned = false;
    private int ValX = 0;
    private int ValY = 0;
    private void Start()
    {

    }

    private void Update()
    {

        CollectInput();
        FlipSprite();
        Movement();

        // MECHNANICS
        Peck();
        WingAttack();
        Poop();

    }

    private void CollectInput()
    {


        if (PlayerNum == 1)
        {
            m_directionalInput = new Vector2(Input.GetAxis("Player1Movement"), Input.GetAxis("Player1Upwards"));


        }
        if (PlayerNum == 2)
        {
            m_directionalInput = new Vector2(Input.GetAxis("Player2Movement"), Input.GetAxis("Player2Upwards"));


        }



    }
    void Peck()// add knockback 
    {
        if (PlayerNum == 1 && Input.GetButton("Player1Peck") && peckActive)
        {
            if (m_facingRight && PlayerNum == 1)
            {

                player2rb.AddForce(transform.right * force, ForceMode2D.Impulse);
            }
            else if (!m_facingRight && PlayerNum == 1)
            {

                player2rb.AddForce(-transform.right * force, ForceMode2D.Impulse);
            }
            player2SpriteRenderer.color = Color.red;
            //otherRb.AddForce(transform.right * 1);
            StartCoroutine("WaitAndChangeColor2");
        }
        if (PlayerNum == 2 && Input.GetButton("Player2Peck") && peckActive)
        {
            if (m_facingRight && PlayerNum == 2)
            {

                player1rb.AddForce(transform.right * force, ForceMode2D.Impulse);
            }
            else if (!m_facingRight && PlayerNum == 2)
            {
                player1rb.AddForce(-transform.right * force, ForceMode2D.Impulse);
                //player1rb.AddForce(-transform.right * force);
            }
            player1SpriteRenderer.color = Color.red;

            StartCoroutine("WaitAndChangeColor1");
        }
    }

    IEnumerator WaitAndChangeColor1()
    {

        // suspend execution for 5 seconds
        yield return new WaitForSeconds(.75f);
        print("it works");
        player1SpriteRenderer.color = Color.white;
    }
    IEnumerator WaitAndChangeColor2()
    {

        // suspend execution for 5 seconds
        yield return new WaitForSeconds(.75f);
        print("it works");
        player2SpriteRenderer.color = Color.white;
    }
    void Poop() // add a cooldown and color
    {

        if (PlayerNum == 1 && Input.GetButtonDown("Player1Poop"))
        {
            poops = Instantiate(poop, transform.position, Quaternion.identity);
            Destroy(poops, 6f);
        }


        if (PlayerNum == 2 && Input.GetButtonDown("Player2Poop"))
        {
            poops = Instantiate(poop, transform.position, Quaternion.identity);
            Destroy(poops, 6f);
        }

        if (poops != null && PlayerNum == 1)
        {
            poops.transform.position = Vector2.MoveTowards(poops.transform.position, player2.transform.position, poopChaseSpeed * Time.deltaTime);


        }

        if (poops != null && PlayerNum == 2)
        {
            poops.transform.position = Vector2.MoveTowards(poops.transform.position, player1.transform.position, poopChaseSpeed * Time.deltaTime);
        }
    }

    void WingAttack()
    {
        //print(endPos);
        //print(Input.GetAxis("Player1Upwards"));

        if(PlayerNum == 1)
        {
            if (Input.GetButton("Player1WingAttack"))
            {
                player1Collider.isTrigger = true;
                player1SpriteRenderer.color = Color.white;
                startPos = player1.transform.position;


                if (ValX == -1)
                {
                    endPos = player1.transform.position + (Vector3.right);
                }
                if (ValX == 1)
                {
                    endPos = player1.transform.position + (Vector3.left);
                }
                if (ValY == -1)
                {
                    endPos = player1.transform.position + (Vector3.up);
                }
                if (ValY == 1)
                {
                    endPos = player1.transform.position + (Vector3.down);
                }


                player1.transform.position = Vector3.MoveTowards(player1.transform.position, endPos, dashSpeed);
                


                Invoke("ChangeCollider1", timer);


            }
        }
        
        if(PlayerNum == 2)
        {
            if (Input.GetButton("Player2WingAttack"))
            {
                player2Collider.isTrigger = true;
                player2SpriteRenderer.color = Color.white;
                startPos = player2.transform.position;


                if (ValX == -1)
                {
                    endPos = player2.transform.position + (Vector3.right);
                }
                if (ValX == 1)
                {
                    endPos = player2.transform.position + (Vector3.left);
                }
                if (ValY == -1)
                {
                    endPos = player2.transform.position + (Vector3.up);
                }
                if (ValY == 1)
                {
                    endPos = player2.transform.position + (Vector3.down);
                }


                player2.transform.position = Vector3.MoveTowards(player2.transform.position, endPos, dashSpeed);
                


                Invoke("ChangeCollider2", timer);


            }
        }

        
    }
    void ChangeCollider1()
    {
        player1Collider.isTrigger = false;
        player1SpriteRenderer.color = greyColor;
        
    }
    void ChangeCollider2()
    {
        player2Collider.isTrigger = false;
        player2SpriteRenderer.color = greyColor;
        
    }


    private void FlipSprite()
    {
        if( m_directionalInput.x < 0)
        {
            ValX = 1;
        }
        if (m_directionalInput.x > 0)
        {
            ValX = -1;
        }
        if (m_directionalInput.x == 0)
        {
            ValX = 0;
        }

        if (m_directionalInput.y < 0)
        {
            ValY = 1;
        }
        if (m_directionalInput.y > 0)
        {
            ValY = -1;
        }
        if (m_directionalInput.y == 0)
        {
            ValY = 0;
        }

    }

    private void Movement()
    {
        if (!stunned)
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

                transform.Translate(((m_lastDirection.normalized + floatSum) * m_finalVelocity) * Time.deltaTime, Space.World);

                m_initialVelocity = m_finalVelocity;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerNum == 1 && collision.gameObject.tag == "Parachute")
        {
            //print("collide");
            //add the button mashing scene

        }
        if (PlayerNum == 2 && collision.gameObject.tag == "Parachute")
        {
            //print("collide");
            //add the button mashing scene

        }


    }

}
