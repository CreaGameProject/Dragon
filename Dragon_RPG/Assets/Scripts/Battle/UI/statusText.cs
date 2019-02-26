using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusText : MonoBehaviour {

    [SerializeField] Text textS;
    [SerializeField] Text text2;

    BattleMain battle;

    string name;
    string[] names;
    int[,] stutus;

	// Use this for initialization
	void Start () {

        battle = GameObject.Find("GameManager").GetComponent<BattleMain>();
        //name = "***";

        
      
    }

    // Update is called once per frame
    void Update () {

        names = battle.names;
        stutus = battle.status;

       // textS.text = names[0] + "\n" + names[1];

        text2.text = names[0] +"  "+ stutus[0, 0] + "\n" + names[1] +"  "+stutus[1, 0];
	}
}
