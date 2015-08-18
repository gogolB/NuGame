using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class LookatNew : MonoBehaviour {

	public GameObject a;
	public GameObject cam;

	public void looknew()
	{
		cam.GetComponent<LookatTarget> ().SetTarget (a.transform);// = a;
	}
}
