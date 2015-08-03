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

	// There is where all the attribs are initilized.
#region Init Attribs

	public void initAttribs()
	{
		initCombatTree();
		initScienceTree();
		initEngrTree();
		initOtherTree();
	}

	private void initCombatTree()
	{
		initRangedCombatTree("Combat|Ranged|");
		initMeleeCombatTree("Combat|Melee|");
	}

	private void initRangedCombatTree(string branch)
	{
		attribs.Add(branch + "Heavy Weapons", 1);
		attribs.Add(branch + "Rifles", 1);
		attribs.Add(branch + "Handgun", 1);
	}

	private void initMeleeCombatTree(string branch)
	{
		attribs.Add(branch + "One Handed", 1);
		attribs.Add(branch + "Two Handed", 1);
		attribs.Add(branch + "Dual Handed", 1);
	}

	private void initScienceTree()
	{
		attribs.Add("Science|Medicine", 1);
		attribs.Add("Science|Discovery", 1);
	}

	private void initEngrTree()
	{
		attribs.Add("Engr|Crafting", 1);
		attribs.Add("Engr|Repair", 1);
		attribs.Add("Engr|Emplacement", 1);
	}

	private void initOtherTree()
	{
		attribs.Add("Other|Stealth", 1);
		attribs.Add("Other|Piloting", 1);
	}

#endregion

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
}










