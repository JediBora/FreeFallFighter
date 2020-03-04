using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
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
    public GameObject otherPlayerSprite;
    public GameObject playerSprite;
    public float chaseSpeed = 4;
    public bool deployPoop = false;
    public bool dashing = false;
    public float dashSpeed;
    private Vector3 startPos;
    private Vector3 endPos;
    SpriteRenderer otherSpriteRenderer;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D otherRb;
    public bool peckActive = false;
    public Color greyColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftVector = new Vector2(-1, 0);
        rightVector = new Vector2(1, 0);
        otherSpriteRenderer = otherPlayerSprite.GetComponent<SpriteRenderer>();
        mySpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
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

        movement.x = Input.GetAxis("Player2Movement");
        movement.y = Input.GetAxis("Player2Upwards");

        if (Input.GetAxis("Player2Movement") > 0)
        {
            facingRight = true;

        }
        if (Input.GetAxis("Player2Movement") < 0)
        {
            facingRight = false;

        }
    }

    public void Poop()  //done
    {
        if (Input.GetButtonDown("Player2Poop") && !deployPoop)
        {
            deployPoop = true;
            poops = Instantiate(poop, transform.position, Quaternion.identity) as GameObject;
            Destroy(poops, 6f);
            deployPoop = false;

        }
        if (poops != null)
        {
            poops.transform.position = Vector2.MoveTowards(poops.transform.position, otherPlayer.transform.position, chaseSpeed * Time.deltaTime);
        }
    }



    public void Peck()  //player attacks the other player
    {
        if (Input.GetButton("Player2Peck") && peckActive)
        {
            otherSpriteRenderer.color = Color.red;
            otherRb.AddForce(transform.right * 1);
            Invoke("DelayColor", 3);
        }

    }
    void DelayColor()
    {
        otherSpriteRenderer.color = greyColor;


    }
    public void WingAttack()  
    {

        if (Input.GetButtonDown("Player2WingAttack"))
        {
            collider.isTrigger = true;
            mySpriteRenderer.color = Color.white;
            startPos = transform.position;
            if (facingRight)
            {
                endPos = transform.position + (Vector3.right);
            }
            else
            {
                endPos = transform.position + (Vector3.left);
            }


            transform.position = Vector3.MoveTowards(transform.position, endPos, dashSpeed);

         

            Invoke("ChangeCollider", timer);


        }
    }

    void ChangeCollider()
    {
        collider.isTrigger = false;
        mySpriteRenderer.color = greyColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parachute")
        {
            print("collide");
            //add the button mashing scene

        }


    }

}
