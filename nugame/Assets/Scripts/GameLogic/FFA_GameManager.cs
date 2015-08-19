using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FFA_GameManager : GameManager 
{
	// This is the kills to death ratio table.
	private Dictionary<string, KDR> kdrTable = null;
	
	// Use this for initialization
	void Start () 
	{
		kdrTable = new Dictionary<string, KDR>();
		EventManager.instance.addListener("onPlayerDeath", onPlayerDeath);

		//this.GetComponent<Event_Tester>().test();
	}
	
	// Update is called once per frame
	//void Update () {
	//}

	public void addEntry(string playerName, KDR kdr = new KDR())
	{
		if(kdrTable != null)
		{
			kdrTable.Add(playerName, kdr);
		}
		else
		{
			Debug.LogError("Could not add to KDR table, because table does not exist.");
		}
	}

	public void onPlayerDeath(EventArgs args)
	{
		DeathEventArgs _args = (DeathEventArgs) args;

		// Update the killer's kill
		string playerName = _args.getKiler().GetComponent<Player_Persona>().playerName;
		if(kdrTable.ContainsKey(playerName))
		{
			KDR temp = kdrTable[playerName];
			temp.kills += 1;
			kdrTable[playerName] = temp;
		}
		else
		{
			Debug.LogError("Could not find killer.");
		}

		// Update the victim's death.
		playerName = _args.getVictim().GetComponent<Player_Persona>().playerName;
		if(kdrTable.ContainsKey(playerName))
		{
			KDR temp = kdrTable[playerName];
			temp.death += 1;
			kdrTable[playerName] = temp;
		}
		else
		{
			Debug.LogError("Could not find Victim.");
		}
	}

	public string getKDRTableAsString()
	{
		if(kdrTable == null)
		{
			#if UNITY_EDITOR
			Debug.LogError("No KDR Table. did you init the game manager?");
			#endif
			return "";
		}

		string str = "";
		foreach(KeyValuePair<string, KDR> entry in kdrTable)
		{
			float kdrRatio = (entry.Value.kills + 0.0f) / (entry.Value.death + 0.0f);
			str += entry.Key + "|" + entry.Value.kills + "|" + entry.Value.death + "|" + kdrRatio + "|\n";
		}
		return str;
	}
}

public struct KDR
{
	public int kills;
	public int death;
}
