using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : MonoBehaviour {


	public Vector3 fwd; 
	// Use this for initialization
	void Start () 
	{
		this.StartCoroutine(destroyAfterTime());
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate(fwd * 1);
	}

	IEnumerator destroyAfterTime()
	{
		yield return new WaitForSeconds(2.0f);
		this.gameObject.SetActive(false);
	}

	void OnEnable() 
	{
		this.StartCoroutine(destroyAfterTime());
	}
}
