using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{

    DragonDate dragon;
    PrincesDate princes;
    BattleProcess bProcess;
    EnemyDate enemyDate;
    TextOpen text;

    [SerializeField] Sprite dragonImage;
    [SerializeField] Sprite princesImage;


    delegate void action();
    action dd;
    List<action> playTurn = new List<action>();
    int playF = 0; //0行動開始可能　１行動中　２行動受付開始

    bool posF = false;
    bool selectF = false; // true = 行動選択　
    bool targetF = false; // true = 味方、敵選択

    int[] activeFs; //(個人用)　0無理　１行動可能　２行動待機
    bool[] alives;


    public static int statusNum = 5;

    [SerializeField] float waitTurnTime = 5f;//ターン間の時間

    enum statusPosition { HP = 0, TP = 1, Attack, Defence, Speed, Position, Time };

    //string[] names;
    public string[] names;
    public List<string> Enenames = new List<string>();

    public List<int[]> enemy = new List<int[]>();
    int totalEnemy = 0;
    int enemyMin = 2;
    int enemyMax = 1;

    float[] times;
    [SerializeField] float timeChargeMax = 10f;

    public int[,] status;//  ステータスの入れ物  //HP TP A D S Pos Time
    int activeChara = 0;//行動キャラ　0 = dragon, 1 = Princes ,2以降　＝敵
    int targetSelect = 2;//選択　上に同じ
    int actionSelect = 0;//選択肢上から０. １．２．３．を選択中　攻　防　魔　逃

    //int enemySelect = 0;

    // Use this for initialization
    void Start()
    {
        dragon = GameObject.Find("DateBase").GetComponent<DragonDate>();
        princes = GameObject.Find("DateBase").GetComponent<PrincesDate>();
        bProcess = GetComponent<BattleProcess>();
        enemyDate = GameObject.Find("DateBase").GetComponent<EnemyDate>();
        text = GameObject.Find("gameText").GetComponent<TextOpen>();
        
        Debug.Log(enemy.Count + "E");
        text.words.Add("バトルスタート");
        text.words.Add("敵は"+enemy.Count+"体だ");

        statusSet();

        //action dd = delegate () { Debug.Log("testes"); };
        //playTurn.Add(dd);

        /*画面の表示
         * 
         * 
         */

        posF = true;

    }

    // Update is called once per frame
    void Update()
    {
        buttonSelect();

        turnStart();

        TimeCharge();

        DeadCheck();

        winCheck();
        endCheck();

    }

    void endCheck()
    {
        if (alives[0] == false || alives[1] == false)
        {
            //　戦闘終了(負け)のやつ

            text.words.Add("やられちゃった（＞＋＜）");
        }
    }

    void DeadCheck()
    {
        int max = 0;
        int min = 10;

        for (int count  =0;count< enemy.Count+2;count++)
        {
            
            if (status[count,(int)statusPosition.HP] <= 0 && alives[count] ==true)
            {
                alives[count] = false;

                text.words.Add(names[count]+"は倒れた");
                
                if (count >1)
                {
                    totalEnemy--;
                    StartCoroutine(objectDelete(count-2));
                }
            }

            if (count >1 && alives[count] == true )
            {
                if (count < min)
                {
                    min = count;
                }

                if (count > max)
                {
                    max = count;
                }
            }
        }

        enemyMax = max;

        enemyMin = min;

    }

    IEnumerator objectDelete(int objectNum)
    {
        yield return new WaitForSeconds(2f);
        enemyDate.enemyObjects[objectNum - 2].SetActive(false);

    }

    void winCheck()
    {
        if (totalEnemy ==0)
        {
            // 戦闘終了（勝ち）のやつ
            Debug.Log("Win");
        }
    }

    void TimeCharge()
    {
        for (int count = 0; count < enemy.Count + 2; count++)
        {
      
            times[count] += Time.deltaTime;

            if (times[count] >= timeChargeMax)
            {
                times[count] = timeChargeMax;

                if (activeFs[count] == 0)
                {
                    if (activeFs[0] != 1 && activeFs[1] != 1)
                    {
                        if (count < 2)
                        {
                            Debug.Log("time" + count + "OK");
                            activeFs[count] = 1;

                            selectF = true;
                            activeChara = count;

                            text.words.Add(names[count]+"の行動可能");
                        }
                    }


                    if (count >= 2)
                    {
                        activeFs[count] = 1;

                        StartCoroutine(enemyActionSet(count));
                    }

                }

            }
        }
    }

    void turnStart()
    {
        if (playTurn.Count > 0 && playF == 0)
        {
            playF = 1;

            playTurn[0]();
            playTurn.RemoveAt(0);
        }
    }

    void statusSet()
    {
        totalEnemy = enemy.Count;
        enemyMax += enemy.Count;

        names = new string[enemy.Count + 2];

        names[0] = dragon.Name;
        names[1] = princes.Name;

        for (int count = 0;count < enemy.Count;count++)
        {
            names[count + 2] = Enenames[count];
        }

        times = new float[enemy.Count + 2];

        activeFs = new int[enemy.Count + 2];

        alives = new bool[enemy.Count + 2];

        for (int count = 0; count < enemy.Count + 2; count++)
        {
            activeFs[count] = 0;
            alives[count] = true;
        }

        status = new int[enemy.Count + 2, statusNum + 2];

        status[0, 0] = dragon.HP;
        status[0, 1] = dragon.TP;
        status[0, 2] = dragon.AttackP;
        status[0, 3] = dragon.DefenceP;
        status[0, 4] = dragon.SpeedP;
        status[0, 5] = 0;//Postion

        status[1, 0] = princes.HP;
        status[1, 1] = princes.TP;
        status[1, 2] = princes.AttackP;
        status[1, 3] = princes.DefenceP;
        status[1, 4] = princes.SpeedP;
        status[1, 5] = 0;//Postion

        for (int countE = 0; countE < enemy.Count; countE++)
        {
            for (int countS = 0; countS < statusNum; countS++)
            {
                status[countE + 2, countS] = enemy[countE][countS];
            }

            status[countE + 2, statusNum] = 0;    //敵のposition

            Debug.Log("enemySet");
        }
    }

    void buttonSelect()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetF == true)
        {
            targetSelect++;

            if (targetSelect >= enemyMax)
            {
                targetSelect = enemyMax;
            }
            else if (alives[targetSelect] == false)
            {
                targetSelect++;
            }

            Debug.Log(targetSelect + "C");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && targetF == true)
        {
            targetSelect--;

            if (targetSelect <= enemyMin)
            {
                targetSelect = enemyMin;
            }
            else if (alives[targetSelect] == false)
            {
                targetSelect--;
            }

            
            Debug.Log(targetSelect + "C");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            selectF = true;
            targetF = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && selectF == true)
        {
            actionSelect--;

            if (actionSelect < 0)
            {
                actionSelect = 0;
            }

            Debug.Log(actionSelect);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && selectF == true)
        {
            actionSelect++;

            if (actionSelect > 3)
            {
                actionSelect = 3;
            }

            Debug.Log(actionSelect);
        }

        if (Input.GetKeyDown(KeyCode.Return))//Enterキー
        {
            actionSet(activeChara);
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && posF == true)
        {
            status[1, 5]++;

            if (status[1, 5] > 2)
            {
                status[1, 5] = 2;
            }

            Debug.Log(status[1, 5]);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && posF == true)
        {
            status[1, 5]--;

            if (status[1, 5] < 0)
            {
                status[1, 5] = 0;
            }

            Debug.Log(status[1, 5]);
        }


    }

    void actionSet(int charaNum)
    {
        if (activeFs[charaNum] == 1 && playF != 1)
        {

            switch (actionSelect)
            {
                case 0:

                    //攻撃
                    if (selectF == true)
                    {
                        Debug.Log("ターゲット");

                        text.words.Add("相手を選択");

                        selectF = false;
                        targetF = true;

                        break;
                    }

                    dd = delegate ()
                    {
                        Debug.Log("A" + charaNum + "to" + targetSelect);

                        StartCoroutine(Attack(charaNum, targetSelect));

                    };

                    playTurn.Add(dd);

                    selectF = false;
                    targetF = false;
                    activeFs[charaNum] = 2;


                    break;

                case 1:

                    //スキル
                    dd = delegate ()
                    {
                        Debug.Log("S");

                        StartCoroutine(waitPlay(charaNum));
                    };

                    playTurn.Add(dd);

                    selectF = false;
                    targetF = false;
                    activeFs[charaNum] = 2;

                    break;

                case 2:

                    //交代
                    dd = delegate ()
                    {
                        Debug.Log("C");

                        StartCoroutine(waitPlay(charaNum));
                    };

                    playTurn.Add(dd);

                    selectF = false;
                    targetF = false;
                    activeFs[charaNum] = 2;

                    break;

                case 3:

                    //逃げる
                    dd = delegate ()
                    {
                        Debug.Log("E");

                        StartCoroutine(waitPlay(charaNum));
                    };

                    playTurn.Add(dd);

                    selectF = false;
                    targetF = false;
                    activeFs[charaNum] = 2;

                    break;
            }

        }

    }

    IEnumerator enemyActionSet(int actionEnemy)
    {
        yield return new WaitForSeconds(2f);

        int enemyActionNum = 0;//敵がどの行動を取るかの変数

        if (playF != 1)
        {

            switch (enemyActionNum)
            {
                case 0:

                    //攻撃              
                    dd = delegate ()
                    {
                        Debug.Log("A to" + 0);
                        Attack(actionEnemy, 0);

                        StartCoroutine(Attack(actionEnemy,0));
                    };

                    Debug.Log("enemySet");

                    playTurn.Add(dd);
                    activeFs[actionEnemy] = 2;

                    break;

                case 1:

                    //スキル
                    dd = delegate ()
                    {
                        Debug.Log("S");

                        //StartCoroutine(waitTime(Attacker));
                    };

                    playTurn.Add(dd);
                    activeFs[actionEnemy] = 2;

                    break;
            }
        }

    }

    //通常攻撃の処理　　引数は　攻撃するやつの番号 と　攻撃を受けるやつの番号（charaSelectのやつ）
    IEnumerator Attack(int Attacker, int target)
    {
        Debug.Log("A awake");

        text.words.Add(names[Attacker]+"の"+names[target]+"への攻撃");

        yield return new WaitForSeconds(1f);

        float damage = status[Attacker, (int)statusPosition.Attack];

        status[target, (int)statusPosition.HP] -= (int)damage;

        text.words.Add(names[target] + "に"+(int)damage+"のダメージ");

        playF = 2;

        StartCoroutine(waitPlay(Attacker));

        Debug.Log("Await");
    }

    IEnumerator Skill(int Skiller)
    {
        text.words.Add(names[Skiller] + "はスキルを使った");

        yield return new WaitForSeconds(1f);

        playF = 2;

        StartCoroutine(waitPlay(Skiller));
    }

    IEnumerator TimerChange(int giver,int receiver)
    {
        text.words.Add(names[giver] + "は力を与えた");

        yield return new WaitForSeconds(1f);

        times[receiver] = timeChargeMax;

        playF = 2;

        StartCoroutine(waitPlay(giver));
    }

    IEnumerator Escape(int Escaper)
    {
        Debug.Log("A awake");

        text.words.Add(names[Escaper] + "は逃げ出した");

        yield return new WaitForSeconds(1f);

        bool escapeF = true;

        for (int countE = 0; countE < enemy.Count; countE++)
        {
            int num = Random.Range(-50, 50);

            if (status[Escaper,(int)statusPosition.Speed] < num)
            {
                escapeF = false;
            }

        }

        if (escapeF == true)
        {
            //逃走成功
            Debug.Log("成功");
            text.words.Add(names[Escaper] + "は逃げ出した");
        }
        else
        {
            Debug.Log("失敗");
            text.words.Add(names[Escaper] + "は囲まれて逃げれない");

            playF = 2;

            StartCoroutine(waitPlay(Escaper));
        }
    }

    IEnumerator waitPlay(int actioner)
    {
        yield return new WaitForSeconds(waitTurnTime);
        activeFs[actioner] = 0;
        times[actioner] = 0;
        playF = 0;

        Debug.Log("waitEnd");
    }

    IEnumerator waitTurn()
    {
        yield return new WaitForSeconds(10f);
    }
}
