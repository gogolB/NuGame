using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DispButton : MonoBehaviour {

	public string act;

	// Use this for initialization
	void Start () {

		GetComponent<Text> ().text = PlayerPrefs.GetString(act);
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = PlayerPrefs.GetString(act);
	
	}
}
