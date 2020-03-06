using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour {

	public static bool isFalling = false;

	public static bool detectGround = false;

	public static bool onGround = false;
	public static bool onCeiling = false;
	public static bool onWall = false;
	public static bool onJumpbleWall = false;

	public static bool moveRight = false;
	public static bool turnRight = false;
	public static bool isMoving = false;

	public static Vector2 direction;

	public static bool isJump = false;
	public static bool isWallJump = false;
	public static bool isSecondJump = false;

	public static float groundHitPoint;
	public static float lastGroundPosition;

}

