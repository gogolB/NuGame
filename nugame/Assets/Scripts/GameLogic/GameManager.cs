using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Character_Factory))]
public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
