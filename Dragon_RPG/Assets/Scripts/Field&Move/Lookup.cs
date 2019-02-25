using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            if (FlagManager.Instance.flags[0] == true)
            {
                Debug.Log("成仏");
            }
        }
    }
}
