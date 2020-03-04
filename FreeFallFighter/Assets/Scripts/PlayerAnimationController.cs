using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator Panim;
    private PlayerParams Param;
    private PlayerMovement Pmove;
    private float MoveX;
    private float MoveY;
    // Start is called before the first frame update
    void Start()
    {
        Panim = gameObject.GetComponentInChildren<Animator>();
        Param = gameObject.GetComponent<PlayerParams>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = Input.GetAxisRaw(Param.MovementAxis);
        if (MoveX < 0)
        {
            Panim.SetInteger("ValX", 1);
        }
        if (MoveX > 0)
        {
            Panim.SetInteger("ValX", -1);
        }
        if (MoveX == 0)
        {
            Panim.SetInteger("ValX", 0);
        }
    }
}
