using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
	// For Development only. only.
	public enum Relative { mesh, camera, world};

	[Tooltip("Against what should the movement be transformed by?")]
	public Relative rel = Relative.camera;

	public float speed = 6.0F;

	private Vector3 moveDirection = Vector3.zero;

	private CharacterController controller;

	private Transform meshTrans;

	private Transform cameraTrans;

	void Start(){
		controller = GetComponent<CharacterController>();
		meshTrans = this.transform.FindChild ("Mesh").transform;
		cameraTrans = this.transform.FindChild ("Camera").transform;
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
			moveDirection = cameraTrans.TransformDirection(moveDirection);
			break;
		}
		moveDirection *= speed;

		// If we are not on the ground, get us to the ground.
		if (!controller.isGrounded)
			moveDirection.y -= 20;
		// Otherwise we are grounded and we don't need to change our Y height.
		else
			moveDirection.y = 0;

		// Finally go ahead and move us.
		controller.Move(moveDirection * Time.deltaTime);
	}

	// Use to set the movement director. 
	public void setMoveDir(Vector3 movDir)
	{
		this.moveDirection = movDir;
	}

	// Use to set the position in the world the character model should look at. 
	public void setMeshLookAt(Vector3 Target)
	{
		meshTrans.LookAt (Target);
		Vector3 newAdjustedAngle = new Vector3(0, meshTrans.eulerAngles.y, 0);
		meshTrans.eulerAngles = newAdjustedAngle;

	}
}
