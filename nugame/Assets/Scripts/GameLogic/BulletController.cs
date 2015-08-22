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
		Vector3 lastPos = this.transform.position;
		this.transform.Translate(fwd * 1);

		checkHit(lastPos);
	}

	private void checkHit(Vector3 lastPos)
	{
		RaycastHit hitInfo;
		if(Physics.Linecast(lastPos, this.transform.position, out hitInfo) && hitInfo.collider.gameObject != this.gameObject)
		{
			// We hit something.
			this.StopCoroutine(destroyAfterTime());

			// We hit a player.
			if(hitInfo.collider.gameObject.GetComponent<Player_Character>() != null)
			{
				hitInfo.collider.gameObject.GetComponent<Player_Character>().takeDamage(15);
			}
			
			this.gameObject.SetActive(false);
		}
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
