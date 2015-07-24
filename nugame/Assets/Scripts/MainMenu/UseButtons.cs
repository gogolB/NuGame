using UnityEngine;
using System.Collections;

public class UseButtons : MonoBehaviour {

	public string inti = "a";

	void Update()
	{
		if (Input.anyKeyDown) {
			inti += Input.inputString;
			inti = (inti[inti.Length-1]).ToString();
		}
	}

	public void SetButton (string Habla) 
	{
		Debug.Log (Habla + ", " + inti);
		PlayerPrefs.SetString (Habla, inti);
	
	}

}
