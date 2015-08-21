using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

// The class to store all tbe basic parts of being a character.
// It also stores all the attributes for the character including the progress of the attribute.
public class Player_Character : MonoBehaviour
{
	// Dictionary to store the buffs for a character.
	private Dictionary<string, int> buffs = new Dictionary<string, int>();

	// Dictionary to store the attribs for a character.
	// The string represents the actual path to the attrib and the int represents the level.
	private Dictionary<string, int> attribs = new Dictionary<string, int>();

	// HACK 
	// The health slider
	private Slider healthSlider = null;

	// Gets the value of an attrib at the path. Otherwise returns -1 if not found.
	public int getAttrib(string fullAttrib)
	{
		int value;

		if(attribs.TryGetValue(fullAttrib, out value))
		{
			return value;
		}

		Debug.LogError("Could not find attrib at path: " + fullAttrib + ".");
		return -1;
	}

	// Sets an attrib at the path with the new value. If final bool is true, will
	// add to tree if not found. Otherwise returns -1.
	public int setAttrib(string fullAttrib, int newValue, bool AddifNotFound = false)
	{
		int value;

		if(attribs.TryGetValue(fullAttrib, out value))
		{
			attribs[fullAttrib] = newValue;
			return value;
		}
		else
		{
			if(AddifNotFound)
			{
				#if UNITY_EDITOR
					Debug.LogWarning("Could not find attrib at path: " + fullAttrib + ". Adding to tree.");
				#endif

				attribs.Add(fullAttrib, newValue);
				return newValue;
			}
			else
			{
				Debug.LogError("Could not find attrib at path: " + fullAttrib);
				return -1;
			}
		}
	}

	// Trys to get a buff, returns -1 if it can not be found.
	public int getBuff(string BuffAttrib)
	{
		int value;
		
		if(buffs.TryGetValue(BuffAttrib, out value))
		{
			return value;
		}
		
		Debug.LogError("Could not find buff with name: " + BuffAttrib + ".");
		return -1;
	}

	// Trys to set a buff, returns the old value.
	public int setBuff(string buff, int newValue, bool AddifNotFound = false)
	{
		int value;
		
		if(buffs.TryGetValue(buff, out value))
		{
			buffs[buff] = newValue;
			return value;
		}
		else
		{
			if(AddifNotFound)
			{
				#if UNITY_EDITOR
					Debug.LogWarning("Could not find buff with name: " + buff + ". Adding to Dictionary.");
				#endif

				buffs.Add(buff, newValue);
				return newValue;
			}
			else
			{
				Debug.LogError("Could not find buff with name:" + buff);
				return -1;
			}
		}
	}

	public void updateBuffs()
	{
		foreach(KeyValuePair<string, int> entry in attribs)
		{
			string filename = entry.Key.Substring(entry.Key.LastIndexOf("|") + 1).Replace(" ", "_") + ".attribute";
			filename = Application.dataPath + "/Resources/Attributes/" + filename;
			if(File.Exists(filename))
			{
				XmlTextReader reader = new XmlTextReader(filename);
				Character_Factory.skipToAttribute(reader, "Level", "name", entry.Value + "");
				while(reader.Read())
				{
					XmlNodeType type = reader.NodeType;
					if(type == XmlNodeType.Element)
					{
						if(reader.Name == "buff")
						{
							string buffName = reader.GetAttribute("name");
							string value = reader.ReadInnerXml();
							setBuff(buffName, int.Parse(value), true);
						}
					}
				}
				reader.Close();

			}
			else
			{
				Debug.LogError("Could not find attribute file: " + filename);
				#if UNITY_EDITOR
					Debug.Break();
				#endif
			}
		}
	}

	public void takeDamage(int amt)
	{
		if(healthSlider == null)
		{
			healthSlider = this.gameObject.GetComponentInChildren<Slider>();
		}

		healthSlider.value -= amt;
	}

#if UNITY_EDITOR
	public void printOutAttribs()
	{
		string toPrint = "Attributes\n";
		foreach(KeyValuePair<string, int> entry in attribs)
		{
			toPrint += entry.Key + ": " + entry.Value + "\n";
		}
		Debug.Log(toPrint);
	}

	public void printOutBuffs()
	{
		string toPrint = "BUFFS\n";
		foreach(KeyValuePair<string, int> entry in buffs)
		{
			toPrint += entry.Key + ": " + entry.Value + "\n";
		}
		Debug.Log(toPrint);
	}

	public Dictionary<string, int> getAttribsTable()
	{
		return attribs;
	}

	public Dictionary<string, int> getBuffsTable()
	{
		return buffs;
	}
#endif
}
