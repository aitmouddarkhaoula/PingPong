using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Player ball;
    // Start is called before the first frame update
    public void Init()
    {
       // ball.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void Reset()
    {
        // player.Reset();
    }
}
