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
        if (elixirUIGo == null)
        {
            if (GUILayout.Button("Create elixirUIGo"))
            {
                GameObject pfb = Resources.Load<GameObject>("ReactiveUI");
                elixirUIGo = GameObject.Instantiate(pfb,Vector3.zero,Quaternion.identity);
            }
        }
        else
        {
            if (GUILayout.Button("Destroy elixirUIGo"))
            {
                GameObject.Destroy(elixirUIGo);
            }
        }
    }


}
