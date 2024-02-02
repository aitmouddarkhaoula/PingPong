using System.Collections;
using System.Collections.Generic;
using Oknaa.Scripts;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    //public PaddleController player;
    public Ball ball;
    public ServerManager serverManager;
    public Score score0;
    public Score score1;

    public EmojisPanel _emojisPanelClient;
    public EmojisPanel _emojisPanelServer;
    
    public int maxScore=5;
    // Start is called before the first frame update
    public void Init()
    {
        ball.Init();
        _emojisPanelClient.Init(!serverManager.IsServer);
        _emojisPanelServer.Init(serverManager.IsServer);
    } 
   
    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void Reset()
    {
        ball.Reset();
        //player.Reset();

    }
}
