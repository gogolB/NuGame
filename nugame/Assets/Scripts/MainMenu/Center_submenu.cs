using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Center_submenu : MonoBehaviour {

	public void get_menu(GameObject g)
	{
		if (g.activeSelf)
			g.SetActive (false);
		else
			g.SetActive (true);
	}

}
