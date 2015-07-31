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
	private Dictionary<string, int> attribs = new Dictionary<string, int>();
}


