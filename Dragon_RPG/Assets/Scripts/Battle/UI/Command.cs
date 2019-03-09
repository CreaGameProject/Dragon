using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{

    [SerializeField] GameObject commandMark;
    [SerializeField] Text commandName;

    //[SerializeField] Text command0;
    //[SerializeField] Text command1;
    //[SerializeField] Text command2;
    //[SerializeField] Text command3;

    [SerializeField] GameObject commandCanvas2;
    [SerializeField] Text commandName2;

    //[SerializeField] Text command20;
    //[SerializeField] Text command21;
    //[SerializeField] Text command22;
    //[SerializeField] Text command23;

    [SerializeField] GameObject commandCanvas3;
    [SerializeField] Text commandName3;

    public Text[] commandBax;
    public Text[] commandBax2;
    public Text[] commandBax3;

    

    [SerializeField] string[] actionKind;

    string[] skillName;
    string[] enemyName;

    public Text[] commands;
    public int select;

    BattleMain battleMain;
    
    // Use this for initialization
    void Start()
    {

        //commandBax = new Text[4] { command0, command1, command2, command3 };
        //commandBax2 = new Text[4] { command20, command21, command22, command23 };
        enemyName = new string[4] {" "," "," "," " };
        skillName = new string[4] { "a", "b", "c", "d" };

        commands = commandBax;

        battleMain = GameObject.Find("GameManager").GetComponent<BattleMain>();

        for (int count =0;count < battleMain.Enenames.Count; count++)
        {
            enemyName[count] = battleMain.Enenames[count];
        }

    }

    // Update is called once per frame
    void Update()
    {
        markTrans(commands, battleMain.sendActionSelect());

        switch (battleMain.sendSelect())
        {
            case 0:

                commandMark.SetActive(false);
                commandCanvas2.SetActive(false);
                commandCanvas3.SetActive(false);

                for (int count = 0; count < 4; count++)
                {

                    commandName.text = "";
                    commandBax[count].text = "";

                }


                break;

            case 1:

                commandMark.SetActive(true);
                commandCanvas2.SetActive(false);
                commandCanvas3.SetActive(false);

                commands = commandBax;

                commandName.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                commandName.text = battleMain.sendActiveChara();

                textWhite( commandBax ,actionKind );


                break;


            case 2:

                commandMark.SetActive(true);
                commandCanvas2.SetActive(true);

                commands = commandBax2;



                break;



            case 3:

                commandMark.SetActive(true);
                commandCanvas3.SetActive(true);

                commands = commandBax3;


                commandName.color = new Color(140f / 255f, 140f / 255f, 140f / 255f);
                commandName.text = "";

                textBlack( commandBax , actionKind );
                textBlack(commandBax2, skillName);
                textWhite(commandBax3, enemyName);

               

                break;
        }

        
    }

    void markTrans(Text[] texts, int flag)
    {
        commandMark.transform.parent = texts[flag].transform;
        commandMark.transform.localPosition = new Vector3(-30, 0, 0);
    }


    void textBlack( Text[]texts , string[]frez)
    {
        for (int count = 0; count < 4; count++)
        {

            texts[count].color = new Color(140f / 255f, 140f / 255f, 140f / 255f);
            texts[count].text = frez[count];

        }
    }

    void textWhite( Text[]texts , string[]frez)
    {
        for (int count = 0; count < 4; count++)
        {

            texts[count].color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
            texts[count].text = frez[count];

        }
    }
}
