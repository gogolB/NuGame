using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
#if UNITY_EDITOR
public class CharTesterGUI : MonoBehaviour 
{
	XmlTextReader reader = null;
	
	public string charToBuild = "";
	public string charSubName = "";

	Character_Factory factory;

	GameObject obj = null;

	void Start()
	{
		factory = this.GetComponent<Character_Factory>();
	}

	void OnGUI()
	{
		if(charToBuild == "" || charSubName == "")
		{
			selectCharGUI();
		}
		else
		{
			if(obj == null)
			{
				obj = factory.ConstructCharacter(charToBuild, charSubName);
				obj.SetActive(false);
			}
			showCharStuff();
		}
	}
	Vector2 scrollPos = Vector2.zero;
	void selectCharGUI()
	{
		if (reader != null)
		{
			reader.Close();
			reader = null;
		}
		reader = new XmlTextReader(Application.dataPath + "/Resources/Characters/Characters.char");

		GUI.Label(new Rect(Screen.width * 0.125f, Screen.height * 0.05f, Screen.width * 0.75f, Screen.height * 0.05f), "Select Character...");

		scrollPos = GUI.BeginScrollView(new Rect(Screen.width * 0.125f, Screen.height * 0.1f, Screen.width * 0.75f, Screen.height * 0.8f), scrollPos, new Rect(0, 0, Screen.width * 0.75f, 200));

		int i = 0;
		int boxHeight = 20;
		int spacing = 5;
		while(reader.Read())
		{
			XmlNodeType nType = reader.NodeType;
			if(nType == XmlNodeType.Element && reader.Name == "Character")
			{
				GUI.Box(new Rect(0, i * (boxHeight + spacing), Screen.width * 0.75f, boxHeight), "");
				GUILayout.BeginHorizontal();
					
					GUI.Label(new Rect(5, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), reader.GetAttribute("name"));
					GUI.Label(new Rect(Screen.width * 0.25f, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), reader.GetAttribute("subname"));

				if(GUI.Button(new Rect(Screen.width * 0.50f, i * (boxHeight + spacing), Screen.width * 0.24f, boxHeight), "Build..."))
				{
					charSubName = reader.GetAttribute("subname");
					string charname = reader.GetAttribute("name");
					string file = reader.ReadInnerXml();
					Debug.Log ("Constructing Character: " + charSubName + " " + charname + " from file " + file);
					charToBuild = Application.dataPath + "/Resources/Characters/" + file;
				}

				GUILayout.EndHorizontal();
				i++;
			}

		}

		GUI.EndScrollView();
	}

	string location = "home";

	void showCharStuff()
	{

		GUILayout.BeginHorizontal();

		if(GUI.Button(new Rect(Screen.width * 0.125f, Screen.height * 0.1f - 20, 150, 18), "Stats"))
		{
			location = "stats";
		}

		if(GUI.Button(new Rect(Screen.width * 0.125f + 160, Screen.height * 0.1f - 20, 150, 18), "Buffs"))
		{
			location = "Buffs";
		}

		if(GUI.Button(new Rect(Screen.width * 0.125f + 320, Screen.height * 0.1f - 20, 150, 18), "Back"))
		{
			location = "home";
			charToBuild = "";
			charSubName = "";
			GameObject.Destroy(obj);
			obj = null;
		}

		GUILayout.EndHorizontal();

		if (location == "home" || location == "stats")
		{
			showStats();
		}
		else if(location == "Buffs")
		{
			showBuffs();
		}
	}


	Vector2 statScrollPos = Vector2.zero;
	void showStats()
	{
		if(obj == null)
			return;

		GUI.Box(new Rect(Screen.width * 0.125f, Screen.height * 0.1f, Screen.width * 0.75f, Screen.height * 0.8f), "Stats");

		statScrollPos = GUI.BeginScrollView(new Rect(Screen.width * 0.125f, Screen.height * 0.1f, Screen.width * 0.75f, Screen.height * 0.8f), statScrollPos, new Rect(0, 0, Screen.width * 0.70f, obj.GetComponent<Player_Character>().getAttribsTable().Count * 25));
		
		int i = 1;
		int boxHeight = 20;
		int spacing = 5;

		if(obj == null)
			return;

		Dictionary<string, int> attribs = obj.GetComponent<Player_Character>().getAttribsTable();
		foreach(KeyValuePair<string, int> entry in attribs)
		{
			GUI.Box(new Rect(Screen.width * 0.025f, i * (boxHeight + spacing), Screen.width * 0.70f, boxHeight), "");
			GUILayout.BeginHorizontal();
			
			GUI.Label(new Rect(Screen.width * 0.025f + 5, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), entry.Key);
			GUI.HorizontalSlider(new Rect(Screen.width * 0.025f + 5 + Screen.width * 0.25f, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), entry.Value, Player_Character.SKILL_LIM_MIN, Player_Character.SKILL_LIM_MAX);
			GUI.Label(new Rect(Screen.width * 0.025f + 5 + Screen.width * 0.55f, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), entry.Value + " / " + Player_Character.SKILL_LIM_MAX);

			GUILayout.EndHorizontal();
			i++;
		}

		GUI.EndScrollView();
	}


	Vector2 buffScrollPos = Vector2.zero;
	void showBuffs()
	{
		if(obj == null)
			return;


		GUI.Box(new Rect(Screen.width * 0.125f, Screen.height * 0.1f, Screen.width * 0.75f, Screen.height * 0.8f), "Buffs");

		buffScrollPos = GUI.BeginScrollView(new Rect(Screen.width * 0.125f, Screen.height * 0.1f, Screen.width * 0.75f, Screen.height * 0.8f), buffScrollPos, new Rect(0, 0, Screen.width * 0.73f, obj.GetComponent<Player_Character>().getBuffsTable().Count * 25));
		
		int i = 1;
		int boxHeight = 20;
		int spacing = 5;
		
		if(obj == null)
			return;
		
		Dictionary<string, int> buffs = obj.GetComponent<Player_Character>().getBuffsTable();
		foreach(KeyValuePair<string, int> entry in buffs)
		{
			GUI.Box(new Rect(Screen.width * 0.025f, i * (boxHeight + spacing), Screen.width * 0.70f, boxHeight), "");
			GUILayout.BeginHorizontal();
			
			GUI.Label(new Rect(Screen.width * 0.025f + 5, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), entry.Key);
			GUI.HorizontalSlider(new Rect(Screen.width * 0.025f + 5 + Screen.width * 0.25f, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), entry.Value, 0, 100);
			GUI.Label(new Rect(Screen.width * 0.025f + 5 + Screen.width * 0.55f, i * (boxHeight + spacing), Screen.width * 0.20f, boxHeight), entry.Value + "%");
			
			GUILayout.EndHorizontal();
			i++;
		}
		
		GUI.EndScrollView();
	}

}
#endif
