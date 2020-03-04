using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player1Script : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    Vector2 movement;
    float move;
    public Collider2D collider;
    [Range(0, 1f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    public float timer = 4;
    public float force = 10.0f;
    public bool facingRight = true;
    Vector2 rightVector;
    Vector2 leftVector;
    public GameObject poop;
    public GameObject parachute;
    GameObject poops;
    public GameObject otherPlayer;
    public GameObject poopSpawn;
    public float chaseSpeed = 4;
    public bool deployPoop = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftVector = new Vector2(-1, 0);
        rightVector = new Vector2(1, 0);
    }
    private void Update()
    {
        Movement();
        Poop();
        Peck();
        WingAttack();



    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void Movement()
    {
        Vector3 targetVelocity = new Vector2(2 * 10f, rb.velocity.y);
        movement = Vector3.SmoothDamp(transform.position, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        movement.x = Input.GetAxis("Player1Movement");
        movement.y = Input.GetAxis("Player1Upwards");

        if (Input.GetAxis("Player1Movement") > 0)
        {
            facingRight = true;

        }
        if (Input.GetAxis("Player1Movement") < 0)
        {
            facingRight = false;

        }
    }

    public void Poop()  //player stuns the other player; disable the script; add an Invoke on the Update
    {
        if (Input.GetButtonDown("Player1Poop"))
        {
            deployPoop = true;
            poops = Instantiate(poop, transform.position, Quaternion.identity) as GameObject;
            Destroy(poops, 6f);
            deployPoop = false;

        }
        poops.transform.position = Vector2.MoveTowards(poops.transform.position, otherPlayer.transform.position, chaseSpeed * Time.deltaTime);
    }



    public void Peck()  //player attacks the other player
    {
        if (Input.GetButton("Player1Peck"))
        {
            collider.isTrigger = true;

        }

    }

    public void WingAttack()  //done
    {

        if (Input.GetButtonDown("Player1WingAttack"))
        {
            collider.isTrigger = true;
            if (facingRight)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(1, transform.position.y), 30);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-1, transform.position.y), 30);
            }

            Invoke("ChangeCollider", timer);


        }
    }

    void ChangeCollider()
    {
        collider.isTrigger = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parachute")
        {
            print("collide");
            //add the button mashing scene

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {

        }
    }
}
