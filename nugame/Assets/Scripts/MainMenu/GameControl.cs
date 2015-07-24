using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;
	public float health;
	public float experience;

	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
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
		} 

		else
			Destroy (gameObject);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerIndex.dat");
		PlayerData data = new PlayerData ();
		data.health = health;
		data.experience = experience;
		bf.Serialize (file, data);
		file.Close ();

	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerIndex.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerIndex.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();
			health = data.health;
			experience = data.experience;

		}

	}

	// Update is called once per frame
	void Update () {
	
	}

	[Serializable]
	class PlayerData
	{
		public float health;
		public float experience;
	};
}
