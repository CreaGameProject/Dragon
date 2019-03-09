using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleMain : MonoBehaviour
{

    DragonDate dragon;
    PrincesDate princes;
    BattleProcess bProcess;
    EnemyDate enemyDate;
    TextOpen text;

    TimeBelt timeBelt;
    List<string> timeBeltList = new List<string>();


    delegate void action();
    action dd;
    List<action> playTurn = new List<action>();
    int playF = 0; //0行動開始可能　１行動中　２行動受付開始

    int selectF = 0 ; // 0=全ての選択無理  １= 行動選択 2= スキル選択　3=ターゲット選択　

    int[] activeFs; //(個人用)　0無理　１行動可能　２行動待機
    bool[] alives;


    public static int statusNum = 5;

    [SerializeField] float waitTurnTime = 5f;//ターン間の時間

    enum statusPosition { HP = 0, TP = 1, Attack, Defence, Speed, Position, Time };

    public string[] names;
    public List<string> Enenames = new List<string>();

    public List<int[]> enemyStatus = new List<int[]>();
    int totalEnemy = 0;
    int enemyMin = 2;
    int enemyMax = 1;

    public float[] times; // 各キャラの行動に関連する時間
    [SerializeField] float timeChargeMax = 10f;

    public int[,] status;//  ステータスの入れ物 // 左キャラ  //右　HP TP A D S Pos 
    int activeChara = 0;//行動キャラ　0 = dragon, 1 = Princes ,2以降　＝敵
    int targetSelect = 2;//選択　上に同じ
    int actionSelect = 0;//選択肢上から０. １．２．３．を選択中　攻　技　交　逃

    
    // Use this for initialization
    void Start()
    {
        dragon = GameObject.Find("DateBase").GetComponent<DragonDate>();
        princes = GameObject.Find("DateBase").GetComponent<PrincesDate>();
        bProcess = GetComponent<BattleProcess>();
        enemyDate = GameObject.Find("DateBase").GetComponent<EnemyDate>();
        text = GameObject.Find("gameText").GetComponent<TextOpen>();

        timeBelt = GameObject.Find("Canvas").GetComponent<TimeBelt>();
        timeBeltList = timeBelt.timeBeltName;

        Debug.Log(enemyStatus.Count + "E");
        text.words.Add("バトルスタート");
        text.words.Add("敵は"+enemyStatus.Count+"体だ");

        statusSet();

    }

    void statusSet()
    {
        totalEnemy = enemyStatus.Count;
        enemyMax += enemyStatus.Count;

        names = new string[enemyStatus.Count + 2];

        names[0] = dragon.Name;
        names[1] = princes.Name;

        for (int count = 0; count < enemyStatus.Count; count++)
        {
            names[count + 2] = Enenames[count];
        }

        times = new float[enemyStatus.Count + 2];
        activeFs = new int[enemyStatus.Count + 2];
        alives = new bool[enemyStatus.Count + 2];

        for (int count = 0; count < enemyStatus.Count + 2; count++)
        {
            activeFs[count] = 0;
            alives[count] = true;
        }

        status = new int[enemyStatus.Count + 2, statusNum + 2];

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

        for (int countE = 0; countE < enemyStatus.Count; countE++)
        {
            for (int countS = 0; countS < statusNum; countS++)
            {
                status[countE + 2, countS] = enemyStatus[countE][countS];
            }

            status[countE + 2, statusNum] = countE;    //敵のposition

        }
    }　　//各キャラのステータスを格納

    public int sendPlay()
    {
        return playF;
    } //別のプログラムに送る用

    public int sendSelect()
    {
        return selectF;
    }　　　//別のプログラムに送る用

    public int sendActionSelect()
    {
        switch (selectF)
        {
            case 0:
                return actionSelect;
                break;

            case 1:  //行動選択時
                return actionSelect;
                break;

            case 2:  //スキル時
                return actionSelect;
                break;

            case 3:　//たいしょう時
                return targetSelect-2;
                break;

            default:
                return actionSelect;
                break;

        }


        
    }　　　//別のプログラムに送る用

    public string sendActiveChara()
    {
        return names[activeChara];
    }　　　//別のプログラムに送る用

    public float sendTimeChargeMax()
    {
        return timeChargeMax;
    }     //別のプログラムに送る用


    // Update is called once per frame
    void Update()
    {
        buttonSelect();

        turnStart();

        TimeCharge();

        DeadCheck();

        endCheck();

    }

    void endCheck()
    {
        if (alives[0] == false || alives[1] == false)
        {
            //　戦闘終了(負け)のやつ
            text.words.Add("やられちゃった（＞＋＜）");
        }

        if (totalEnemy == 0)
        {
            // 戦闘終了（勝ち）のやつ
            text.words.Add("勝ったよ～～～（＿＋＿）");
        }
    }　//バトル終了チェック

    void DeadCheck()
    {
        int max = 0;
        int min = 10;

        for (int count  =0;count< enemyStatus.Count+2;count++)
        {
            
            if (status[count,(int)statusPosition.HP] <= 0 && alives[count] ==true)
            {
                status[count, (int)statusPosition.HP] = 0;
                alives[count] = false;
                timeBeltList.Remove(names[count]);

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

    } //死んだか判定

    IEnumerator objectDelete(int objectNum)
    {
        yield return new WaitForSeconds(2f);
        enemyDate.enemyObjects[objectNum].SetActive(false);

    }　//死んだ敵を画面から消す

    void TimeCharge()
    {
        for (int count = 0; count < enemyStatus.Count + 2; count++)
        {

            if (activeFs[count] ==0) {
                times[count] += Time.deltaTime;
            }

            if (times[count] >= timeChargeMax)
            {
                times[count] = timeChargeMax;

                if (activeFs[count] == 0 && playF != 1 )//　個人の行動と全体での行動ができない
                {
                    if (selectF == 0)  //　行動選択可能でない場合　　ということ
                    {
                        if (count < 2)　//主人公と姫用
                        {
                            Debug.Log("time" + count + "OK");
                            activeFs[count] = 1;

                            selectF = 1;
                            activeChara = count;

                        }
                    }


                    if (count >= 2)　//敵キャラ用　   
                    {
                        activeFs[count] = 1;

                        StartCoroutine(enemyActionSet(count));
                    }

                }

            }
        }
    }　 //各キャラの待機時間をためる

    void turnStart()
    {
        if (playTurn.Count > 0 && playF == 0)
        {
            playF = 1;

            playTurn[0]();
            playTurn.RemoveAt(0);
            
        }
    }　　//playTurnに入ってる行動を呼ぶ
    
    void buttonSelect()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && selectF == 3)
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
        else if (Input.GetKeyDown(KeyCode.UpArrow) && selectF == 3)
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
            
            if(selectF ==2){

                selectF--;

            }
            else if (selectF == 3 && actionSelect == 0)
            {
                selectF = 1;
            }
            else if (selectF ==3 && actionSelect ==1)
            {

            }


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && selectF == 1)
        {
            actionSelect--;

            if (actionSelect < 0)
            {
                actionSelect = 0;
            }

            Debug.Log(actionSelect);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && selectF == 1)
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

        if (Input.GetKeyDown(KeyCode.RightShift) && playF != 1)
        {
            status[1, 5]++;

            if (status[1, 5] > 2)
            {
                status[1, 5] = 2;
            }

            Debug.Log(status[1, 5]);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playF != 1)
        {
            status[1, 5]--;

            if (status[1, 5] < 0)
            {
                status[1, 5] = 0;
            }

            Debug.Log(status[1, 5]);
        }


    }  //ボタンによる行動選択

    void actionSet(int charaNum)
    {
        if (activeFs[charaNum] == 1 && playF != 1)
        {

            switch (actionSelect)
            {
                case 0:

                    //攻撃
                    if (selectF == 1)
                    {
                        Debug.Log("ターゲット");
                        text.words.Add("相手を選択");

                        targetSelect = enemyMin;
                        selectF = 3;

                        break;
                    }

                    dd = delegate ()
                    {

                        StartCoroutine(Attack(charaNum, targetSelect));

                    };


                    playTurn.Add(dd);
                    timeBeltList.Add(names[charaNum]);

                    selectF = 0;
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
                    timeBeltList.Add(names[charaNum]);

                    selectF = 0;
                    activeFs[charaNum] = 2;

                    break;

                case 2:

                    //交代
                    dd = delegate ()
                    {
                        Debug.Log("C");

                        StartCoroutine(TimerChange(charaNum));
                    };

                    playTurn.Add(dd);
                    timeBeltList.Add(names[charaNum]);

                    selectF = 0;
                    activeFs[charaNum] = 2;

                    break;

                case 3:

                    //逃げる
                    dd = delegate ()
                    {
                        Debug.Log("E");

                        StartCoroutine(Escape(charaNum));
                    };

                    playTurn.Add(dd);
                    timeBeltList.Add(names[charaNum]);

                    selectF = 0;
                    activeFs[charaNum] = 2;

                    break;
            }

        }

    }   //エンターキーで行動選択を終了した時　選択に応じて行動をplayTurn(List)に Add

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
                    timeBeltList.Add(names[actionEnemy]);

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

    }  //actionSetの敵版

    //通常攻撃の処理(Setで入れる行動の中身)　　引数は　攻撃するやつの番号 と　攻撃を受けるやつの番号
    IEnumerator Attack(int Attacker, int target)
    {
        if (alives[Attacker] == true) {

            Debug.Log("A awake");

            if (alives[target] == false)
            {
                target = enemyMin;
            }

            text.words.Add(names[Attacker] + "の" + names[target] + "への攻撃");

            yield return new WaitForSeconds(1f);

            float damage = status[Attacker, (int)statusPosition.Attack];

            status[target, (int)statusPosition.HP] -= (int)damage;

            text.words.Add(names[target] + "に" + (int)damage + "のダメージ");

            

            Debug.Log("Await");
        }

        playF = 2;

        StartCoroutine(waitPlay(Attacker));
    }

    IEnumerator Skill(int Skiller)
    {
        if (alives[Skiller] == true) {

            text.words.Add(names[Skiller] + "はスキルを使った");

            yield return new WaitForSeconds(1f);

        }

        playF = 2;

        StartCoroutine(waitPlay(Skiller));
    }

    IEnumerator TimerChange(int giver)
    {
        if (alives[giver] == true)
        {

            text.words.Add(names[giver] + "は力を与えた");

            yield return new WaitForSeconds(1f);

            times[Mathf.Abs(giver - 1)] = timeChargeMax;

            Debug.Log("wwww");

        }

        playF = 2;

        StartCoroutine(waitPlay(giver));

    }　//味方の待機時間を短縮するやつ

    IEnumerator Escape(int Escaper)
    {
        if (alives[Escaper] == true)
        {

            Debug.Log("A awake");

            text.words.Add(names[Escaper] + "は逃げ出した");

            yield return new WaitForSeconds(1f);

            bool escapeF = true;

            for (int count = 0; count < enemyStatus.Count; count++)
            {
                int num = Random.Range(-50, 50);

                if (status[Escaper, (int)statusPosition.Speed] < num && alives[count + 2] == true)
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
    }

    //

    IEnumerator waitPlay(int actioner)
    {
        times[actioner] = 0;
        
        yield return new WaitForSeconds(waitTurnTime);
        activeFs[actioner] = 0;

        if (alives[actioner] == true) {
            timeBeltList.RemoveAt(0);
        }

        playF = 0;

        Debug.Log("waitEnd");
    }　　//各行動の間のインターバル

    IEnumerator waitTurn()
    {
        yield return new WaitForSeconds(10f);
    }
}
