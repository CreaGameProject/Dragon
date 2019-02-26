using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class commandMark : MonoBehaviour {

    Command command;
    BattleMain battleMain;

	// Use this for initialization
	void Start () {

        command = GameObject.Find("Canvas").GetComponent<Command>();
        battleMain = GameObject.Find("GameManager").GetComponent<BattleMain>();

	}
	
	// Update is called once per frame
	void Update () {

        if (battleMain.sendSelect() == 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }


        transform.parent = command.commandBax[ battleMain.sendActionSelect() ].transform;
        transform.localPosition = new Vector3(-20, 0, 0);
		
	}
}
