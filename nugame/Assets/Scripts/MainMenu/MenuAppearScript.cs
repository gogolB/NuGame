using UnityEngine;
using System.Collections;

public class MenuAppearScript : MonoBehaviour {
	
	public GameObject menu; // Assign in inspector
	public GameObject optio;
	private bool isShowing;

	void Awake()
	{
		menu.SetActive (false);
	}
	
	void Update() {
		if (GetComponent<Display>().open != isShowing) {
			isShowing = !isShowing;
			menu.SetActive(isShowing);
		}
	}
}