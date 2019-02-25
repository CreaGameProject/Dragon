using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encount : MonoBehaviour {

    [SerializeField] private int encountper;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Stay");
            if (Random.Range(0, encountper) == 50)
            {
                FadeManager.Instance.LoadScene("BattleScene", 1.0f);
            }
        }
    }
}
