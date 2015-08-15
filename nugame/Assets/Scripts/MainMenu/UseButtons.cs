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
		int count = 0;
		if (PlayerPrefs.GetString ("Fire") == inti)
			count += 1;
		if (PlayerPrefs.GetString ("Up") == inti)
			count += 1;
		if (PlayerPrefs.GetString ("Down") == inti)
			count += 1;
		if (PlayerPrefs.GetString ("Left") == inti)
			count += 1;
		if (PlayerPrefs.GetString ("Right") == inti)
			count += 1;
		if (PlayerPrefs.GetString ("Interact") == inti)
			count += 1;

		if(count == 0)
			PlayerPrefs.SetString (Habla, inti);
	
	}

}
