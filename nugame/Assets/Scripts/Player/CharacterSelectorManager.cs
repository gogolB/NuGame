using UnityEngine;
using System.Collections;

public class CharacterSelectorManager : MonoBehaviour {

	Character_Factory factory;
	public GameObject charModel;
	public GameObject prevModel;
	public GameObject nextModel;

	// Use this for initialization
	void Start () {
		factory = this.GetComponent<Character_Factory>();
		charModel.SetActive(true);
	}

	public void loadCharacter(string file, string name = "")
	{
		
	}

	public void loadNext()
	{

	}
}
