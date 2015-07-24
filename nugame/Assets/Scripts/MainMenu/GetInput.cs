using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetInput : MonoBehaviour {

	public GameObject controller;

	// Use this for initialization
	void Start () {

		GetComponent<Text> ().text = controller.GetComponent<UseButtons>().inti;
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = controller.GetComponent<UseButtons>().inti;
	}
}
