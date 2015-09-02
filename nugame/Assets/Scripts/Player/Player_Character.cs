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
	// Maximum attribute level.
	public static readonly int SKILL_LIM_MAX = 15;

	// Minimum attribute level
	public static readonly int SKILL_LIM_MIN = -5;

	// This is the value returned if no attrib was found.
	public static readonly int ATTRIB_NOT_FOUND = -10;

	// Dictionary to store the buffs for a character.
	private Dictionary<string, int> buffs = new Dictionary<string, int>();

	// Dictionary to store the attribs for a character.
	// The string represents the actual path to the attrib and the int represents the level.
	private Dictionary<string, int> attribs = new Dictionary<string, int>();

	// HACK 
	// The health slider
	private Slider healthSlider = null;

	// Gets the value of an attrib at the path. Otherwise returns -10 if not found.
	public int getAttrib(string fullAttrib)
	{
		int value;

		if(attribs.TryGetValue(fullAttrib, out value))
		{
			return value;
		}

		Debug.LogError("Could not find attrib at path: " + fullAttrib + ".");
		return ATTRIB_NOT_FOUND;
	}

	// Sets an attrib at the path with the new value. If final bool is true, will
	// add to tree if not found. Otherwise returns -1.
	public int setAttrib(string fullAttrib, int newValue, bool AddifNotFound = false)
	{
		int value;

		if(attribs.TryGetValue(fullAttrib, out value))
		{
			attribs[fullAttrib] = newValue;

			// Check the limits.
			if(attribs[fullAttrib] > SKILL_LIM_MAX)
				attribs[fullAttrib] = SKILL_LIM_MAX;
			else if(attribs[fullAttrib] < SKILL_LIM_MIN)
				attribs[fullAttrib] = SKILL_LIM_MIN;

			return value;
		}
		else
		{
			if(AddifNotFound)
			{
				#if UNITY_EDITOR
					Debug.LogWarning("Could not find attrib at path: " + fullAttrib + ". Adding to tree.");
				#endif

				// Check the limit.
				if(newValue > SKILL_LIM_MAX)
					newValue = SKILL_LIM_MAX;
				else if(newValue < SKILL_LIM_MIN)
					newValue = SKILL_LIM_MIN;

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
		StartCoroutine(loadBuffs());
	}

	IEnumerator loadBuffs()
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
			yield return 0;
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
