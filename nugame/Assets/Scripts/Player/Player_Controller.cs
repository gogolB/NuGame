using UnityEngine;
using System.Collections;
using System;

public class Player_Controller : MonoBehaviour
{
	[Serializable]
	public class CharacterSettings
	{
		public float MoveForward = 1.0f;
		public float MoveBackward = 1.0f;
		public float StrafeLeft = 1.0f;
		public float StrafeRight = 1.0f;
	}
	// For Development only. only.
	public enum Relative { mesh, camera, world};

	[Tooltip("Against what should the movement be transformed by?")]
	public Relative rel = Relative.camera;

	public float speed = 6.0F;

	private Vector3 moveDirection = Vector3.zero;

	private CharacterController controller;

	private Transform meshTrans;

	private Transform cameraTrans;

	public CharacterSettings settings;

	void Start(){
		controller = GetComponent<CharacterController>();
		meshTrans = this.transform.FindChild ("Mesh").transform;
		cameraTrans = this.transform.FindChild ("Camera").transform;
		settings = new CharacterSettings();
	}


	void Update() {

		// If this is enabled it will allow players to move relative to how the character is
		// facing in the world. Otherwise it will move how relative to how the camera is 
		// facing the world. And if none of those, it will move based off of the world's
		// orientation.
		switch(rel)
		{
		case Relative.mesh:
			moveDirection = meshTrans.TransformDirection(moveDirection);
			break;
		case Relative.camera:
			moveDirection = cameraTrans.TransformDirection(moveDirection);
			break;
		default:
		case Relative.world:
			moveDirection = this.transform.TransformDirection(moveDirection);
			break;
		}
		moveDirection *= speed;

		// If we are not on the ground, get us to the ground.
		if (!controller.isGrounded)
			moveDirection.y -= 20;
		// Otherwise we are grounded and we don't need to change our Y height.
		else
			moveDirection.y = 0;


		move();
	}

	public void move()
	{
		// Adjust the move speed.
		if(moveDirection.z > 0)
			moveDirection *= settings.MoveForward;
		else if(moveDirection.z < 0)
			moveDirection *= settings.MoveBackward;

		if (moveDirection.x > 0)
			moveDirection *= settings.StrafeRight;
		else if(moveDirection.x < 0)
			moveDirection *= settings.StrafeLeft;

		this.controller.SimpleMove(moveDirection);
	}

	// Use to set the movement director. 
	// Calculates the correct animation to play.
	public void setMoveDir(Vector3 movDir)
	{
		this.moveDirection = movDir;

		switch(rel)
		{
		case Relative.mesh:
			movDir = meshTrans.TransformDirection(movDir);
			break;
		case Relative.camera:
			movDir = cameraTrans.TransformDirection(movDir);
			break;
		default:
		case Relative.world:
			movDir = this.transform.TransformDirection(movDir);
			break;
		}

		this.GetComponentInChildren<Animator>().SetFloat("Horizontal", movDir.x);
		this.GetComponentInChildren<Animator>().SetFloat("Vertical", movDir.z);
	}

	// Use to set the position in the world the character model should look at. 
	public void setMeshLookAt(Vector3 Target)
	{
		meshTrans.LookAt (Target);
		Vector3 newAdjustedAngle = new Vector3(0, meshTrans.eulerAngles.y, 0);
		meshTrans.eulerAngles = newAdjustedAngle;

	}

	public void fireBullet()
	{
		GameObject bullet = GameObject.FindObjectOfType<BulletPoolManager>().createNewBulletInstance();
		bullet.transform.position = this.transform.position;
		bullet.transform.position += meshTrans.forward + Vector3.up;
		bullet.GetComponent<BulletController>().fwd = meshTrans.forward;

		bullet.SetActive(true);
	}

	void OnAnimatorMove() {
		Animator animator = GetComponentInChildren<Animator>();
		if (animator) {
			Vector3 newPosition = transform.position;
			newPosition.y += animator.deltaPosition.y;
			newPosition.x += animator.deltaPosition.x;
			transform.position = newPosition;
		}
	}
}
