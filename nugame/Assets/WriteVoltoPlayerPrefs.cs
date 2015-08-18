using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WriteVoltoPlayerPrefs : MonoBehaviour {

	public string set;

	void Awake()
	{
		gameObject.GetComponent<Slider> ().value = PlayerPrefs.GetFloat (set);
	}

	public void wvtpp()
	{
		float y = gameObject.GetComponent<Slider>().value;
		PlayerPrefs.SetFloat (set, y);
	}
}
