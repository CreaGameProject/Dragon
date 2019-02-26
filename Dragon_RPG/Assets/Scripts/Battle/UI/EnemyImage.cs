using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImage : MonoBehaviour {

    [SerializeField] int enemyNum;

    BattleMain battleMain;

	// Use this for initialization
	void Start () {

        battleMain = GameObject.Find("GameManager").GetComponent<BattleMain>();
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3( (battleMain.status[enemyNum+2,5]*-2)-2+enemyNum*(0.5f), transform.position.y, transform.position.z);
		
	}
}
