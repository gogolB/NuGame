using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Character_Factory : MonoBehaviour 
{
	public GameObject CharPrefab = null;

	public enum Character {
		Ingrid_Arteaga
	}

	public Character charToBuild = Character.Ingrid_Arteaga;

	// Use this for initialization
	// HACK this is only while i'm test driving the system.
	void Start () 
	{
		if(CharPrefab == null)
		{
			Debug.LogError("Error! No Character Prefab Set!");
		}
		ConstructCharacter(Application.dataPath + "/Resources/Characters/Ingrid_Arteaga.char");
	}

	private string printChildren(XmlNode baseNode, string str = "")
	{
		string toReturn = "";
		if(baseNode.ChildNodes.Count > 1)
		{
			int numChildren = baseNode.ChildNodes.Count;
			toReturn += str + baseNode.Name + "\n";
			for(int i = 0; i < numChildren; i++)
			{
				toReturn += printChildren(baseNode.ChildNodes.Item(i), str + "\t");
			}
			return toReturn;
		}
		else
		{
			if(baseNode.Attributes.Count == 0)
				return toReturn += str +  baseNode.Name + ": " + baseNode.InnerText + "\n";
			else
				return toReturn += str +  baseNode.Name + " (" + baseNode.Attributes.Item(0).InnerText + "): " + baseNode.InnerText + "\n";
		}
	}

	public void ConstructCharacter(string toCharFile, string name = "")
	{
		// HACK For visuilation purposes only.
		GameObject obj = GameObject.Instantiate(CharPrefab);
		obj.transform.position = new Vector3(10, 0, 10);
		//obj.SetActive(false);

		XmlTextReader textReader = new XmlTextReader(toCharFile);
		while(textReader.Read())
		{
			XmlNodeType nType = textReader.NodeType;
			if(nType == XmlNodeType.Element)
			{
				handleCharFileElements(textReader, obj);
			}
			else if(nType == XmlNodeType.EndElement)
			{
				if(textReader.Name == "Character")
				{
					#if UNITY_EDITOR
						Debug.Log("Done with Character");
					obj.GetComponent<Player_Character>().printOutAttribs();
					obj.GetComponent<Player_Character>().updateBuffs();
					obj.GetComponent<Player_Character>().printOutBuffs();
					#endif
					break;
				}
			}
		}
		textReader.Close();
	}


	// Handles each of the various elements of the buff table.
	private void handleCharFileElements(XmlTextReader reader, GameObject obj)
	{
		string str;

		// Handle the Characters Root Node.
		if(reader.Name == "Character")
		{
			#if UNITY_EDITOR
				Debug.Log("Character: " + reader.GetAttribute("name"));
			#endif
		}
		// Handle the Name Node.
		else if( reader.Name == "Name")
		{
			str = reader.ReadElementContentAsString();
			obj.name = str;

			#if UNITY_EDITOR
				Debug.Log ("Name: " + str);
			#endif
		}
		// Handle the Main class stuff.
		else if(reader.Name == "MainClass")
		{
			str = reader.ReadElementContentAsString();
			obj.GetComponent<Player_Persona>().MainClass = str;
			handleMainClass(str, obj.GetComponent<Player_Character>());

			#if UNITY_EDITOR
				Debug.Log ("MainClass: " + str);
			#endif
		}
		// Handle the SubClass Stuff.
		else if(reader.Name == "SubClass")
		{
			str = reader.ReadElementContentAsString();
			obj.GetComponent<Player_Persona>().SubClass = str;
			handleSubClass(obj.GetComponent<Player_Persona>().MainClass , str, obj.GetComponent<Player_Character>());

			#if UNITY_EDITOR
				Debug.Log ("SubClass: " + str);
			#endif
		}
		// Handle the History Stuff.
		else if(reader.Name == "History")
		{
			str = reader.ReadElementContentAsString();
			obj.GetComponent<Player_Persona>().History = str;
			handleHistory(obj.GetComponent<Player_Persona>().SubClass, str, obj.GetComponent<Player_Character>());

			#if UNITY_EDITOR
				Debug.Log ("History: " + str);
			#endif
		}
		// Handle the attribute tree.
		else if (reader.Name == "Attribs")
		{
			handleAttribs(reader, obj.GetComponent<Player_Character>());
		}
	}

	// This goes through and loads all the base stuff that needs to be loaded.
	private void handleMainClass(string mainClass, Player_Character character)
	{
		XmlTextReader reader = new XmlTextReader(Application.dataPath + "/Resources/Classes/Main_Class.atr");
		#if UNITY_EDITOR
			Debug.Log("Loading Base Class");
		#endif

		loadBaseClass(reader, character);

		#if UNITY_EDITOR
			Debug.Log("Loading Class info.");
		#endif

		skipToAttribute(reader, "Class", "name", mainClass);

		XmlNodeType nType;
		do
		{
			reader.Read ();
			nType = reader.NodeType;
			if(nType == XmlNodeType.Element)
			{
				if(reader.Name == "Attribs")
				{
					handleAttribs(reader, character);
				}
			}

		}while(!(reader.Name == "Class" && nType == XmlNodeType.EndElement));

		reader.Close();
	}

	// This goes through and loads the base class.
	private void loadBaseClass(XmlTextReader reader, Player_Character character)
	{
		XmlNodeType nType = reader.NodeType;
		do
		{
			reader.Read();
			nType = reader.NodeType;
			if(nType == XmlNodeType.Element)
				if(reader.Name == "Attribs")
				{
					handleAttribs(reader, character);
				}
		} while(!(reader.Name == "Base_Class" && nType == XmlNodeType.EndElement) && !reader.EOF);
	}

	// This goes through and loads all the subclass stuff that needs to be loaded.
	private void handleSubClass(string mainClass, string subClass, Player_Character character)
	{
		#if UNITY_EDITOR
			Debug.Log("Loading subclass info.");
		#endif

		XmlTextReader reader = new XmlTextReader(Application.dataPath + "/Resources/Classes/SC_" + mainClass +".atr");

		skipToAttribute(reader, "SubClass", "name", subClass);
		
		XmlNodeType nType;
		do
		{
			reader.Read ();
			nType = reader.NodeType;
			if(nType == XmlNodeType.Element)
			{
				if(reader.Name == "Attribs")
				{
					handleAttribs(reader, character);
				}
			}
			
		}while(!(reader.Name == "SubClass" && nType == XmlNodeType.EndElement));

		reader.Close();
	}

	// This goes through and loads all the history stuff that needs to be loaded.	
	private void handleHistory(string subClass, string history, Player_Character character)
	{
		#if UNITY_EDITOR
			Debug.Log("Loading History Info.");
		#endif

		XmlTextReader reader = new XmlTextReader(Application.dataPath + "/Resources/Classes/H_" + subClass +".atr");

		skipToAttribute(reader, "History", "name", history);
		
		XmlNodeType nType;
		do
		{
			reader.Read ();
			nType = reader.NodeType;
			if(nType == XmlNodeType.Element)
			{
				if(reader.Name == "Attribs")
				{
					handleAttribs(reader, character);
				}
			}
			
		}while(!(reader.Name == "History" && nType == XmlNodeType.EndElement));

		reader.Close();
	}

	// This handles all the attribute trees in every loader.
	private void handleAttribs(XmlTextReader reader, Player_Character character)
	{
		XmlNodeType nType = reader.NodeType;
		string fullSkill = "";
		string tmp = "";
		do
		{
			reader.Read ();
			nType = reader.NodeType;
			if(nType == XmlNodeType.Element)
			{
				if(reader.Name == "Attribute")
				{
					fullSkill += reader.GetAttribute("name");
					tmp = reader.ReadElementContentAsString();
					if(tmp.StartsWith("+") || tmp.StartsWith("-"))
					{
						// We are going to modify the value.
						if(tmp.StartsWith("+"))
						{
							// Going to add it.
							tmp = tmp.Substring(1);
							int currentValue = character.getAttrib(fullSkill);
							if (currentValue > 0)
								character.setAttrib(fullSkill, currentValue + int.Parse(tmp));
						}
						else if (tmp.StartsWith("-"))
						{
							// Going to subtract it.
							tmp = tmp.Substring(1);
							int currentValue = character.getAttrib(fullSkill);
							if (currentValue > 0)
								character.setAttrib(fullSkill, currentValue - int.Parse(tmp));
						}
					}
					else
					{
						// We are just going to set the value hard.
						character.setAttrib(fullSkill, int.Parse(tmp), true);
					}
					fullSkill = fullSkill.Remove(fullSkill.LastIndexOf("|") + 1);
				}
				else
				{
					fullSkill += reader.Name + "|";
				}
			}
			else if(nType == XmlNodeType.EndElement)
			{
				if(fullSkill.IndexOf(reader.Name) >= 0)
					fullSkill = fullSkill.Remove(fullSkill.IndexOf(reader.Name));
			}
		}while(!(reader.Name == "Attribs" && nType == XmlNodeType.EndElement));
	}

	// This pushes the reader forward to an element with the given name and an attrib with the given name and value.
	public static void skipToAttribute(XmlTextReader reader, string elementName, string AttribName, string attribvalue)
	{
		XmlNodeType nType = reader.NodeType;
		while(!reader.EOF)
		{
			reader.Read();
			nType = reader.NodeType;
			if(nType == XmlNodeType.Element && reader.Name == elementName && reader.GetAttribute(AttribName) == attribvalue)
				return;
		}
		Debug.LogError("Could not find " + elementName + ", with attrib " + AttribName + " with value "+ attribvalue);
	}
}
