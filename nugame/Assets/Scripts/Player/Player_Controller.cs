using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	private Vector3 moveDirection = Vector3.zero;
	void Update() {

		CharacterController controller = GetComponent<CharacterController>();

		// TODO Need to change this from unity's input system to our own input system.
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		// If we are not on the ground, get us to the ground.
		if (!controller.isGrounded)
			moveDirection.y -= 20;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
