using UnityEngine;
using System.Collections;

public class BellFadeIn : MonoBehaviour {

	private Color ourColor;
	private Color color = new Color();
	public bool ready = false;
	float timemit = 0.0f;

	// Use this for initialization
	void Start () {
		ourColor = GetComponent<Renderer>().material.GetColor("_Color");
		color.r = ourColor.r;
		color.g = ourColor.g;
		color.b = ourColor.b;
		color.a = 0.0f;



		GetComponent<Renderer>().material.SetColor ("_Color", color);

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("space")) {
			color.a = 1.0f;
			GetComponent<Renderer> ().material.SetColor ("_Color", color);
			ready = true;
		}

		if (!ready) {

			timemit += Time.deltaTime;

			if (timemit >= 6.0f) {
				color.a += 0.0005f * timemit;
				if (color.a >= 1) {
					color.a = 1;
					GetComponent<Renderer> ().material.SetColor ("_Color", color);
					ready = true;
				}

				GetComponent<Renderer> ().material.SetColor ("_Color", color);

				if (color.a < 1.0)
					transform.localScale += new Vector3 (0.01f / timemit, 0.01f / timemit, 0);
			}
		}


	}
}
