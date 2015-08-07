using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The class to store all tbe basic parts of being a character.
// It also stores all the attributes for the character including the progress of the attribute.
public class Player_Character : MonoBehaviour
{
	// Dictionary to store the buffs for a character.
	private Dictionary<string, int> buffs = new Dictionary<string, int>();

	// Dictionary to store the attribs for a character.
	// The string represents the actual path to the attrib and the int represents the level.
	private Dictionary<string, int> attribs = new Dictionary<string, int>();

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
				Debug.LogWarning("Could not find attrib at path: " + fullAttrib + ". Adding to tree.");
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
				Debug.LogWarning("Could not find buff with name: " + buff + ". Adding to Dictionary.");
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
}
