using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour {

    [SerializeField] Text commandName;
    [SerializeField] Text command0;
    [SerializeField] Text command1;
    [SerializeField] Text command2;
    [SerializeField] Text command3;

    Text[] commandBax;


    [SerializeField] string[] actionKind;

    BattleMain battleMain;

	// Use this for initialization
	void Start () {

        commandBax = new Text[4]{ command0,command1,command2,command3};

        battleMain = GameObject.Find("GameManager").GetComponent<BattleMain>();

	}
	
	// Update is called once per frame
	void Update () {

        switch (battleMain.sendSelect())
        {
            case 0:

                for (int count = 0; count < 4; count++)
                {

                    commandName.text = "";
                    commandBax[count].text = "";

                }


                break;

            case 1:

                commandName.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                commandName.text = battleMain.sendActiveChara();

                for (int count = 0; count < 4; count++)
                {
                    
                    commandBax[count].color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                    commandBax[count].text = actionKind[count];

                }


                break;

            case 2:

                commandName.color = new Color(140f / 255f, 140f / 255f, 140f / 255f);
                commandName.text = "";

                for (int count = 0; count < 4; count++)
                {

                    commandBax[count].color = new Color(140f / 255f, 140f / 255f, 140f / 255f);
                    commandBax[count].text = actionKind[count];

                }


                break;
        }

        /*
        if ( battleMain.sendSelect() ==0 )
        {
            commandText.text = " ";
        }
        else
        {
            if ( battleMain.sendSelect() !=1 )
            {
                commandText.color = new Color(140f/255f,140f/255f,140f/255f);
            }
            else
            {
                commandText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
            }


            for (int count = 0;count<4;count++)
            {
                if (count == 0 )
                {
                    commandText.text = actionKind[count] + "\n";
                }
                else
                {
                    commandText.text += actionKind[count] + "\n";
                }

            }

        }
            */   
 
	}
}
