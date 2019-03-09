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

    int dragonHPmax;
    int dragonTPmax;

    int princesHPmax;
    int princesTPmax;

	// Use this for initialization
	void Start () {

        battle = GameObject.Find("GameManager").GetComponent<BattleMain>();
        //name = "***";

        dragonHPmax = GameObject.Find("DateBase").GetComponent<DragonDate>().HP;
        dragonTPmax = GameObject.Find("DateBase").GetComponent<DragonDate>().TP;

        princesHPmax = GameObject.Find("DateBase").GetComponent<PrincesDate>().HP;
        princesTPmax = GameObject.Find("DateBase").GetComponent<PrincesDate>().TP;
    }

    // Update is called once per frame
    void Update () {

        names = battle.names;
        stutus = battle.status;

       // textS.text = names[0] + "\n" + names[1];

        text2.text = names[0] +"  "+ stutus[0, 0]+"/"+dragonHPmax+"  "+stutus[0,1]+"/"+dragonTPmax + 
            "\n" + names[1] +"     "+stutus[1, 0]+"/"+princesHPmax+"  "+stutus[1,1]+"/"+princesTPmax;
	}
}
