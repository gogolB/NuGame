using UnityEngine;
using System.Collections;
using System.Xml;

public class Character_Factory : MonoBehaviour 
{
	public enum Character{
		Ingrid_Arteaga
	}

	public Character charToBuild = Character.Ingrid_Arteaga;
	// Use this for initialization
	void Start () 
	{
		XmlDocument doc =  new XmlDocument();
		doc.Load(Application.dataPath + "/Resources/Characters/Ingrid_Arteaga.char");
		XmlNode root = doc.DocumentElement;
		printChildren(root);

	}

	private void printChildren(XmlNode baseNode)
	{
		int numChildren = baseNode.ChildNodes.Count;
		Debug.Log("Node " + baseNode.Name +" has of children: " + numChildren);
		Debug.Log(baseNode.InnerText);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
