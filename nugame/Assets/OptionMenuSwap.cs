using UnityEngine;
using System.Collections;

public class OptionMenuSwap : MonoBehaviour {

	public GameObject a, b, c;
	
	public void setOptionMenu(GameObject g)
	{
		a.SetActive (false);
		b.SetActive (false);
		c.SetActive (false);
		g.SetActive (true);
	}
}
