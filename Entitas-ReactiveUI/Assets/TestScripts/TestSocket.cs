using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class TestSocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (GUILayout.Button("Test tickentity"))
        {
            Debug.LogError("game tickentity null ->" + (Contexts.sharedInstance.game.tickEntity == null));
        }
    }
}
