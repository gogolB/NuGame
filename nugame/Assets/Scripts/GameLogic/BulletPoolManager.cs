using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPoolManager : MonoBehaviour 
{
	public GameObject bulletPrefab = null;
	List<GameObject> bulletPool = null;

	public int initialCap = 100;
	// Use this for initialization
	void Start () 
	{
		if(bulletPrefab == null)
		{
			#if UNITY_EDITOR
				Debug.LogError("There is no bullet prefab.");
			#endif
			this.enabled = false;
		}
		bulletPool = new List<GameObject>(100);
		StartCoroutine(initBullets());
	}

	IEnumerator initBullets()
	{
		for(int i = 0; i < bulletPool.Capacity; i++)
		{
			GameObject obj = (GameObject)GameObject.Instantiate(bulletPrefab);
			bulletPool.Add(obj);
			obj.SetActive(false);
			yield return 0;
		}
	}

	public GameObject createNewBulletInstance()
	{

		// Check for one we have already made.
		for(int i = 0; i < bulletPool.Capacity; i++)
		{
			if(!bulletPool[i].activeInHierarchy)
				return bulletPool[i];
		}

		// None of the ones we already make were deactive, need to make a new one.
		GameObject obj = (GameObject)GameObject.Instantiate(bulletPrefab);
		bulletPool.Add(obj);
		return obj;

	}
}
