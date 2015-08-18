using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testInp : MonoBehaviour {

	//public GameObject gc;
	public string ac;
	public string ke;

	public void RegCont(GameObject gc)
	{
		//public GameObject gc;
		ac = gameObject.GetComponent<sendInput> ().a;
		ke = gameObject.GetComponent<sendInput> ().k;

		gc.GetComponent<InputManager> ().RegisterControl (ac, ke);

	}

	public void BindCont(GameObject gc)
	{
		//public GameObject gc;
		ac = gameObject.GetComponent<sendInput> ().a;
		ke = gameObject.GetComponent<sendInput> ().k;
		
		gc.GetComponent<InputManager> ().BindControl (ac, ke);

	}

	public void GetCont(GameObject gc)
	{
		//public GameObject gc;
		ac = gameObject.GetComponent<sendInput> ().a;
		ke = gameObject.GetComponent<sendInput> ().k;
		
		if (gc.GetComponent<InputManager> ().GetControl (ac)) {
			Debug.Log ("TRUE");
		} else {
			Debug.Log ("FALSE");
		}
	}

	public void PrintCont(GameObject gc)
	{
		//public GameObject gc;
		ac = gameObject.GetComponent<sendInput> ().a;
		ke = gameObject.GetComponent<sendInput> ().k;
		
		gc.GetComponent<InputManager> ().print ();

	}



	// Update is called once per frame
	void Update () {
	
	}
}
