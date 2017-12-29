using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject elixirUIGo;

    private void OnGUI()
    {
        if (!Contexts.sharedInstance.game.hasReactiveUI)
        {
            if (GUILayout.Button("Start Game"))
            {
                Contexts.sharedInstance.input.CreateEntity().isStartGame = true;
                //GameObject pfb = Resources.Load<GameObject>("ReactiveUI");
                //elixirUIGo = GameObject.Instantiate(pfb,Vector3.zero,Quaternion.identity);
            }
        }
        else
        {
            if (GUILayout.Button("Destroy Game"))
            {
                Contexts.sharedInstance.input.CreateEntity().isEndGame = true;
                //GameObject.Destroy(elixirUIGo);
            }
        }
    }


}
