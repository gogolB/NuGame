using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * The Pool managers are a great way to create a large number of objects that occur often so that memory doesn't need to be allocated often.
 */
public class BulletPoolManager : MonoBehaviour 
{
	public GameObject bulletPrefab = null;
	List<GameObject> bulletPool = null;

	private GameObject parent = null;

	public int initialCap = 5;
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
		bulletPool = new List<GameObject>(initialCap);
		parent = new GameObject("BulletParent");
		StartCoroutine(initBullets());
	}

	IEnumerator initBullets()
	{
		for(int i = 0; i < bulletPool.Capacity; i++)
		{
			GameObject obj = (GameObject)GameObject.Instantiate(bulletPrefab);
			obj.transform.SetParent(parent.transform);
			bulletPool.Add(obj);
			obj.SetActive(false);
			yield return 0;
		}
	}

	/**
	 * Gives you a single instance of a bullet. It will create a newone if it can't get you one from the list.
	 */
	public GameObject createNewBulletInstance()
	{

		// Check for one we have already made.
		for(int i = 0; i < bulletPool.Count; i++)
		{
			if(!bulletPool[i].activeSelf)
				return bulletPool[i];
		}

		// None of the ones we already make were deactive, need to make a new one.
		GameObject obj = (GameObject)GameObject.Instantiate(bulletPrefab);
		bulletPool.Add(obj);
		obj.transform.SetParent(parent.transform);
		return obj;

	}

	public void destroyAll()
	{
		for(int i = 0; i < bulletPool.Count; i++)
		{
			Destroy(bulletPool[i]);
		}
	}
}
