using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDra : MonoBehaviour {

    [SerializeField] Text text;

    BattleMain battle;

    string name;
    string[] names;

	// Use this for initialization
	void Start () {

        battle = GameObject.Find("GameManager").GetComponent<BattleMain>();
        //name = "***";


	}
	
	// Update is called once per frame
	void Update () {

        names = battle.names;

        text.text = names[0] + "\n" + names[1];

        
	}
}
