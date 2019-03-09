using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour {

    [SerializeField] Slider rSlider;
    [SerializeField] Slider lSlider;

    BattleMain battleMain;
   
    // Use this for initialization
    void Start () {

        battleMain = GameObject.Find("GameManager").GetComponent<BattleMain>();

	}
	
	// Update is called once per frame
	void Update () {

        rSlider.maxValue = battleMain.sendTimeChargeMax();
        lSlider.maxValue = battleMain.sendTimeChargeMax();

        rSlider.value = battleMain.times[0];
        lSlider.value = battleMain.times[1];
		
	}
}
