using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "SERVER"))
        {
            NGNetworkManager.StartServer();
        }

        if (GUI.Button(new Rect(10, 70, 50, 30), "CLIENT"))
        {
            NGNetworkManager.StartClient();
            if(NGNetworkManager.Client.Connect("127.0.0.1", 65000))
            {
                
            }
        }
    }
}
