using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "CreateEnemyDataBase")]
public class EnemyDataBase : ScriptableObject
{

    //[SerializeField]
    //private List<EnemyDateTable> enemyLists = new List<EnemyDateTable>();

    public List<EnemyDateTable> enemyLists = new List<EnemyDateTable>();

    //　アイテムリストを返す
    public List<EnemyDateTable> GetenemyLists()
    {
        return enemyLists;
    }
}

