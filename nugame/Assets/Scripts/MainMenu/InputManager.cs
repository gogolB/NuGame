using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	List<InputMap> map = new List<InputMap>();

	void Awake()
	{
		//Take in PlayerPrefs and but them into map
		if (!PlayerPrefs.HasKey ("Fire"))
			PlayerPrefs.SetString ("Fire", "m");
		if(!PlayerPrefs.HasKey("Up"))
			PlayerPrefs.SetString ("Up", "w");
		if(!PlayerPrefs.HasKey("Down"))
			PlayerPrefs.SetString ("Down", "s");
		if(!PlayerPrefs.HasKey("Right"))
			PlayerPrefs.SetString ("Right", "d");
		if(!PlayerPrefs.HasKey("Left"))
			PlayerPrefs.SetString ("Left", "a");
		if(!PlayerPrefs.HasKey("Interact"))
			PlayerPrefs.SetString ("Interact", "n");
		if (!PlayerPrefs.HasKey ("Volume"))
			PlayerPrefs.SetFloat ("Volume", 10.0f);
		if (!PlayerPrefs.HasKey ("SFX"))
			PlayerPrefs.SetFloat ("SFX", 10.0f);

		RegisterControl ("Fire", PlayerPrefs.GetString ("Fire"));
		RegisterControl ("Up", PlayerPrefs.GetString ("Up"));
		RegisterControl ("Down", PlayerPrefs.GetString ("Down"));
		RegisterControl ("Right", PlayerPrefs.GetString ("Right"));
		RegisterControl ("Left", PlayerPrefs.GetString ("Left"));
		RegisterControl ("Interact", PlayerPrefs.GetString ("Interact"));

		print ();

	}

	// Checks if action or key already exist in map, if not, add to map
	public void RegisterControl(string a, string k)
	{
		bool add = true;
		for (int i = 0; i < map.Count; ++i)
			if (map[i].act == a || map[i].key == k) {
				add = false;
				break;
			}

		if(add == true)
		{
			InputMap ad = new InputMap(a, k);
			map.Add(ad);
		}
	}

	// Binds new control to a pre-established action
	public void BindControl(string a, string k)
	{
		bool bind = true;

		for(int i = 0; i < map.Count; ++i) //check if specified key is already in use
			if(map[i].key == k)
			{
				bind = false;
				break;
				//return false;
			}

		if(bind) 
			for(int i = 0; i < map.Count; ++i) //if new key is not yet in use, find control and bind new key
				if(map[i].act == a) {
					map[i].key = k;
					PlayerPrefs.SetString(a, k);
					//return true;
					break;
				}

		//return false;
	}

	// Get state of control
	public bool GetControl(string a)
	{
		for (int i = 0; i < map.Count; ++i)
			if (map [i].act == a) {
				return Input.GetKey(map[i].key);
			}

		return false;
	}


	public void print()
	{
		for (int i = 0; i < map.Count; ++i)
			Debug.Log (map [i].act + ", " + map [i].key);
	}


	// Class to hold action associated with key
	class InputMap
	{
		public string act;
		public string key;

		public InputMap()
		{
			act = null;
			key = null;
		}

		public InputMap(string a, string k)
		{
			act = a;
			key = k;
		}
			
	};
}
