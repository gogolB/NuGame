using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sendInput : MonoBehaviour {

	public GameObject act_in;
	public GameObject key_in;

	public string a;
	public string k;


	// Use this for initialization
	void Start () {

		InputField input = act_in.GetComponent<InputField> ();
		InputField onput = key_in.GetComponent<InputField>();
		input.onEndEdit.AddListener (SubmitName);
		onput.onEndEdit.AddListener (SubmitKey);
	
	}

	void SubmitName(string arg0)
	{
		a = arg0;
		Debug.Log (arg0);
	}

	void SubmitKey(string arg0)
	{
		k = arg0;
		Debug.Log (arg0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
