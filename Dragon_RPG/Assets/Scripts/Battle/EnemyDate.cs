using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDate : MonoBehaviour {

    BattleMain battle;
    startBattle start;

    [SerializeField] EnemyDataBase enemyDataBase;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;

    public List<GameObject> enemyObjects;

    private List<int> Para;
  
    int[] enemyPara ;

 
    List<EnemyDateTable> enemyDates;

    private void Awake()
    {
        battle = GameObject.Find("GameManager").GetComponent<BattleMain>();
        enemyObjects = new List<GameObject>() { enemy1,enemy2,enemy3 };

        enemyDates = enemyDataBase.GetenemyLists();

      
        for (int count =0;count < enemyDates.Count;count++)
        {
            battle.Enenames.Add(enemyDates[count].GetName());

            enemyObjects[count].GetComponent<SpriteRenderer>().sprite = enemyDates[count].GetImage();


            Para = new List<int>(enemyDates[count].GetPara());
            int[] enemyPara = new int[BattleMain.statusNum];

            for (int countN =0;countN < BattleMain.statusNum;countN++)
            {
                enemyPara[countN] = Para[countN];
            }

            battle.enemyStatus.Add(enemyPara);

        }

       

    }

    // Use this for initialization
    void Start()
    {

        Debug.Log(enemyDates[0].GetName());

        Para = new List<int>(enemyDates[0].GetPara());

        for (int count = 0; count < Para.Count; count++)
        {
            Debug.Log(Para[count]);
        }


    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
