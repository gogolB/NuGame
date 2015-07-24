using UnityEngine;
using System.Collections;

public class FadeAllAndScene : MonoBehaviour {

	public bool zooom = false;
	public bool fader = false;
	public GameObject logo1;
	public GameObject logo2;
	public GameObject cam;
	//private Color optColor, logoColor;
	private Color color = new Color();
	private Color logoColor = new Color ();

	public float timemit = 0.0f;

	// Use this for initialization
	void Start () {

		color.a = 0.0f;
		logoColor = logo2.GetComponent<Renderer> ().material.GetColor ("_Color");
		logoColor.a = 1.0f;

	
	}
	
	// Update is called once per frame

	void OnMouseDown()
	{
		fader = true;
	}

	void Update () {

		if (fader) {

			logo1.GetComponent<Display>().open = false;
			GetComponent<Renderer>().material.SetColor ("_Color", color);
			logo1.GetComponent<Renderer>().material.SetColor ("_Color", color);
			//logoColor = logo2.GetComponent<Renderer> ().material.GetColor ("_Color");

			timemit += Time.deltaTime;

			if(timemit >= 1.0f)
			{
				logoColor.a -= 0.04f;
				logo2.GetComponent<Renderer>().material.SetColor ("_Color", logoColor);
				if(logoColor.a <= .1)
					zooom = true;
			}

			if(zooom)
			{
				cam.GetComponent<Camera>().fieldOfView -= 1;
				if(cam.GetComponent<Camera>().fieldOfView <= 40)
				{
					cam.GetComponent<Camera>().fieldOfView -= 1;

				}

				if(cam.GetComponent<Camera>().fieldOfView <= 1)
				{
					fader = false;
					zooom = false;
				}
			}


		}
	
	}
}
