using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDate : MonoBehaviour {

    public string Name;

    public int HP;
    public int TP;

    public int AttackP;
    public int DefenceP;
    public int SpeedP;

    BattleMain battle;

    private void Awake()
    {
        battle = GameObject.Find("GameManager").GetComponent<BattleMain>();

    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void skill()
    {

    }
}
