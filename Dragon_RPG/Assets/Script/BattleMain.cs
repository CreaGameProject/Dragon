using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{

    DragonDate dragon;
    PrincesDate princes;

    int DHP;//dragonステ
    int DTP;
    int Dattack;
    int Ddefence;
    int Dspeed;

    int PHP;//Princesステ
    int PTP;
    int Pattack;
    int Pdefence;
    int Pspeed;

    int pPostion = 0;

    delegate void DDD();


    int[,] status; //HP TP A D S Pos Time Play
    bool charaSelect = true;//選択　true = dragon, false = Princes
    int actionSelect = 0;//選択肢上から０. １．２．３．を選択中　攻　防　魔　逃

    List<GameObject> enemy = new List<GameObject>();
    int enemySelect = 0;

    // Use this for initialization
    void Start()
    {

        dragon = GameObject.Find("DateBase").GetComponent<DragonDate>();
        princes = GameObject.Find("DateBase").GetComponent<PrincesDate>();


        /*画面の表示
         * 
         * 
         */



    }

    // Update is called once per frame
    void Update()
    {
        buttonSelect();
    }

    void buttonSelect()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            charaSelect = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            charaSelect = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            actionSelect--;

            if (actionSelect < 0)
            {
                actionSelect = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            actionSelect++;

            if (actionSelect > 3)
            {
                actionSelect = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))//Enterキー
        {
            actionStart();
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            pPostion++;

            if (pPostion > 2)
            {
                pPostion = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            pPostion--;

            if (pPostion <0)
            {
                pPostion = 0;
            }
        }


    }

    void actionStart()
    {
            switch (actionSelect)
            {
                case 0:

                    Debug.Log("A");

                    break;

                case 1:

                    Debug.Log("D");

                    break;

                case 2:

                    Debug.Log("S");

                    break;

                case 3:

                    Debug.Log("E");

                    break;
            }
             
    }

    void FuncTest()
    {
        Debug.Log("kansu");
    }
}
