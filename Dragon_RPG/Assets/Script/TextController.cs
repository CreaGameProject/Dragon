using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextController : MonoBehaviour
{

    TextAsset csvFile;
    public string story;
    [SerializeField] Text uiText;   // uiTextへの参照
    [SerializeField] List<string> Stories = new List<string>();

    public MassageController massage;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.1f;
    private List<string>.Enumerator enumerator;
    private int currentSentenceNum = 0; //現在表示している文章番号
    public string currentSentence = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeBeganDisplay = 1;         // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;       // 表示中の文字数
    private bool line = true;
    private int nun = 0;


    void Start()
    {
        csvFile = Resources.Load(story) as TextAsset;
        StringReader reder = new StringReader(csvFile.text);
        while (reder.Peek() != -1)
        {
            string line = reder.ReadLine();
            Stories.Add(line);
        }
        enumerator = Stories.GetEnumerator();
        OnClick();
    }
    void Update()
    {

        if (IsDisplayComplete())
        {
            //最後の文章ではない & ボタンが押された
            if (currentSentenceNum < currentSentence.Length && Input.GetKeyUp(KeyCode.Space))
            {
                OnClick();
            }
        }
        else
        {
            //ボタンが押された
            if (Input.GetKeyUp(KeyCode.Space))
            {
                timeUntilDisplay = 0; 
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }

        //表示される文字数を計算
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);

        //表示される文字数が表示している文字数と違う
        if (displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            //表示している文字数の更新
            lastUpdateCharCount = displayCharCount;
        }
    }
    public void OnClick()
    {
        
        currentSentence = Stories[currentSentenceNum];
        //massage.GetComponent<MassageController>().sentence = currentSentence;
        currentSentence=currentSentence.ToString().Replace(@"_", System.Environment.NewLine);
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        Debug.Log(timeUntilDisplay);
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }
    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }
}