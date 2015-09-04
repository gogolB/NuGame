using UnityEngine;
using System.Collections;

public class Player_Input : MonoBehaviour 
{
	private Vector3 moveDirection = Vector3.zero;

	private Player_Controller controller = null;

	private Camera playerCamera = null;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<Player_Controller> ();
		playerCamera = transform.FindChild ("Camera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		getMovementInput ();
		if(!Input.GetMouseButton(2))
			getRotationInput (); 

		if(Input.GetMouseButtonDown(0));
			//controller.fireBullet();
	}

	private void getMovementInput()
	{
		// TODO Need to change this from unity's input system to our own input system.
		// Only information that needs to be sent across the network. Player animations handle motion.
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		controller.setMoveDir (moveDirection);
	}

	private void getRotationInput()
	{
		// TODO Need to change this from unity's input system to our own input system.
		Ray ray = playerCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100f)) {
			controller.setMeshLookAt (hit.point);
		}
	}
}
