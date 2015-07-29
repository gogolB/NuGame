using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Network_Setup : NetworkBehaviour {

	[SerializeField]AudioListener audioListner;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<CharacterController>().enabled = true;
			GetComponent<Player_Controller>().enabled = true;
			audioListner.enabled = true;
		}
	}

}
