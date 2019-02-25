using UnityEngine;

public class dialogueHolder : MonoBehaviour {

    public string dialogue;
    private DialogueManager dMAn;

    public string[] dialogueLines;
	// Use this for initialization
	void Start () {
        dMAn = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
            //Debug.Log("a");
        if(other.gameObject.name == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //Debug.Log("b");
                //dMAn.ShowBox(dialogue);

                if (!dMAn.dialogueActive)
                {
                    dMAn.dialogueLines = dialogueLines;
                    dMAn.currentLine = 0;
                    //Invoke("ShowDialogue", 0.5f);
                    dMAn.ShowDialogue();
                }
            }
        }
    }
}
