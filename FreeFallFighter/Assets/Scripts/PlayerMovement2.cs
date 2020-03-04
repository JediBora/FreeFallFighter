using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerParams2))]
public class PlayerMovement2 : MonoBehaviour
{
    //Ray stuffs
    private RaycastHit2D hit;

    private float wallMargin = 0.1f;

    //Player states
    private bool jumpPress = false;
    private bool movePress = false;
    private bool peckAttack = false;
    private bool isCollidedWithPlayer = false;


    //current states
    private float currentSpeed = 0.0f;
    private float currentGravity = 0.0f;
    private float currentJumpSpeed = 0.0f;
    private float currentWallJumpSpeed = 0.0f;
    private float currentAcc = 0.0f;
    private float currentJumpCount = 0;

    private float gravity = 0;

    private Vector2 prevDirection;

    private float currentJumpInterval;
    private bool canJump = true;

    //Refs
    public PlayerParams2 playerParams2;
    
    private Transform playerTransform;
    private Transform platform;
    private Transform prevPlatform;

    public SpriteRenderer mySpriteRenderer;
    private Collider2D collider;



    float x;
    void Awake()
    {

        currentSpeed = 0;
        playerParams2 = GetComponent<PlayerParams2>();

        playerTransform = transform;
        prevDirection = PlayerStates.direction;
        PlayerStates.moveRight = true;
    }

    void OnDrawGizmos()
    {
        playerParams2 = GetComponent<PlayerParams2>();
        Gizmos.DrawWireCube(transform.position, new Vector3(playerParams2.playerWidth, playerParams2.playerHeight, 1));
    }


    public void Gravity()
    {
        if (!PlayerStates.isJump)
        {

            if (PlayerStates.onJumpbleWall && !PlayerStates.onGround)
            {
                gravity = playerParams2.wallFallSpeed;
            }
            else
            {

                gravity = playerParams2.maxFallSpeed;
            }

            if (currentGravity < gravity)
            {
                currentGravity += playerParams2.gravityAccelerate * Time.deltaTime;
            }
            else
            {
                currentGravity = gravity;
            }

            GroundCheker();

            if (!PlayerStates.detectGround)
            {

                playerTransform.Translate(0, -currentGravity * Time.deltaTime, 0);

                PlayerStates.isFalling = true;
                PlayerStates.onGround = false;
            }
            else
            {
                playerTransform.Translate(0, hit.point.y - (playerTransform.position.y - playerParams2.playerHeight / 2), 0);
                PlayerStates.onGround = true;
                PlayerStates.isJump = false;
                PlayerStates.isFalling = false;
                currentGravity = 0.0f;
                currentJumpSpeed = 0.0f;
            }
        }
        else
        {
            PlayerStates.onGround = false;
        }
    }

    public void Moving()
    {

        prevDirection = PlayerStates.direction;

        //Handle Inputs************************
        if (Input.GetAxisRaw(playerParams2.MovementAxis) > 0)
        {

            if (!playerParams2.disableMove)
            {
                PlayerStates.moveRight = true;
                PlayerStates.isMoving = true;
                movePress = true;
            }
        }

        if (Input.GetAxisRaw(playerParams2.MovementAxis) < 0)
        {
            if (!playerParams2.disableMove)
            {

                PlayerStates.moveRight = false;
                PlayerStates.isMoving = true;
                movePress = true;
            }
        }

        if (Input.GetAxisRaw(playerParams2.MovementAxis) == 0)
        {
            PlayerStates.isMoving = false;
            movePress = false;
            playerParams2.disableMove = false;
        }

        //**************************************

        if (PlayerStates.detectGround)
        {
            currentAcc = playerParams2.groundAccelerate;
        }
        else
        {
            currentAcc = playerParams2.airAccelerate;
        }

        if (movePress && !playerParams2.disableMove)
        {

            if (PlayerStates.moveRight)
            {
                PlayerStates.direction = Vector2.right;

                if (currentSpeed < playerParams2.speed)
                {
                    currentSpeed += (currentAcc * Time.deltaTime);
                }
                if (currentSpeed > playerParams2.speed)
                {
                    currentSpeed = playerParams2.speed;
                }
            }

            if (!PlayerStates.moveRight)
            {
                PlayerStates.direction = -Vector2.right;

                if (currentSpeed < playerParams2.speed)
                {
                    currentSpeed += (currentAcc * Time.deltaTime);
                }
                if (currentSpeed > playerParams2.speed)
                {
                    currentSpeed = playerParams2.speed;
                }
            }
        }

        if (!movePress)
        {

            if (currentSpeed > 0)
            {
                currentSpeed -= (currentAcc * Time.deltaTime);
            }
            else
            {
                currentSpeed = 0;
            }
        }

        if (PlayerStates.direction != prevDirection)
        {
            currentSpeed = 0.0f;
        }


        WallCheker(currentSpeed);


        if (PlayerStates.onWall)
        {
            currentSpeed = 0.0f;
            if (PlayerStates.moveRight)
            {
                playerTransform.Translate(hit.point.x - (playerTransform.position.x + ((playerParams2.playerWidth / 2) + wallMargin)), 0, 0);
            }
            if (!PlayerStates.moveRight)
            {
                playerTransform.Translate(hit.point.x - (playerTransform.position.x - ((playerParams2.playerWidth / 2) + wallMargin)), 0, 0);
            }

            else
            {

                if (!playerParams2.disableMove)
                {
                    playerTransform.Translate(currentSpeed * PlayerStates.direction.x * Time.deltaTime, 0, 0);
                }
            }
        }
        else
        {
            if (!playerParams2.disableMove)
            {
                playerTransform.Translate(currentSpeed * PlayerStates.direction.x * Time.deltaTime, 0, 0);
            }
        }
    }


    //JUMP
    public void Jump()
    {

        if (PlayerStates.detectGround)
        {
            PlayerStates.isSecondJump = false;
            currentJumpCount = 0;
            currentWallJumpSpeed = 0;

            if (Timer())
            {
                canJump = true;
            }
        }

        //Check if  jump button not yet pressed
        if (!jumpPress)
        {

            //GROUND_JUMP
            if ((Input.GetAxisRaw(playerParams2.JumpAxis) != 0) && !PlayerStates.isJump && PlayerStates.detectGround && canJump)
            {
                jumpPress = true;
                PlayerStates.onGround = false;
                PlayerStates.lastGroundPosition = PlayerStates.groundHitPoint;
                currentJumpCount += 1;
                PlayerStates.isJump = true;
                PlayerStates.detectGround = false;
                currentJumpSpeed = playerParams2.jumpSpeed;
                canJump = false;
                currentJumpInterval = playerParams2.jumpInterval;
            }


            //SECOND_JUMP
            if (playerParams2.doubleJump)
            {//if Double jump allowed (see player params)
                if ((Input.GetAxis(playerParams2.JumpAxis) != 0) && (currentJumpCount == 1) && !jumpPress && !PlayerStates.detectGround && !PlayerStates.onWall)
                {
                    jumpPress = true;
                    PlayerStates.isJump = true;
                    PlayerStates.isSecondJump = true;
                    currentJumpCount = 0;
                    currentGravity = 0.0f;
                    currentJumpSpeed = playerParams2.jumpSpeed;
                    canJump = false;
                    currentJumpInterval = playerParams2.jumpInterval;
                }
            }


            //WALL_JUMP
            if (playerParams2.wallJump)
            {//if Wall jump allowed (see player params)
                if ((Input.GetAxis(playerParams2.JumpAxis) != 0) && !PlayerStates.detectGround && PlayerStates.onJumpbleWall && !PlayerStates.isJump)
                {
                    PlayerStates.isJump = true;
                    PlayerStates.isWallJump = true;
                    currentGravity = 0.0f;
                    currentJumpSpeed = playerParams2.jumpSpeed;
                    currentWallJumpSpeed = playerParams2.wallJumpSpeed;
                    movePress = false;
                    playerParams2.disableMove = true;
                    canJump = false;
                    currentJumpInterval = playerParams2.jumpInterval;
                    currentSpeed = 0.0f;

                    if (PlayerStates.moveRight)
                    {
                        PlayerStates.direction = -Vector2.right;
                        PlayerStates.moveRight = false;
                    }
                    else
                    {
                        PlayerStates.direction = Vector2.right;
                        PlayerStates.moveRight = true;
                    }
                }
            }
        }

        //if we release jump button
        if (Input.GetAxisRaw(playerParams2.JumpAxis) == 0)
        {
            jumpPress = false;
        }


        //Speed down ground jump power 
        if (currentJumpSpeed > 0)
        {
            currentJumpSpeed -= playerParams2.jumpAccelerate * Time.deltaTime;
        }
        else
        {
            currentJumpSpeed = 0.0f;
            PlayerStates.isJump = false;
        }

        //Speed down wall jump power 
        if ((currentWallJumpSpeed > 0) && PlayerStates.isWallJump)
        {
            currentWallJumpSpeed -= playerParams2.wallJumpAccelerate * Time.deltaTime;
        }
        else
        {
            currentWallJumpSpeed = 0.0f;
            PlayerStates.isWallJump = false;
        }

        //Function for detecting ceiling;
        ceilingCheck();


        //Translate character
        if (!PlayerStates.onCeiling)
        {
            playerTransform.Translate(0, currentJumpSpeed * Time.deltaTime, 0);
        }
        else
        {
            playerTransform.Translate(0, hit.point.y - (playerTransform.position.y + playerParams2.playerHeight / 2), 0);
            PlayerStates.isJump = false;
            currentJumpCount = 0;
        }

        //Function for detecting walls;
        WallCheker(currentWallJumpSpeed);

        if (PlayerStates.isWallJump)
        {

            if (!PlayerStates.onWall)
            {
                playerTransform.Translate(currentWallJumpSpeed * PlayerStates.direction.x * Time.deltaTime, 0, 0);
            }
            else
            {
                currentWallJumpSpeed = 0;
                if (PlayerStates.moveRight)
                {
                    playerTransform.Translate(hit.point.x - (playerTransform.position.x + ((playerParams2.playerWidth / 2) + wallMargin)), 0, 0);
                }

                if (!PlayerStates.moveRight)
                {
                    playerTransform.Translate(hit.point.x - (playerTransform.position.x - ((playerParams2.playerWidth / 2) + wallMargin)), 0, 0);
                }
            }
        }
    }//END_OF_JUMP


    public bool Timer()
    {

        if (currentJumpInterval > 0)
        {
            currentJumpInterval -= Time.deltaTime;
            return false;
        }
        else
        {
            currentJumpInterval = 0;
            return true;
        }
    }
    void Flip()
    {
        if (PlayerStates.moveRight)
        {
            mySpriteRenderer.flipX = false;
        }
        else
        {
            mySpriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // print(PlayerStates.moveRight);
        if (!playerParams2.disable)
        {
            Gravity();
            Moving();
            Jump();
        }
        print(Input.GetAxisRaw(playerParams2.UpwardAxis));
        //Flip();
    }

    public void GroundCheker()
    {

        float originX = playerTransform.position.x - playerParams2.playerWidth / 2;
        float originY = playerTransform.position.y - playerParams2.playerHeight / 2;

        for (int i = 0; i < playerParams2.rayCount + 1; i++)
        {

            hit = Physics2D.Raycast(new Vector2(originX, originY), -Vector2.up, currentGravity * Time.deltaTime, playerParams2.surfaceLayer);

            Debug.DrawRay(new Vector2(originX, originY), -Vector2.up * currentGravity * Time.deltaTime);

            originX += (playerParams2.playerWidth / playerParams2.rayCount);

            if (hit.collider != null)
            {

                PlayerStates.groundHitPoint = hit.point.y;
                PlayerStates.detectGround = true;

                prevPlatform = platform;
                platform = hit.collider.transform;

                if (prevPlatform != platform)
                {
                    transform.parent = null;

                    if (platform.tag == playerParams2.movingPlatform)
                    {
                        transform.parent = platform;
                    }
                }
                break;
            }
            else
            {

                PlayerStates.detectGround = false;
                continue;
            }
        }
    }

    public void WallCheker(float dist)
    {

        float originX = playerTransform.position.x;
        float originY = (playerTransform.position.y + (playerParams2.playerHeight / 2)) - 0.02f;

        if (PlayerStates.moveRight)
        {
            originX += (playerParams2.playerWidth / 2) + wallMargin;

        }
        else
        {
            originX -= (playerParams2.playerWidth / 2) + wallMargin;

        }

        for (int i = 0; i < playerParams2.rayCount + 1; i++)
        {

            hit = Physics2D.Raycast(new Vector2(originX, originY), PlayerStates.direction, dist * Time.deltaTime, playerParams2.surfaceLayer);

            Debug.DrawRay(new Vector2(originX, originY), PlayerStates.direction * dist * Time.deltaTime);

            originY = originY - (playerParams2.playerHeight - 0.04f) / playerParams2.rayCount;

            if (hit.collider != null)
            {

                PlayerStates.onWall = true;

                if (hit.collider.tag == playerParams2.jumpWallTag)
                {
                    PlayerStates.onJumpbleWall = true;
                }

                break;

            }
            else
            {
                PlayerStates.onJumpbleWall = false;
                PlayerStates.onWall = false;
            }
        }
    }

    public void ceilingCheck()
    {

        float originX = playerTransform.position.x - playerParams2.playerWidth / 2;
        float originY = playerTransform.position.y + playerParams2.playerHeight / 2;

        for (int i = 0; i < playerParams2.rayCount; i++)
        {

            originX += (playerParams2.playerWidth / playerParams2.rayCount);

            hit = Physics2D.Raycast(new Vector2(originX, originY), Vector2.up, currentJumpSpeed * Time.deltaTime, playerParams2.surfaceLayer);

            if (hit.collider != null)
            {
                PlayerStates.onCeiling = true;
                break;
            }
            else
            {
                PlayerStates.onCeiling = false;
                continue;
            }
        }
    }
    public void Poop()  //player stuns the other player; disable the script; add an Invoke on the Update
    {

        //playerParams.disable = true;
    }
    public void Peck()  //player attacks the other player
    {
        peckAttack = true;

    }

    public void WingAttack()  //player dodges the other player's attack; add an Invoke on the update
    {
        collider.isTrigger = true;

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
        if (playerParams2.disable == true) //push the enemy away
        {
            if (collision.gameObject.name == "Player2")
            {
                isCollidedWithPlayer = true;
            }

        }

    }
}