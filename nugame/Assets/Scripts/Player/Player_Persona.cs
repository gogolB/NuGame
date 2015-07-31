using UnityEngine;
using System.Collections;

// This object sets all the values for a single person. It also sets all the
// attributes as well as loading all the correct skills for the person.
public class Player_Persona : MonoBehaviour {

	public enum Class_Types
	{
		Support, Fighter, Leader, Jester
	}

	public Class_Types mainClassType = Class_Types.Support;


}
