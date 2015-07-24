using UnityEngine;
using System.Collections;

public class Display : MonoBehaviour {
	
	public GameObject ui;

	public bool open = false;

	public char kree;

	void OnMouseUp()
	{
		GetComponent<AudioSource> ().Play ();

		if (open == false)
			open = true;
		else
			open = false;
	}

	// Update is called once per frame
	void Update () {

		if (open) {
			double sca = 1.0f;
			//ui.GetComponentsInChildren<RectTransform>(true).

		}
		else
		{
			double sca = 0.0f;
			//ui.GetComponentsInChildren<
			
		}
	
	}
}
