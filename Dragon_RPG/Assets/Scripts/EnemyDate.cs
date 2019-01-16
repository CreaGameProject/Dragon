using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDate : MonoBehaviour {

    BattleMain battle;

    startBattle start;

    [SerializeField] private EnemyDataBase enemyData;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;

    private List<int> Para;
    public List<int> enemy = new List<int>();

    public List<GameObject> enemyObjects;

    private void Awake()
    {
        battle = GameObject.Find("GameManager").GetComponent<BattleMain>();

        enemyObjects = new List<GameObject>() { enemy1,enemy2,enemy3 };

        int countMax = enemyData.GetenemyLists().Count;

        int paraNum = enemyData.GetenemyLists()[0].GetPara().Count;
        int[] enemyPara;

        for (int count =0;count < countMax;count++)
        {
            battle.Enenames.Add(enemyData.GetenemyLists()[count].GetName());

            Para = new List<int>(enemyData.GetenemyLists()[count].GetPara());

            enemyPara = new int[paraNum];

            enemyObjects[count].GetComponent<SpriteRenderer>().sprite = enemyData.GetenemyLists()[count].GetImage();

            for (int countN =0;countN < paraNum;countN++)
            {
                enemyPara[countN] = Para[countN];
            }

            battle.enemy.Add(enemyPara);

        }


    }

    // Use this for initialization
    void Start()
    {

        Debug.Log(enemyData.GetenemyLists()[0].GetName());

        Para = new List<int>(enemyData.GetenemyLists()[0].GetPara());

        for (int count = 0; count < Para.Count; count++)
        {
            Debug.Log(Para[count]);
        }


    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
