using UnityEngine;
using System.Collections;

public class SimpleFadeIn : MonoBehaviour {
	
	private Color ourColor;
	private Color color = new Color();
	public bool done = false;
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
			done = true;
			color.a = 0.67f;
			GetComponent<Renderer>().material.SetColor ("_Color", color);
		}
		
		timemit += Time.deltaTime;
		
		if (timemit >= 10.0f && !done) {
			color.a += 0.0002f * timemit;
			if(color.a >= 0.66)
				done = true;
			
			GetComponent<Renderer>().material.SetColor ("_Color", color);
			
			//if(color.a < 1.0)
			//	transform.localScale += new Vector3(0.004f/timemit, 0.004f/timemit, 0);
		}
		
		
	}
}