using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruitget : MonoBehaviour {

    GameObject zuri;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D other)
    {
        zuri = GameObject.Find("zurinomi");
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FlagManager.Instance.flags[0] = true;
            Destroy(zuri);
        }
    }
}
