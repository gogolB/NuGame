using UnityEngine;
using System.Collections;
using System;
using System.Runtime;
using UnityEngine.UI;

public class Begin_JMatch : MonoBehaviour {

	public GameObject a, b, c, d, e, f, g, h;
	
	public void verify()
	{
		bool dontstart = false;
		//Debug.Log (a.GetComponent<InputField> ().text);
		//Debug.Log (b.GetComponent<InputField> ().text);
		
		//check IP field - work on this
		if (isnum (a.GetComponent<InputField> ().text) &&
			isnum (b.GetComponent<InputField> ().text) &&
			isnum (c.GetComponent<InputField> ().text) &&
			isnum (d.GetComponent<InputField> ().text) &&
			isnum (e.GetComponent<InputField> ().text)) {

			if (Convert.ToInt32 (a.GetComponent<InputField> ().text, 10) >= 256) {
				dontstart = true;
			}

			if (Convert.ToInt32 (b.GetComponent<InputField> ().text, 10) >= 256) {
				dontstart = true;
			}

			if (Convert.ToInt32 (c.GetComponent<InputField> ().text, 10) >= 256) {
				dontstart = true;
			}

			if (Convert.ToInt32 (d.GetComponent<InputField> ().text, 10) >= 256) {
				dontstart = true;
			}

			if (Convert.ToInt32 (e.GetComponent<InputField> ().text, 10) >= 65636) {
				dontstart = true;
			}
		} 

		else 
			dontstart = true;

		if (dontstart)
			g.SetActive (true);
		
		//check port field
		if (isnum (f.GetComponent<InputField> ().text)) {
			if (Convert.ToInt32 (f.GetComponent<InputField> ().text, 10) <= 1000) {
				h.SetActive (true);
				dontstart = true;
			}
		} 
		else 
		{
			h.SetActive (true);
			dontstart = true;
		}
		
		if (!dontstart) {
			Debug.Log ("START");
			/*NetworkManager.Connect((a.GetComponent<InputField> ().text + "."
			                        + b.GetComponent<InputField> ().text + "."
			                        + c.GetComponent<InputField> ().text + "."
			                       + d.GetComponent<InputField> ().text + "."
			                       + e.GetComponent<InputField>().text), 
			                       f.GetComponent<InputField>().text);*/
			if(GameObject.FindObjectOfType<NetworkManagerCustom>() != null)
				GameObject.FindObjectOfType<NetworkManagerCustom>().JoinGame((a.GetComponent<InputField> ().text + "."
				                                                              + b.GetComponent<InputField> ().text + "."
				                                                              + c.GetComponent<InputField> ().text + "."
				                                                              + d.GetComponent<InputField> ().text), 
				                                                             int.Parse(f.GetComponent<InputField>().text));
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
