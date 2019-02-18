using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBelt : MonoBehaviour {

    [SerializeField] Text timeBeltText;

    public List<string> timeBeltName = new List<string>();

	// Use this for initialization
	void Start () {

        timeBeltText.text = "";

	}
	
	// Update is called once per frame
	void Update () {

        if (timeBeltName.Count == 0)
        {
            timeBeltText.text = " ";
        }

        for (int count = 0; count< timeBeltName.Count;count++)
        {
            if (count == 0)
            {
                timeBeltText.text = timeBeltName[count] + "\n";
            }
            else
            {
                timeBeltText.text += timeBeltName[count] + "\n";
            }
            
        }
		
	}
}
