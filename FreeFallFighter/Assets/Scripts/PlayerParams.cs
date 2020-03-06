using UnityEngine;
using System.Collections;

public class PlayerParams : MonoBehaviour {

	public bool disable;
	public bool doubleJump;
	public bool wallJump;

	[HideInInspector]
	public bool disableMove;

	public LayerMask surfaceLayer;

	[Header("Tags")]
	public string jumpWallTag;
	public string movingPlatform;

	[Header("Player settings")]
	public float playerWidth = 1f;
	public float playerHeight = 1f;
	public float centerOffset = 0.0f;
	public float speed = 5;
	public int rayCount = 8;
	
	public float groundAccelerate = 1.0f;
	public float airAccelerate = 1.0f;
	
	public float gravityAccelerate = 10;
	public float wallSlideAccelerate = 20;
	
	public float jumpSpeed = 7;
	public float wallJumpSpeed = 7;
	public float jumpAccelerate = 20;
	public float wallJumpAccelerate = 8;
	public float jumpInterval = 0.1f;
	
	public float maxFallSpeed = 25f;
	public float wallFallSpeed = 2f;

	[Header("Input")]
	public string MovementAxis;
	public string JumpAxis;

	public void DisablePlayer (){
		disable = true;
	}

	public void EnablePlayer(){
		disable = false;
	}
}
