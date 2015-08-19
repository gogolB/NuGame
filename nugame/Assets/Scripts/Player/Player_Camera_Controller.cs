using UnityEngine;
using System.Collections;

// TODO only do calculations when the variables are dirty, otherwise don't. Saves on compute time.
public class Player_Camera_Controller : MonoBehaviour {


	[Range(0.0f, 80.0f)]
	[Tooltip("This is the angle formed by the forward vector of the camera and the ground plane.")]
	public float angleOfIncidence = 60f;

	[Range(0.0f, 50.0f)]
	[Tooltip("This is the distance from the camera to the player.")]
	public float armLength = 10f;

	[Tooltip("This is allows the player to move the camera with the right mouse button.")]
	public bool allowPlayerCameraRotation = false;

	[Range(1.0f, 50.0f)]
	[Tooltip("This is how sensitive the mouse is to player movement.")]
	public float mouseSensitivity = 5.0f;

	[Range(0.0f, 360.0f)]
	public float groundRotation = 180.0f;

	[Tooltip("What is this camera looking at.")]
	public GameObject target;

	// Use this for initialization
	void Start () 
	{
		if (target == null) {
			Debug.LogError("NO PLAYER TARGET SET FOR CAMERA. PLEASE SET TARGET.");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (allowPlayerCameraRotation && Input.GetMouseButton(2)) {
			groundRotation += Input.GetAxis("Mouse X") * mouseSensitivity;

			// Keep the value of the ground rotation between 0 and 360.
			if(groundRotation > 360f)
				groundRotation -= 360f;
			else if(groundRotation < 0.0f)
				groundRotation += 360f;
		}

		float height = Mathf.Sin(Mathf.Deg2Rad * angleOfIncidence) * armLength;
		float groundDist = Mathf.Cos(Mathf.Deg2Rad * angleOfIncidence) * armLength;


		float groundX = Mathf.Sin (Mathf.Deg2Rad * groundRotation) * groundDist;
		float groundZ = Mathf.Cos (Mathf.Deg2Rad * groundRotation) * groundDist;

		this.transform.localPosition = new Vector3 (groundX, height, groundZ);
		this.transform.LookAt (target.transform.position);
	}
}
