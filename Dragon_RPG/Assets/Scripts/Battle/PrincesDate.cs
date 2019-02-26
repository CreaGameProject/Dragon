using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincesDate : MonoBehaviour
{
    public string Name;
    [SerializeField] Sprite princesImage;

    public int HP;
    public int TP;

    public int AttackP;
    public int DefenceP;
    public int SpeedP;

    BattleMain battleMain;

    private void Awake()
    {
        battleMain = GameObject.Find("GameManager").GetComponent<BattleMain>();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3((battleMain.status[1,5] * 2) + 2 , transform.position.y, transform.position.z);

    }
}
