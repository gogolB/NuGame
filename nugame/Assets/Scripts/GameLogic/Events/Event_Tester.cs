using UnityEngine;
using System.Collections;

public class Event_Tester : MonoBehaviour 
{
	public GameObject Test1;
	public GameObject Test2;


	// Use this for initialization
	public void test () 
	{
		FFA_GameManager gm = this.GetComponent<FFA_GameManager>();
		gm.addEntry("Player 1");
		gm.addEntry("Player 2");

		DeathEventArgs args = new DeathEventArgs(Test1, Test2);
		EventManager.instance.invokeEvent("onPlayerDeath", args);

		args = new DeathEventArgs(Test2, Test1);
		EventManager.instance.invokeEvent("onPlayerDeath", args);

		Debug.Log(gm.getKDRTableAsString());
	}
}
