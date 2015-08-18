using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<ulong> inventory = new List<ulong>();
	public List<int> quantity = new List<int>();
	public int cap;

	public void addItem(ulong iid)
	{
		if (inventory.Count < cap) {
			inventory.Add (iid);
			quantity.Add (1);
		}
		//take stackable items into consideration
	}

}
