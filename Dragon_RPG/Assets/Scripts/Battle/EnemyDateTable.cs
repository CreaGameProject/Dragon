using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(menuName = "DateBase/Create EnemyDateTable", fileName = "EnemyDateTable")]
public class EnemyDateTable : ScriptableObject
{

    //　Asset内で素材のように敵キャラのステータスなどをいじるためのやつ

    public string Name;

    [SerializeField] private int HPmax;
    [SerializeField] private int TPmax;

    [SerializeField] private int AttackP;
    [SerializeField] private int DefenceP;
    [SerializeField] private int SpeedP;

    [SerializeField] private Sprite image;

    public List<int> Para;

    private void Awake()
    {
        Para = new List<int>() { HPmax,TPmax,AttackP,DefenceP,SpeedP};
    }

    public string GetName()
    {
        return Name;
    }

    public Sprite GetImage()
    {
        return image;
    }

    public List<int> GetPara()
    {
        return Para;
    }

    public int GetHP()
    {
        return HPmax;
    }

    public int GetTP()
    {
        return TPmax;
    }

    public int GetAttackP()
    {
        return AttackP;
    }

    public int GetDefenceP()
    {
        return DefenceP;
    }

    public int GetSpeedP()
    {
        return SpeedP;
    }

}

/*public class EnemyDateObject : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}*/
