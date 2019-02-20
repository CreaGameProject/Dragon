using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextOpen : MonoBehaviour {

    [SerializeField] Text text;

    public List<string> words = new List<string>();

    bool wordF = true;

    [SerializeField] float waitWordTime = 3f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (words.Count > 0 && wordF == true)
        {
            wordF = false;

            text.text = words[0];
            words.RemoveAt(0);

            StartCoroutine( waitTime(waitWordTime) );
            
        }

    }

    IEnumerator waitTime(float Time)
    {
        yield return new WaitForSeconds(Time);

        text.text = " ";

        wordF = true;
    }
}
