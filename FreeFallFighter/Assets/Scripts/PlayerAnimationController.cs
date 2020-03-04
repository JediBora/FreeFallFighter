using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Range(1,2)]
    public int PlayerNum;

    private int FacingDirH = 0;
    private int FacingDirV = -1;
    private Animator Panim;
    private Player1Script Param1;
    private Player2Script Param2;
    private PlayerMovement Pmove;
    private float MoveX;
    private float MoveY;
    // Start is called before the first frame update
    void Start()
    {
        Panim = gameObject.GetComponentInChildren<Animator>();

        if(PlayerNum == 1)
        {
            Param1 = gameObject.GetComponent<Player1Script>();
        }
        
        if(PlayerNum == 2)
        {
            Param2 = gameObject.GetComponent<Player2Script>();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerNum == 1)
        {
            MoveGet(new Vector2(Input.GetAxis("Player1Movement"), Input.GetAxis("Player1Upwards")));
            if(Input.GetButton("Player1Peck"))
            {
                PeckSet();
            }
        }

        if (PlayerNum == 2)
        {
            MoveGet(new Vector2(Input.GetAxis("Player2Movement"), Input.GetAxis("Player2Upwards")));
            if (Input.GetButton("Player2Peck"))
            {
                PeckSet();
            }
        }
        
        
    }


    void MoveGet(Vector2 input)
    {
        // Horizontal Movement
        if (input.x < 0)
        {
            Panim.SetInteger("FaceH", 0);
            Panim.SetInteger("FaceV", 0);

            Panim.SetInteger("ValX", 1);
            Panim.SetInteger("FaceH", 1);
        }
        if (input.x > 0)
        {
            Panim.SetInteger("FaceH", 0);
            Panim.SetInteger("FaceV", 0);

            Panim.SetInteger("ValX", -1);
            Panim.SetInteger("FaceH", -1);
        }
        if (input.x == 0)
        {
            Panim.SetInteger("ValX", 0);
            
        }

        // Vertical Movement
        if (input.y < 0)
        {
            Panim.SetInteger("FaceH", 0);
            Panim.SetInteger("FaceV", 0);

            Panim.SetInteger("ValY", -1);
            Panim.SetInteger("FaceV", -1);
        }
        if (input.y > 0)
        {
            Panim.SetInteger("FaceH", 0);
            Panim.SetInteger("FaceV", 0);

            Panim.SetInteger("ValY", 1);
            Panim.SetInteger("FaceV", 1);
        }
        if (input.y == 0)
        {
            Panim.SetInteger("ValY", 0);
            
        }
    }

    void PeckSet()
    {
        Panim.SetTrigger("PeckTrigger");
    }
}
