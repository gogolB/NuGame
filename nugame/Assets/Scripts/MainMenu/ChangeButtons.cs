using UnityEngine;
using System.Collections;

public class ChangeButtons : MonoBehaviour {

	bool open = false;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseUp()
	{
		if (open == false)
			open = true;
	}

	void OnGui()
	{
		//if(!open)
		{
			if(GUI.Button(new Rect(gameObject.transform.position.x, gameObject.transform.position.y, 100, 30), "Fire"))
			{
				Debug.Log ("Fire!!!");
			}
			//if(GUI.Button.newRect(10,100, 100, 30), "Fire");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
