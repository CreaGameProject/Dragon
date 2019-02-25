using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour
{

    GameObject Player;
    GameObject homingObj;
    public float Speed;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        homingObj = GameObject.Find("Homing");
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector2.Distance(this.transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y));
        if (Distance > 1.5)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), Speed * Time.deltaTime);
        }
        else if(Distance < 5.0)
        {
            
        }
    }
}