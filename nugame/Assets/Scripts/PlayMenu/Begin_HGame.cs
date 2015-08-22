using UnityEngine;
using System.Collections;
using System;
using System.Runtime;
using UnityEngine.UI;

public class Begin_HGame : MonoBehaviour {

	public GameObject a, b, c, d, e, f;

	public void verify()
	{
		//string err = "Invalid - ";
		bool dontstart = false;
		bool is_num = false;
		/*Debug.Log (a.GetComponent<InputField> ().text);
		Debug.Log (b.GetComponent<InputField> ().text);
		Debug.Log (c.GetComponent<InputField> ().text);*/

		//check name field
		if (a.GetComponent<InputField> ().text == "") {
			d.SetActive(true);
			dontstart = true;
		}

		//check player number field





		if (isnum (b.GetComponent<InputField> ().text))
		{
			if (!(Convert.ToInt32 (b.GetComponent<InputField> ().text, 10) >= 2 &&
				Convert.ToInt32 (b.GetComponent<InputField> ().text, 10) <= 64)) {

				e.SetActive (true);
				dontstart = true;
			}
		}

		else
		{
			dontstart = true;
			e.SetActive (true);
		}

		//check port field
		if (isnum (c.GetComponent<InputField> ().text)) {
			if (Convert.ToInt32 (c.GetComponent<InputField> ().text, 10) <= 1000) {
				f.SetActive (true);
				dontstart = true;
			}
		} 

		else 
		{
			dontstart = true;
			f.SetActive(true);
		}

		if (!dontstart) {
			Debug.Log ("START");
			//StartServer(a.GetComponent<InputField> ().text, b.GetComponent<InputField> ().text, c.GetComponent<InputField> ().text);
			// HACK
			if(GameObject.FindObjectOfType<NetworkManagerCustom>() != null)
				GameObject.FindObjectOfType<NetworkManagerCustom>().StartupHost(int.Parse(c.GetComponent<InputField> ().text));
		}
	}

	bool isnum(string t)
	{
		if (t.Length == 0)
			return false;

		for (int i = 0; i < t.Length; ++i) {
			if(t.Substring(i, 1) != "1" && t.Substring(i, 1) != "2" && t.Substring(i, 1) != "3" &&
			   t.Substring(i, 1) != "4" && t.Substring(i, 1) != "5" && t.Substring(i, 1) != "6" &&
			   t.Substring(i, 1) != "7" && t.Substring(i, 1) != "8" && t.Substring(i, 1) != "9" &&
			   t.Substring(i, 1) != "0")
			{
				return false;
			}
		}

		return true;
	}
}
