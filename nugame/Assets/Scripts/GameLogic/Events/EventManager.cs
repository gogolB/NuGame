using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This is an event manager for the game as a whole. Global level events will pass through this game manager.
 * Things such as deaths and respawns and so forth.
 */
public class EventManager : MonoBehaviour 
{
	public delegate void EventAction(EventArgs args);
	private static Dictionary<string, EventAction> eventTable = null;

	private static EventManager eventManager;
	
	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;
				
				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init (); 
				}
			}
			
			return eventManager;
		}
	}

	void Init ()
	{
		if (eventTable == null)
		{
			eventTable = new Dictionary<string, EventAction>();
		}
	}
	
	public void addListener(string Evt, EventAction callback)
	{
		// First check if the event is in the table.
		EventAction action = null;
		if(eventTable.TryGetValue(Evt, out action))
		{
			action += callback;
		}
		else // It isn't in the table.
		{
			eventTable.Add(Evt, callback);
		}
	}

	public void removeListener(string Evt, EventAction callback)
	{

		EventAction action = null;
		if(eventTable.TryGetValue(Evt, out action))
		{
			action -= callback;
		}
		#if UNITY_EDITOR
		else
		{
			Debug.LogWarning("Could not find event: " + Evt + ". Could not remove all listeners.");
		}
		#endif
	}

	public void removeAllListeners(string Evt)
	{
		if(eventTable.ContainsKey(Evt))
		{
			eventTable.Remove(Evt);
		}
		#if UNITY_EDITOR
		else
		{
			Debug.LogWarning("Could not find event: " + Evt + ". Could not remove all listeners.");
		}
		#endif
	}

	public void invokeEvent(string Evt, EventArgs args)
	{
		// Check if it is in the table.
		EventAction action = null;
		if(eventTable.TryGetValue(Evt, out action))
		{
			action.Invoke(args);
		}
		#if UNITY_EDITOR
		else
		{
			Debug.LogWarning("Could not invoke event: " + Evt);
		}
		#endif
	}
}

public class EventArgs
{

}

public class DeathEventArgs : EventArgs
{
	GameObject victim = null;
	GameObject killer = null;

	public DeathEventArgs(GameObject _victim, GameObject _killer)
	{
		this.victim = _victim;
		this.killer = _killer;
	}

	public GameObject getVictim()
	{
		return victim;
	}

	public GameObject getKiler()
	{
		return killer;
	}
}
