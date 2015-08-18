using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BindButt : MonoBehaviour {

	public string action;
	public GameObject g;

	public void Binder(GameObject Infield)
	{
		string key = Infield.GetComponent<InputField> ().text;
		g.GetComponent<InputManager> ().BindControl (action, key);
			
	}
}
